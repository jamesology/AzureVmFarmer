using System;
using System.Collections.Generic;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class MessageHandler : IMessageHandler
	{
		private readonly IDictionary<string, IMessageHandler> _handlers;

		public MessageHandler(IPowershellExecutor executor)
		{
			_handlers = new Dictionary<string, IMessageHandler>
			{
				{"Create", new SowMessageHandler(executor)},
				{"Delete", new ReapMessageHandler(executor)}
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