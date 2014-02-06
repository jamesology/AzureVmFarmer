using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public abstract class PowerShellCommand
	{
		//TODO: Common Parameters

		protected abstract Command BuildCommand();

		public static implicit operator Command(PowerShellCommand command)
		{
			return command.BuildCommand();
		}
	}
}
