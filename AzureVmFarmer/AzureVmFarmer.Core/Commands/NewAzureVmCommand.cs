using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class NewAzureVmCommand : PowerShellCommand
	{
		private const string CommandName = "New-AzureVM";
		private const string LocationParameter = "Location";
		private const string ServiceNameParameter = "ServiceName";
		private const string WaitForBootParameter = "WaitForBoot";

		public string Location { get; set; }
		public string ServiceName { get; set; }
		public bool WaitForBoot { get; set; }

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

			if (WaitForBoot)
			{
				result.Parameters.Add(WaitForBootParameter);
			}

			return result;
		}
	}
}