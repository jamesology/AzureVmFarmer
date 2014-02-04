using System;
using System.Reflection;
using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core
{
	public static class BrokeredMessageExtensions
	{
		private const string MessageType = "MessageType";

		private const string VirtualMachineName = "VirtualMachine.Name";

		public static void SetMessageType(this BrokeredMessage message, string messageType)
		{
			message.Properties[MessageType] = messageType;
		}

		public static string GetMessageType(this BrokeredMessage message)
		{
			var result = Convert.ToString(message.Properties[MessageType]);

			return result;
		}

		public static void SetObject<T>(this BrokeredMessage message, T thinger)
		{
			var type = typeof (T);
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var property in properties)
			{
				var name = String.Format("{0}.{1}", type.Name, property.Name);

				message.Properties[name] = property.GetValue(thinger);
			}
		}

		public static T GetObject<T>(this BrokeredMessage message)
			where T : new ()
		{
			var result = new T();
			var type = typeof(T);
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var property in properties)
			{
				var name = String.Format("{0}.{1}", type.Name, property.Name);

				property.SetValue(result, message.Properties[name]);
			}

			return result;
		}

		/*public static void SetVirtualMachine(this BrokeredMessage message, VirtualMachine virtualMachine)
		{
			message.Properties[VirtualMachineName] = virtualMachine.Name;
		}

		public static VirtualMachine GetVirtualMachine(this BrokeredMessage message)
		{
			var name = Convert.ToString(message.Properties[VirtualMachineName]);

			var result = new VirtualMachine
			{
				Name = name
			};

			return result;
		}*/
	}
}