using AzureVmFarmer.Core.Messengers.Impl;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;
using Rhino.Mocks;

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

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();

			var messageHandler = new SowMessageHandler(executor);

			Assert.That(() => messageHandler.HandleMessage(message), Throws.ArgumentException);
		}

		//TODO: complete testing when powershell abstraction exists.
	}
}
