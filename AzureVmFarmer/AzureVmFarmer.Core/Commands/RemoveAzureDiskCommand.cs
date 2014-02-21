using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class RemoveAzureDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Remove-AzureDisk";
		public const string DiskNameParameter = "DiskName";
		public const string DeleteVhdParameter = "DeleteVHD";

		public string DiskName { get; set; }
		public bool DeleteVhd { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(DiskName) == false)
			{
				result.Parameters.Add(DiskNameParameter, DiskName);
			}

			if (DeleteVhd)
			{
				result.Parameters.Add(DeleteVhdParameter);
			}

			return result;
		}
	}
}
