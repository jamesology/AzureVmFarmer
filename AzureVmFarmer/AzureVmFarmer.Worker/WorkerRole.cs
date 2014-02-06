using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using AzureVmFarmer.Core.Messengers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using StructureMap;
using StructureMap.Diagnostics;

namespace AzureVmFarmer.Worker
{
	public class WorkerRole : RoleEntryPoint
	{
		// QueueClient is thread-safe. Recommended that you cache 
		// rather than recreating it on every request
		private QueueClient _client;
		private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);
		private IMessageHandler _handler;

		public override void Run()
		{
			ICollection<Exception> errorCollection = new List<Exception>();
			Trace.WriteLine("Starting processing of messages");

			// Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
			_client.OnMessage((receivedMessage) =>
				{
					try
					{
						_handler.HandleMessage(receivedMessage);
						//TODO: make this all async and ack the message
					}
					catch(Exception ex)
					{
						errorCollection.Add(ex);
					}
				});

			_completedEvent.WaitOne();
		}

		public override bool OnStart()
		{
			StructureMapConfig.Bootstrap();
			_handler = ObjectFactory.GetInstance<IMessageHandler>();

			// Set the maximum number of concurrent connections 
			ServicePointManager.DefaultConnectionLimit = 12;

			// Create the queue if it does not exist already
			var connectionString = CloudConfigurationManager.GetSetting("ServiceBus.ConnectionString");
			var queueName = CloudConfigurationManager.GetSetting("QueueName");

			var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
			if (!namespaceManager.QueueExists(queueName))
			{
				namespaceManager.CreateQueue(queueName);
			}

			// Initialize the connection to Service Bus Queue
			_client = QueueClient.CreateFromConnectionString(connectionString, queueName);
			return base.OnStart();
		}

		public override void OnStop()
		{
			// Close the connection to Service Bus Queue
			_client.Close();
			_completedEvent.Set();
			base.OnStop();
		}

	}
}
