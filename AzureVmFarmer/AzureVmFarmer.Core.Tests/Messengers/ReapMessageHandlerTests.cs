using AzureVmFarmer.Core.Messengers.Impl;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Messengers
{
	[TestFixture]
	class ReapMessageHandlerTests
	{
		[Test]
		public void HandleMessage_MessageTypeIsCreate_ThrowsArgumentException()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("Create");

			var messageHandler = new ReapMessageHandler();

			Assert.That(() => messageHandler.HandleMessage(message), Throws.ArgumentException);
		}

		//TODO: complete testing when powershell abstraction exists.
	}
}
