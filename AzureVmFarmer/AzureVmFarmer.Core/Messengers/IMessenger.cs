using AzureVmFarmer.Objects;

namespace AzureVmFarmer.Core.Messengers
{
	public interface IMessenger
	{
		void QueueCreateMessage(VirtualMachine virtualMachine);

		void QueueDeleteMessage();
	}
}
