using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class ReapMessageHandler : IMessageHandler
	{
		public void HandleMessage(BrokeredMessage message)
		{
			throw new System.NotImplementedException();
		}
	}
}