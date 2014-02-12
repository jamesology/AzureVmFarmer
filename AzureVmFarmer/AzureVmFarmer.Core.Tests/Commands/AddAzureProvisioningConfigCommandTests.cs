using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class AddAzureProvisioningConfigCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new AddAzureProvisioningConfigCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_AdminPasswordIsEmpty_SetsNothing()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				AdminPassword = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.PasswordParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_AdminPasswordIsSomething_SetsAdminPassword()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				AdminPassword = "SomePassword"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.PasswordParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.AdminPassword));
		}

		[Test]
		public void CommandOperator_AdminUserNameIsEmpty_SetsNothing()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				AdminUsername = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.AdminUsernameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_AdminUserNameIsSomething_SetsAdminUserName()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				AdminUsername = "SomeUserName"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.AdminUsernameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.AdminUsername));
		}

		[Test]
		public void CommandOperator_TimeZoneIsEmpty_SetsNothing()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				TimeZone = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.TimeZoneParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_TimeZoneIsSomething_SetsTimeZone()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				TimeZone = "SomeTimeZone"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.TimeZoneParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.TimeZone));
		}

		[Test]
		public void CommandOperator_WindowsIsFalse_SetsNothing()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				Windows = false
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.WindowsParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_WindowsIsTrue_SetsWindows()
		{
			var expected = new AddAzureProvisioningConfigCommand
			{
				Windows = true
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == AddAzureProvisioningConfigCommand.WindowsParameter);

			Assert.That(actual, Is.Not.Null);
		}
	}
}
