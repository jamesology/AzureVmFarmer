using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public class GetAzureStorageBlobCopyStateCommand:PowerShellCommand
	{
		private const string CommandName = "Get-AzureStorageBlobCopyState";
		protected override Command BuildCommand()
		{
			var result = new Command(CommandName);

			return result;
		}
	}

	public class StartAzureStorageBlobCopyCommand : PowerShellCommand
	{
		private const string CommandName = "Start-AzureStorageBlobCopy";
		private const string SrcBlobParameter = "SrcBlob";
		private const string SrcContainerParameter = "SrcContainer";
		private const string DestBlobParameter = "DestBlob";
		private const string DestContainerParameter = "DestContainer";
		private const string ForceParameter = "Force";

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
