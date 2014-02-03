using AzureVmFarmer.Core.Messengers.Impl;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Messengers
{
	[TestFixture]
	public class MessengerTest
	{
		public BrokeredMessage RetrieveQueuedMessage()
		{
			var connectionString = CloudConfigurationManager.GetSetting("ServiceBus.ConnectionString");
			var queueName = CloudConfigurationManager.GetSetting("QueueName");

			var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

			if (!namespaceManager.QueueExists(queueName))
			{
				namespaceManager.CreateQueue(queueName);
			} 
			
			var client = QueueClient.CreateFromConnectionString(connectionString, queueName);

			var result = client.Receive();

			return result;
		}

		[TearDown]
		public void TearDown()
		{
			var orphan = RetrieveQueuedMessage();

			while (orphan != null)
			{
				orphan.Complete();
				orphan = RetrieveQueuedMessage();
			}
		}

		[Test]
		public void QueueCreateMessage_NoData_ThrowsArgumentException()
		{
			var messenger = new Messenger();

			Assert.That(() => messenger.QueueCreateMessage(null), Throws.ArgumentException);
		}

		[Test]
		public void QueueCreateMessage_InvalidVirtualMachine_ThrowsArgumentException()
		{
			var messenger = new Messenger();
			var virtualMachine = new VirtualMachine();

			Assert.That(() => messenger.QueueCreateMessage(virtualMachine), Throws.ArgumentException);
		}

		[Test]
		public void QueueCreateMessage_ValidVirtualMachine_QueuesMessage()
		{
			var messenger = new Messenger();
			var virtualMachine = new VirtualMachine { Name = "Test" };

			messenger.QueueCreateMessage(virtualMachine);

			//TODO: Verify message on test queue
			var result = RetrieveQueuedMessage();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Properties["VirtualMachine.Name"], Is.EqualTo("Test"));
			result.Complete();
		}
	}
}
