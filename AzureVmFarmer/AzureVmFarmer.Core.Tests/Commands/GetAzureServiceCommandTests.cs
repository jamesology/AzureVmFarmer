using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class GetAzureServiceCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new GetAzureServiceCommand();

			Command actual = expected;

			Assert.That(actual.Parameters.Count, Is.EqualTo(0));
		}

		[Test]
		public void CommandOperator_ServiceNameIsEmpty_SetsNothing()
		{
			var expected = new GetAzureServiceCommand
			{
				ServiceName = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == GetAzureServiceCommand.ServiceNameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ServiceNameIsSomething_SetsServiceName()
		{
			var expected = new GetAzureServiceCommand
			{
				ServiceName = "SomeService"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == GetAzureServiceCommand.ServiceNameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.ServiceName));
		}
	}
}
