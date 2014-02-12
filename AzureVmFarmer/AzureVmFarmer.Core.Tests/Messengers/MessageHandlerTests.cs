using AzureVmFarmer.Core.Messengers.Impl;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Messengers
{
	[TestFixture]
	class MessageHandlerTests
	{
		[Test]
		public void HandleMessage_InvalidMessageType_ThrowsArgumentException()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("InvalidType");

			var messageHandler = new MessageHandler();

			Assert.That(() => messageHandler.HandleMessage(message), Throws.ArgumentException);
		}
	}
}
