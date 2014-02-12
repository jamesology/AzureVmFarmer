using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureStorageBlobCopyStateCommand:PowerShellCommand
	{
		private const string CommandName = "Get-AzureStorageBlobCopyState";
		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			return result;
		}
	}
}
