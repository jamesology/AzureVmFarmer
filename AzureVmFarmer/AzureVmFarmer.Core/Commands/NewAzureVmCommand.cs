using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class NewAzureVmCommand : PowerShellCommand
	{
		private const string CommandName = "New-AzureVM";
		public const string LocationParameter = "Location";
		public const string ServiceNameParameter = "ServiceName";
		public const string WaitForBootParameter = "WaitForBoot";
		public const string DeploymentNameParameter = "DeploymentName";

		public string Location { get; set; }
		public string ServiceName { get; set; }
		public bool WaitForBoot { get; set; }
		public string DeploymentName { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(Location) == false)
			{
				result.Parameters.Add(LocationParameter, Location);
			}

			if (String.IsNullOrWhiteSpace(ServiceName) == false)
			{
				result.Parameters.Add(ServiceNameParameter, ServiceName);
			}

			if (String.IsNullOrWhiteSpace(DeploymentName) == false)
			{
				result.Parameters.Add(DeploymentNameParameter, DeploymentName);
			}

			if (WaitForBoot)
			{
				result.Parameters.Add(WaitForBootParameter);
			}

			return result;
		}
	}
}