using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class AddAzureDiskCommand : PowerShellCommand
	{
		private const string CommandName = "Add-AzureDisk";
		public const string DiskNameParameter = "DiskName";
		public const string LabelParameter = "Label";
		public const string MediaLocationParameter = "MediaLocation";

		public string DiskName { get; set; }
		public string Label { get; set; }
		public string MediaLocation { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(DiskName) == false)
			{
				result.Parameters.Add(DiskNameParameter, DiskName);
			}

			if (String.IsNullOrWhiteSpace(Label) == false)
			{
				result.Parameters.Add(LabelParameter, Label);
			}

			if (String.IsNullOrWhiteSpace(MediaLocation) == false)
			{
				result.Parameters.Add(MediaLocationParameter, MediaLocation);
			}

			return result;
		}
	}
}
