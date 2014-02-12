using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class GetAzureVmCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new GetAzureVmCommand();

			Command actual = expected;

			Assert.That(actual.Parameters.Count, Is.EqualTo(0));
		}

		[Test]
		public void CommandOperator_NameIsEmpty_SetsNothing()
		{
			var expected = new GetAzureVmCommand
			{
				Name = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == GetAzureVmCommand.NameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_NameIsSomething_SetsName()
		{
			var expected = new GetAzureVmCommand
			{
				Name = "SomeName"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == GetAzureVmCommand.NameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.Name));
		}

		[Test]
		public void CommandOperator_ServiceNameIsEmpty_SetsNothing()
		{
			var expected = new GetAzureVmCommand
			{
				ServiceName = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == GetAzureVmCommand.ServiceNameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ServiceNameIsSomething_SetsServiceName()
		{
			var expected = new GetAzureVmCommand
			{
				ServiceName = "SomeName"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == GetAzureVmCommand.ServiceNameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.ServiceName));
		}
	}
}
