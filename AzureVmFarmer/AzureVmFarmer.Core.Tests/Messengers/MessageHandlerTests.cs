using AzureVmFarmer.Core.Messengers.Impl;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;
using Rhino.Mocks;

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

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();

			var messageHandler = new MessageHandler(executor);

			Assert.That(() => messageHandler.HandleMessage(message), Throws.ArgumentException);
		}
	}
}
