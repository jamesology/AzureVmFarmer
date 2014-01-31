using System;
using NUnit.Framework;

namespace AzureVmFarmer.Objects.Tests
{
	[TestFixture]
	public class VirtualMachineTest
	{
		[Test]
		public void IsValid_ValueMissingName_ReturnsFalse()
		{
			var value = new VirtualMachine {Name = String.Empty};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueIsValid_ReturnsTrue()
		{
			var value = new VirtualMachine {Name = "New Machine"};

			Assert.That(VirtualMachine.IsValid(value), Is.True);
		}
	}
}
