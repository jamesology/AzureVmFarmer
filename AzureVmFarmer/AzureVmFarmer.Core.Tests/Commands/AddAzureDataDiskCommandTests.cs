using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class AddAzureDataDiskCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new AddAzureDataDiskCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_DiskNameIsEmpty_SetsNothing()
		{
			var expected = new AddAzureDataDiskCommand
			{
				DiskName = String.Empty
			};

			Command actual = expected;
			var diskNameParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDataDiskCommand.DiskNameParameter);

			Assert.That(diskNameParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_DiskNameIsSomething_SetsDiskName()
		{
			var expected = new AddAzureDataDiskCommand
			{
				DiskName = "SomeDisk"
			};

			Command actual = expected;
			var diskNameParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDataDiskCommand.DiskNameParameter);

			Assert.That(diskNameParameter, Is.Not.Null);
			Assert.That(diskNameParameter.Value, Is.EqualTo("SomeDisk"));
		}

		[Test]
		public void CommandOperator_ImportIsFalse_SetsNothing()
		{
			var expected = new AddAzureDataDiskCommand
			{
				Import = false
			};

			Command actual = expected;
			var importParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDataDiskCommand.ImportParameter);

			Assert.That(importParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_ImportIsTrue_SetsImport()
		{
			var expected = new AddAzureDataDiskCommand
			{
				Import = true
			};

			Command actual = expected;
			var importParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDataDiskCommand.ImportParameter);

			Assert.That(importParameter, Is.Not.Null);
		}

		[Test]
		public void CommandOperator_LogicalUnitNumberIsNull_SetsNothing()
		{
			var expected = new AddAzureDataDiskCommand
			{
				LogicalUnitNumber = null
			};

			Command actual = expected;
			var logicalUnitNumberParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDataDiskCommand.LogicalUnitNumberParameter);

			Assert.That(logicalUnitNumberParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_LogicalUnitNumberIsSomething_SetsLogicalUnitNumber()
		{
			var expected = new AddAzureDataDiskCommand
			{
				LogicalUnitNumber = 1
			};

			Command actual = expected;
			var logicalUnitNumberParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDataDiskCommand.LogicalUnitNumberParameter);

			Assert.That(logicalUnitNumberParameter, Is.Not.Null);
			Assert.That(logicalUnitNumberParameter.Value, Is.EqualTo(1));
		}
	}
}
