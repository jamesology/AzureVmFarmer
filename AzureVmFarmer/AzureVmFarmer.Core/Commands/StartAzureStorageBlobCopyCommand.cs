using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class StartAzureStorageBlobCopyCommand : PowerShellCommand
	{
		private const string CommandName = "Start-AzureStorageBlobCopy";
		public const string SrcBlobParameter = "SrcBlob";
		public const string SrcContainerParameter = "SrcContainer";
		public const string DestBlobParameter = "DestBlob";
		public const string DestContainerParameter = "DestContainer";
		public const string ForceParameter = "Force";

		public string SrcBlob { get; set; }
		public string SrcContainer { get; set; }
		public string DestBlob { get; set; }
		public string DestContainer { get; set; }
		public bool Force { get; set; }

		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			if (String.IsNullOrWhiteSpace(SrcBlob) == false)
			{
				result.Parameters.Add(SrcBlobParameter, SrcBlob);
			}

			if (String.IsNullOrWhiteSpace(SrcContainer) == false)
			{
				result.Parameters.Add(SrcContainerParameter, SrcContainer);
			}

			if (String.IsNullOrWhiteSpace(DestBlob) == false)
			{
				result.Parameters.Add(DestBlobParameter, DestBlob);
			}

			if (String.IsNullOrWhiteSpace(DestContainer) == false)
			{
				result.Parameters.Add(DestContainerParameter, DestContainer);
			}

			if (Force)
			{
				result.Parameters.Add(ForceParameter);
			}

			return result;
		}
	}
}
