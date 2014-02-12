using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class StartAzureStorageBlobCopyCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new StartAzureStorageBlobCopyCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_SrcBlobIsEmpty_SetsNothing()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				SrcBlob = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.SrcBlobParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_SrcBlobIsSomething_SetsSrcBlob()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				SrcBlob = "SomeBlob"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.SrcBlobParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.SrcBlob));
		}

		[Test]
		public void CommandOperator_SrcContainerIsEmpty_SetsNothing()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				SrcContainer = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.SrcContainerParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_SrcContainerIsSomething_SetsSrcContainer()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				SrcContainer = "SomeContainer"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.SrcContainerParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.SrcContainer));
		}

		[Test]
		public void CommandOperator_DestBlobIsEmpty_SetsNothing()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				DestBlob = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.DestBlobParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_DestBlobIsSomething_SetsDestBlob()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				DestBlob = "SomeBlob"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.DestBlobParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.DestBlob));
		}

		[Test]
		public void CommandOperator_DestContainerIsEmpty_SetsNothing()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				DestContainer = String.Empty
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.DestContainerParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_DestContainerIsSomething_SetsDestContainer()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				DestContainer = "SomeContainer"
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.DestContainerParameter);

			Assert.That(actual, Is.Not.Null);
			Assert.That(actual.Value, Is.EqualTo(expected.DestContainer));
		}

		[Test]
		public void CommandOperator_ForceIsFalse_SetsNothing()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				Force = false
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.ForceParameter);

			Assert.That(actual, Is.Null);
		}

		[Test]
		public void CommandOperator_ForceIsTrue_SetsForce()
		{
			var expected = new StartAzureStorageBlobCopyCommand
			{
				Force = true
			};

			Command command = expected;
			var actual = command.Parameters.FirstOrDefault(x => x.Name == StartAzureStorageBlobCopyCommand.ForceParameter);

			Assert.That(actual, Is.Not.Null);
		}
	}
}
