using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class RemoveAzureServiceCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new RemoveAzureServiceCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_ServiceNameIsEmpty_SetsNothing()
		{
			var expected = new RemoveAzureServiceCommand
			{
				ServiceName = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == RemoveAzureServiceCommand.ServiceNameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ServiceNameIsSomething_SetsServiceName()
		{
			var expected = new RemoveAzureServiceCommand
			{
				ServiceName = "SomeName"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == RemoveAzureServiceCommand.ServiceNameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.ServiceName));
		}

		[Test]
		public void CommandOperator_ForceIsFalse_SetsNothing()
		{
			var expected = new RemoveAzureServiceCommand
			{
				Force = false
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == RemoveAzureServiceCommand.ForceParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ForceIsTrue_SetsForce()
		{
			var expected = new RemoveAzureServiceCommand
			{
				Force = true
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == RemoveAzureServiceCommand.ForceParameter);

			Assert.That(actual, Is.Not.Null);
		}

		[Test]
		public void CommandOperator_DeleteAllIsFalse_SetsNothing()
		{
			var expected = new RemoveAzureServiceCommand
			{
				DeleteAll = false
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == RemoveAzureServiceCommand.DeleteAllParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_DeleteAllIsTrue_SetsDeleteAll()
		{
			var expected = new RemoveAzureServiceCommand
			{
				DeleteAll = true
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == RemoveAzureServiceCommand.DeleteAllParameter);

			Assert.That(actual, Is.Not.Null);
		}
	}
}
