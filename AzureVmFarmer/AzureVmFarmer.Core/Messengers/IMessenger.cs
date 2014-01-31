using AzureVmFarmer.Objects;

namespace AzureVmFarmer.Core.Messengers
{
	public interface IMessenger
	{
		void QueueMessage();
	}
}
