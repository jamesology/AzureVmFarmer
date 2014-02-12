using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureVmCommand : PowerShellCommand
	{
		private const string CommandName = "Get-AzureVM";
		public const string NameParameter = "Name";
		public const string ServiceNameParameter = "ServiceName";

		public string Name { get; set; }
		public string ServiceName { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(Name) == false)
			{
				result.Parameters.Add(NameParameter, Name);
			}

			if (String.IsNullOrWhiteSpace(ServiceName) == false)
			{
				result.Parameters.Add(ServiceNameParameter, ServiceName);
			}

			return result;
		}
	}
}
