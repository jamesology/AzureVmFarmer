using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class AddAzureProvisioningConfigCommand : PowerShellCommand
	{
		private const string CommandName = "Add-AzureProvisioningConfig";
		public const string WindowsParameter = "Windows";
		public const string PasswordParameter = "Password";
		public const string AdminUsernameParameter = "AdminUsername";
		public const string TimeZoneParameter = "TimeZone";

		public bool Windows { get; set; }
		public string AdminPassword { get; set; }
		public string AdminUsername { get; set; }
		public string TimeZone { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(AdminUsername) == false)
			{
				result.Parameters.Add(AdminUsernameParameter, AdminUsername);
			}

			if (String.IsNullOrWhiteSpace(AdminPassword) == false)
			{
				result.Parameters.Add(PasswordParameter, AdminPassword);
			}

			if (String.IsNullOrWhiteSpace(TimeZone) == false)
			{
				result.Parameters.Add(TimeZoneParameter, TimeZone);
			}

			if (Windows)
			{
				result.Parameters.Add(WindowsParameter);
			}

			return result;
		}
	}
}
