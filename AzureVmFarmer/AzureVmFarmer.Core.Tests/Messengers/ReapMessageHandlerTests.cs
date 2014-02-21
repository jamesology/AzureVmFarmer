using System.Linq;
using System.Management.Automation;
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

		[Test]
		public void HandleMessage_VirtualMachineDoesNotExist_PowershellIsInvokedOnce()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("Delete");
			message.SetObject(new VirtualMachine {Name = "SomeName"});

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();
			executor.Expect(x => x.Execute(Arg<PowerShellCommand[]>.Is.TypeOf))
				.Return(Enumerable.Empty<PSObject>())
				.Repeat.Once();

			var messageHandler = new ReapMessageHandler(executor);

			messageHandler.HandleMessage(message);

			executor.VerifyAllExpectations();
		}

		[Test]
		public void HandleMessage_VirtualMachineExists_PowershellIsInvokedTwice()
		{
			var message = new BrokeredMessage();
			message.SetMessageType("Delete");
			message.SetObject(new VirtualMachine { Name = "SomeName" });

			var executor = MockRepository.GenerateStub<IPowershellExecutor>();
			executor.Expect(x => x.Execute(Arg<PowerShellCommand[]>.Is.TypeOf))
				.Return(new[] { new PSObject() })
				.Repeat.Times(2);

			var messageHandler = new ReapMessageHandler(executor);

			messageHandler.HandleMessage(message);

			executor.VerifyAllExpectations();
		}
	}
}
