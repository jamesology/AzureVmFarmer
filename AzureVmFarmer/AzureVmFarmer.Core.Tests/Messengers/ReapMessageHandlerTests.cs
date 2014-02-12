using AzureVmFarmer.Core.Messengers.Impl;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;
using Rhino.Mocks;

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

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();

			var messageHandler = new ReapMessageHandler(executor);

			Assert.That(() => messageHandler.HandleMessage(message), Throws.ArgumentException);
		}

		//TODO: complete testing when powershell abstraction exists.
	}
}
