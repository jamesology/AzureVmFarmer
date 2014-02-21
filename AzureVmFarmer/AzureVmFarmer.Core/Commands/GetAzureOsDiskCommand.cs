using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureOsDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Get-AzureOsDisk";

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			return result;
		}
	}
}
