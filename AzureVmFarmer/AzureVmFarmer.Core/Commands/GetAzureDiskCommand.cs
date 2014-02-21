using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Get-AzureDisk";
		public const string DiskNameParameter = "DiskName";

		public string DiskName { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(DiskName) == false)
			{
				result.Parameters.Add(DiskNameParameter, DiskName);
			}

			return result;
		}
	}
}
