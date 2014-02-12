using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class AddAzureDiskCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new AddAzureDiskCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_DiskNameIsEmpty_SetsNothing()
		{
			var expected = new AddAzureDiskCommand
			{
				DiskName = String.Empty
			};

			Command actual = expected;
			var diskNameParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDiskCommand.DiskNameParameter);

			Assert.That(diskNameParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_DiskNameIsSomething_SetsDiskName()
		{
			var expected = new AddAzureDiskCommand
			{
				DiskName = "SomeName"
			};

			Command actual = expected;
			var diskNameParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDiskCommand.DiskNameParameter);

			Assert.That(diskNameParameter, Is.Not.Null);
			Assert.That(diskNameParameter.Value, Is.EqualTo(expected.DiskName));
		}

		[Test]
		public void CommandOperator_LabelIsEmpty_SetsNothing()
		{
			var expected = new AddAzureDiskCommand
			{
				Label = String.Empty
			};

			Command actual = expected;
			var labelParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDiskCommand.LabelParameter);

			Assert.That(labelParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_LabelIsSomething_SetsLabel()
		{
			var expected = new AddAzureDiskCommand
			{
				Label = "SomeLabel"
			};

			Command actual = expected;
			var labelParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDiskCommand.LabelParameter);

			Assert.That(labelParameter, Is.Not.Null);
			Assert.That(labelParameter.Value, Is.EqualTo(expected.Label));
		}

		[Test]
		public void CommandOperator_MediaLocationIsEmpty_SetsNothing()
		{
			var expected = new AddAzureDiskCommand
			{
				MediaLocation = String.Empty
			};

			Command actual = expected;
			var mediaLocationParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDiskCommand.MediaLocationParameter);

			Assert.That(mediaLocationParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_MediaLocationIsSomething_SetsMediaLocation()
		{
			var expected = new AddAzureDiskCommand
			{
				MediaLocation = "SomeUrl"
			};

			Command actual = expected;
			var mediaLocationParameter = actual.Parameters.FirstOrDefault(x => x.Name == AddAzureDiskCommand.MediaLocationParameter);

			Assert.That(mediaLocationParameter, Is.Not.Null);
			Assert.That(mediaLocationParameter.Value, Is.EqualTo(expected.MediaLocation));
		}
	}
}
