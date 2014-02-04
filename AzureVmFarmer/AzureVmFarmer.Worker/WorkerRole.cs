using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using AzureVmFarmer.Core.Messengers;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using StructureMap;

namespace AzureVmFarmer.Worker
{
	public class WorkerRole : RoleEntryPoint
	{
		// QueueClient is thread-safe. Recommended that you cache 
		// rather than recreating it on every request
		private QueueClient _client;
		private readonly ManualResetEvent _completedEvent = new ManualResetEvent(false);
		private readonly IMessageHandler _handler;

		public WorkerRole()
		{
			StructureMapConfig.Bootstrap();
			_handler = ObjectFactory.GetInstance<IMessageHandler>();
		}

		public override void Run()
		{
			Trace.WriteLine("Starting processing of messages");

			// Initiates the message pump and callback is invoked for each message that is received, calling close on the client will stop the pump.
			_client.OnMessage((receivedMessage) =>
				{
					try
					{
						_handler.HandleMessage(receivedMessage);
						receivedMessage.Complete();
					}
					catch
					{
						receivedMessage.Abandon();
					}
				});

			_completedEvent.WaitOne();
		}

		public override bool OnStart()
		{
			// Set the maximum number of concurrent connections 
			ServicePointManager.DefaultConnectionLimit = 12;

			// Create the queue if it does not exist already
			string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
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
