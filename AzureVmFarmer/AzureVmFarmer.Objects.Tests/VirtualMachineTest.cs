using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
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
				Size = AzureVirtualMachineSize.ExtraSmall,
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
				Size = AzureVirtualMachineSize.None,
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
				Size = AzureVirtualMachineSize.ExtraSmall,
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
				Size = AzureVirtualMachineSize.ExtraSmall,
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
				Size = AzureVirtualMachineSize.ExtraSmall,
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
				Size = AzureVirtualMachineSize.ExtraSmall,
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
				Size = AzureVirtualMachineSize.ExtraSmall,
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone",
				Location = "location"
			};

			Assert.That(VirtualMachine.IsValid(value), Is.True);
		}

		[Test]
		public void VirtualMachine_IsJsonSerializable()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = AzureVirtualMachineSize.ExtraSmall,
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone",
				Location = "location"
			};

			var serializedVirtualMachine = new StringBuilder();

			var serializer = new JsonSerializer();
			var writer = new StringWriter(serializedVirtualMachine);

			serializer.Serialize(writer, value);

			var reader = new StringReader(serializedVirtualMachine.ToString());
			var jsonReader = new JsonTextReader(reader);

			var result = serializer.Deserialize<VirtualMachine>(jsonReader);

			Assert.That(VirtualMachine.IsValid(result), Is.True);
		}

		[Test]
		public void VirtualMachine_IsXmlSerializable()
		{
			var value = new VirtualMachine
			{
				Name = "name",
				Size = AzureVirtualMachineSize.ExtraSmall,
				AdminUserName = "admin",
				AdminPassword = "password",
				TimeZone = "timeZone",
				Location = "location"
			};

			var serializedVirtualMachine = new StringBuilder();

			var serializer = new XmlSerializer(typeof(VirtualMachine));
			var writer = new StringWriter(serializedVirtualMachine);

			serializer.Serialize(writer, value);

			var reader = new StringReader(serializedVirtualMachine.ToString());

			var result = serializer.Deserialize(reader);

			Assert.That(result, Is.TypeOf<VirtualMachine>());
			Assert.That(VirtualMachine.IsValid((VirtualMachine) result), Is.True);
		}
	}
}
