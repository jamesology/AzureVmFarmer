using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class UpdateAzureVmCommand:PowerShellCommand
	{
		private const string CommandName = "Update-AzureVM";
		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			return result;
		}
	}
}
