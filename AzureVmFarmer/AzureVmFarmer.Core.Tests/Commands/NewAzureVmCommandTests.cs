using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class NewAzureVmCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new NewAzureVmCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_LocationIsEmpty_SetsNothing()
		{
			var expected = new NewAzureVmCommand
			{
				Location = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.LocationParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_LocationIsSomething_SetsLocation()
		{
			var expected = new NewAzureVmCommand
			{
				Location = "SomeLocation"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.LocationParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.Location));
		}

		[Test]
		public void CommandOperator_ServiceNameIsEmpty_SetsNothing()
		{
			var expected = new NewAzureVmCommand
			{
				ServiceName = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.ServiceNameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ServiceNameIsSomething_SetsServiceName()
		{
			var expected = new NewAzureVmCommand
			{
				ServiceName = "SomeService"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.ServiceNameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.ServiceName));
		}

		[Test]
		public void CommandOperator_DeploymentNameIsEmpty_SetsNothing()
		{
			var expected = new NewAzureVmCommand
			{
				DeploymentName = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.DeploymentNameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_DeploymentNameIsSomething_SetsDeploymentName()
		{
			var expected = new NewAzureVmCommand
			{
				DeploymentName = "SomeDeployment"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.DeploymentNameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.DeploymentName));
		}

		[Test]
		public void CommandOperator_WaitForBootIsFalse_SetsNothing()
		{
			var expected = new NewAzureVmCommand
			{
				WaitForBoot = false
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.WaitForBootParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_WaitForBootIsTrue_SetsWaitForBoot()
		{
			var expected = new NewAzureVmCommand
			{
				WaitForBoot = true
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmCommand.WaitForBootParameter);

			Assert.That(actual, Is.Not.Null);
		}
	}
}
