using System;
using System.Reflection;
using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core
{
	public static class BrokeredMessageExtensions
	{
		private const string MessageType = "MessageType";

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

				message.Properties[name] = property.PropertyType.IsEnum
					? property.GetValue(thinger).ToString()
					: property.GetValue(thinger);
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
				var value = property.PropertyType.IsEnum
					? Enum.Parse(property.PropertyType, message.Properties[name].ToString())
					: message.Properties[name];

				property.SetValue(result, value);
			}

			return result;
		}
	}
}