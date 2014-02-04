using System;
using NUnit.Framework;

namespace AzureVmFarmer.Objects.Tests
{
	[TestFixture]
	public class VirtualMachineTest
	{
		[Test]
		public void IsValid_ValueIsNull_ReturnsFalse()
		{
			Assert.That(VirtualMachine.IsValid(null), Is.False);
		}

		[Test]
		public void IsValid_ValueMissingName_ReturnsFalse()
		{
			var value = new VirtualMachine
			{
				Name = String.Empty,
				Size = "size",
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone"
			};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueMissingSize_ReturnsFalse()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = String.Empty,
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone"
			};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueMissingAdminUserName_ReturnsFalse()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = "size",
				AdminUserName = String.Empty,
				AdminPassword = "password",
				TimeZone = "timeZone"
			};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueMissingAdminPassword_ReturnsFalse()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = "size",
				AdminUserName = "admin",
				AdminPassword = String.Empty,
				TimeZone = "timeZone"
			};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueMissingTimeZone_ReturnsFalse()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = "size",
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = String.Empty
			};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueMissingLocation_ReturnsFalse()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = "size",
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone",
				Location = String.Empty
			};

			Assert.That(VirtualMachine.IsValid(value), Is.False);
		}

		[Test]
		public void IsValid_ValueIsValid_ReturnsTrue()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = "size",
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone",
				Location = "location"
			};

			Assert.That(VirtualMachine.IsValid(value), Is.True);
		}
	}
}
