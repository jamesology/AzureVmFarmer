using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core.Messengers
{
	public interface IMessageHandler
	{
		void HandleMessage(BrokeredMessage message);
	}
}