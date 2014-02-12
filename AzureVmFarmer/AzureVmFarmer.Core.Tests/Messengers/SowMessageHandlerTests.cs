using System.Linq;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Core.Messengers.Impl;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using AzureVmFarmer.Objects;
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


		[Test]
		public void HandleMessage_VirtualMachineExists_PowershellIsInvokedOnce()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("Create");
			message.SetObject(new VirtualMachine { Name = "SomeName" });

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();
			executor.Expect(x => x.Execute(Arg<PowerShellCommand[]>.Is.TypeOf))
				.Return(new[] { new object() })
				.Repeat.Once();

			var messageHandler = new SowMessageHandler(executor);

			messageHandler.HandleMessage(message);

			executor.VerifyAllExpectations();
		}

		[Test]
		public void HandleMessage_VirtualMachineDoesNotExist_PowershellIsInvokedALot()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("Create");
			message.SetObject(new VirtualMachine { Name = "SomeName" });

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();
			executor.Expect(x => x.Execute(Arg<PowerShellCommand[]>.Is.TypeOf))
				.Return(Enumerable.Empty<object>())
				.Repeat.Times(6);

			var messageHandler = new SowMessageHandler(executor);

			messageHandler.HandleMessage(message);

			executor.VerifyAllExpectations();
		}
	}
}
