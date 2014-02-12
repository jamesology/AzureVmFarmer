using AzureVmFarmer.Core.Messengers.Impl;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Messengers
{
	[TestFixture]
	class SowMessageHandlerTests
	{
		[Test]
		public void HandleMessage_MessageTypeIsCreate_ThrowsArgumentException()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("Delete");

			var messageHandler = new SowMessageHandler();

			Assert.That(() => messageHandler.HandleMessage(message), Throws.ArgumentException);
		}

		//TODO: complete testing when powershell abstraction exists.
	}
}
