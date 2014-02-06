using System;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class Messenger : IMessenger
	{
		public void QueueCreateMessage(VirtualMachine virtualMachine)
		{
			QueueMessage("Create", virtualMachine);
		}

		public void QueueDeleteMessage(VirtualMachine virtualMachine)
		{
			QueueMessage("Delete", virtualMachine);
		}

		private static void QueueMessage(string messageType, VirtualMachine virtualMachine)
		{
			if (VirtualMachine.IsValid(virtualMachine) == false)
			{
				throw new ArgumentException("Invalid virtual machine.", "virtualMachine");
			}

			var connectionString = CloudConfigurationManager.GetSetting("ServiceBus.ConnectionString");
			var queueName = CloudConfigurationManager.GetSetting("QueueName");

			var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

			if (!namespaceManager.QueueExists(queueName))
			{
				var queue = new QueueDescription(queueName)
				{
					DefaultMessageTimeToLive = new TimeSpan(7, 0, 0, 0)
				};

				namespaceManager.CreateQueue(queue);
			}

			var message = PrepareMessage(messageType, virtualMachine);

			var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

			client.Send(message);
		}

		private static BrokeredMessage PrepareMessage(string messageType, VirtualMachine virtualMachine)
		{
			var message = new BrokeredMessage();
			message.SetMessageType(messageType);
			message.SetObject(virtualMachine);

			return message;
		}
	}
}
