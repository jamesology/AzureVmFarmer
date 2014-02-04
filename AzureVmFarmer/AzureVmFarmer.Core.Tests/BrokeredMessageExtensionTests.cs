using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests
{
	[TestFixture]
	public class BrokeredMessageExtensionTests
	{
		[Test]
		public void SetAndGetMessageType_MessageTypeIsPersisted()
		{
			var message = new BrokeredMessage();
			const string expected = "MessageType";

			message.SetMessageType(expected);

			var actual = message.GetMessageType();

			Assert.That(actual, Is.EqualTo(expected));
		}

		[Test]
		public void SetAndGetVirtualMachine_NameIsPersisted()
		{
			var message = new BrokeredMessage();
			var expected = new VirtualMachine
			{
				Name = "Name",
				AdminUserName = "AdminUserName",
				AdminPassword = "AdminPassword",
				Location = "Location",
				Size = "Size",
				TimeZone = "TimeZone"
			};

			message.SetObject(expected);

			var actual = message.GetObject<VirtualMachine>();

			Assert.That(actual.Name, Is.EqualTo(expected.Name));
		}

		[Test]
		public void SetAndGetVirtualMachine_AdminUserNameIsPersisted()
		{
			var message = new BrokeredMessage();
			var expected = new VirtualMachine
			{
				Name = "Name",
				AdminUserName = "AdminUserName",
				AdminPassword = "AdminPassword",
				Location = "Location",
				Size = "Size",
				TimeZone = "TimeZone"
			};

			message.SetObject(expected);

			var actual = message.GetObject<VirtualMachine>();

			Assert.That(actual.AdminUserName, Is.EqualTo(expected.AdminUserName));
		}

		[Test]
		public void SetAndGetVirtualMachine_AdminPasswordIsPersisted()
		{
			var message = new BrokeredMessage();
			var expected = new VirtualMachine
			{
				Name = "Name",
				AdminUserName = "AdminUserName",
				AdminPassword = "AdminPassword",
				Location = "Location",
				Size = "Size",
				TimeZone = "TimeZone"
			};

			message.SetObject(expected);

			var actual = message.GetObject<VirtualMachine>();

			Assert.That(actual.AdminPassword, Is.EqualTo(expected.AdminPassword));
		}

		[Test]
		public void SetAndGetVirtualMachine_LocationIsPersisted()
		{
			var message = new BrokeredMessage();
			var expected = new VirtualMachine
			{
				Name = "Name",
				AdminUserName = "AdminUserName",
				AdminPassword = "AdminPassword",
				Location = "Location",
				Size = "Size",
				TimeZone = "TimeZone"
			};

			message.SetObject(expected);

			var actual = message.GetObject<VirtualMachine>();

			Assert.That(actual.Location, Is.EqualTo(expected.Location));
		}

		[Test]
		public void SetAndGetVirtualMachine_SizeIsPersisted()
		{
			var message = new BrokeredMessage();
			var expected = new VirtualMachine
			{
				Name = "Name",
				AdminUserName = "AdminUserName",
				AdminPassword = "AdminPassword",
				Location = "Location",
				Size = "Size",
				TimeZone = "TimeZone"
			};

			message.SetObject(expected);

			var actual = message.GetObject<VirtualMachine>();

			Assert.That(actual.Size, Is.EqualTo(expected.Size));
		}

		[Test]
		public void SetAndGetVirtualMachine_TimeZoneIsPersisted()
		{
			var message = new BrokeredMessage();
			var expected = new VirtualMachine
			{
				Name = "Name",
				AdminUserName = "AdminUserName",
				AdminPassword = "AdminPassword",
				Location = "Location",
				Size = "Size",
				TimeZone = "TimeZone"
			};

			message.SetObject(expected);

			var actual = message.GetObject<VirtualMachine>();

			Assert.That(actual.TimeZone, Is.EqualTo(expected.TimeZone));
		}
	}
}
