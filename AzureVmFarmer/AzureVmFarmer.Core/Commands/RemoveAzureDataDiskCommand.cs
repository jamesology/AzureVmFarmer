using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class RemoveAzureDataDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Remove-AzureDataDisk";
		public const string LogicalUnitNumberParameter = "LUN";
		
		public int? LogicalUnitNumber { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (LogicalUnitNumber.HasValue && (LogicalUnitNumber.Value >= 0))
			{
				result.Parameters.Add(LogicalUnitNumberParameter, LogicalUnitNumber);
			}

			return result;
		}
	}
}
