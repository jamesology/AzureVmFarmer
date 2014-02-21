using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureDataDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Get-AzureDataDisk";

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			return result;
		}
	}
}
