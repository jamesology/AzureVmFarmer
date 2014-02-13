using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Objects;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class NewAzureVmConfigCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new NewAzureVmConfigCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_NameIsEmpty_SetsNothing()
		{
			var expected = new NewAzureVmConfigCommand
			{
				Name = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmConfigCommand.NameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_NameIsSomething_SetsName()
		{
			var expected = new NewAzureVmConfigCommand
			{
				Name = "SomeName"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmConfigCommand.NameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.Name));
		}

		[Test]
		public void CommandOperator_ImageNameIsEmpty_SetsNothing()
		{
			var expected = new NewAzureVmConfigCommand
			{
				ImageName = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmConfigCommand.ImageNameParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ImageNameIsSomething_SetsImageName()
		{
			var expected = new NewAzureVmConfigCommand
			{
				ImageName = "SomeName"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmConfigCommand.ImageNameParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.ImageName));
		}

		[Test]
		public void CommandOperator_InstanceSizeIsEmpty_SetsNothing()
		{
			var expected = new NewAzureVmConfigCommand
			{
				InstanceSize = AzureVirtualMachineSize.None
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmConfigCommand.InstanceSizeParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_InstanceSizeIsSomething_SetsInstanceSize()
		{
			var expected = new NewAzureVmConfigCommand
			{
				InstanceSize = AzureVirtualMachineSize.ExtraSmall,
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == NewAzureVmConfigCommand.InstanceSizeParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.InstanceSize));
		}
	}
}
