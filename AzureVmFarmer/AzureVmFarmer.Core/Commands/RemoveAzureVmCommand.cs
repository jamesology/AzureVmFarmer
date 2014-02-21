using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class RemoveAzureVmCommand : PowerShellCommand
	{
		private const string CommandName = "Remove-AzureVM";
		public const string ServiceNameParameter = "ServiceName";
		public const string NameParameter = "Name";

		public string ServiceName { get; set; }
		public string Name { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(ServiceName) == false)
			{
				result.Parameters.Add(ServiceNameParameter, ServiceName);
			}

			if (String.IsNullOrWhiteSpace(Name) == false)
			{
				result.Parameters.Add(NameParameter, Name);
			}

			return result;
		}
	}
}
