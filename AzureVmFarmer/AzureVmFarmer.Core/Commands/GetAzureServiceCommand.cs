using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureServiceCommand : PowerShellCommand
	{
		private const string CommandName = "Get-AzureService";
		private const string ServiceNameParameter = "ServiceName";

		public string ServiceName { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(ServiceName) == false)
			{
				result.Parameters.Add(ServiceNameParameter, ServiceName);
			}

			return result;
		}
	}
}