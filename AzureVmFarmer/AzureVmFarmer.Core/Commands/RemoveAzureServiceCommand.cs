using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class RemoveAzureServiceCommand : PowerShellCommand
	{
		private const string CommandName = "Remove-AzureService";
		public const string ServiceNameParameter = "ServiceName";
		public const string ForceParameter = "Force";
		public const string DeleteAllParameter = "DeleteAll";

		public string ServiceName { get; set; }
		public bool Force { get; set; }
		public bool DeleteAll { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(ServiceName) == false)
			{
				result.Parameters.Add(ServiceNameParameter, ServiceName);
			}

			if (Force)
			{
				result.Parameters.Add(ForceParameter);
			}

			if (DeleteAll)
			{
				result.Parameters.Add(DeleteAllParameter);
			}

			return result;
		}
	}
}
