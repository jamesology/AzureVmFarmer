using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class UpdateAzureVmCommandTests
	{
		[Test]
		public void CommandOperator_DefaultValues_SetsNothing()
		{
			var expected = new UpdateAzureVmCommand();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}
	}
}
