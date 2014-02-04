using System;
using System.Collections.Generic;
using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class MessageHandler : IMessageHandler
	{
		private readonly IDictionary<string, IMessageHandler> _handlers;

		public MessageHandler()
		{
			_handlers = new Dictionary<string, IMessageHandler>
			{
				{"Create", new SowMessageHandler()}
			};
		}

		public void HandleMessage(BrokeredMessage message)
		{
			IMessageHandler handler;
			if (_handlers.TryGetValue(message.GetMessageType(), out handler))
			{
				handler.HandleMessage(message);
			}
			else
			{
				throw new ArgumentException("Shit is broke!", "message");
			}
		}
	}
}