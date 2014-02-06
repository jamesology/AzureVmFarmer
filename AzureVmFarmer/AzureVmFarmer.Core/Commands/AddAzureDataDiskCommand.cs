using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class AddAzureDataDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Add-AzureDataDisk";
		private const string ImportParameter = "Import";
		private const string DiskNameParameter = "DiskName";
		private const string LogicalUnitNumberParameter = "LUN";

		public bool Import { get; set; }
		public string DiskName { get; set; }
		public int LogicalUnitNumber { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (Import)
			{
				result.Parameters.Add(ImportParameter);
			}

			if (String.IsNullOrWhiteSpace(DiskName) == false)
			{
				result.Parameters.Add(DiskNameParameter, DiskName);
			}

			if (LogicalUnitNumber >= 0)
			{
				result.Parameters.Add(LogicalUnitNumberParameter, LogicalUnitNumber);
			}

			return result;
		}
	}
}
