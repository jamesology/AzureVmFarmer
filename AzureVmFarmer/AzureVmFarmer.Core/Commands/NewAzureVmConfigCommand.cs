using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class NewAzureVmConfigCommand : PowerShellCommand
	{
		private const string CommandName = "New-AzureVMConfig";
		public const string NameParameter = "Name";
		public const string ImageNameParameter = "ImageName";
		public const string InstanceSizeParameter = "InstanceSize";

		public string Name { get; set; }
		public string ImageName { get; set; }
		public string InstanceSize { get; set; } //TODO: Make enum?

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(Name) == false)
			{
				result.Parameters.Add(NameParameter, Name);
			}

			if (String.IsNullOrWhiteSpace(ImageName) == false)
			{
				result.Parameters.Add(ImageNameParameter, ImageName);
			}

			if (String.IsNullOrWhiteSpace(InstanceSize) == false)
			{
				result.Parameters.Add(InstanceSizeParameter, InstanceSize);
			}

			return result;
		}
	}
}
