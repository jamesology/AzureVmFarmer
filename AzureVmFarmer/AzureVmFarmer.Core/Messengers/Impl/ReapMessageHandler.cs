using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class ReapMessageHandler : IMessageHandler
	{
		private readonly IPowershellExecutor _executor;

		public ReapMessageHandler(IPowershellExecutor executor)
		{
			_executor = executor;
		}

		public void HandleMessage(BrokeredMessage message)
		{
			if (message.GetMessageType() == "Delete")
			{
				var virtualMachine = message.GetObject<VirtualMachine>();

				//var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
				//var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
				//var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
				//var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

				//TODO: find a subscription?

				//TODO: get storage account?

				DeleteExistingArtifacts(_executor, virtualMachine);
			}
			else
			{
				throw new ArgumentException("Invalid Message Type.", "message");
			}
		}

		private static void DeleteExistingArtifacts(IPowershellExecutor executor, VirtualMachine virtualMachine)
		{
			if (AzureServiceExists(executor, virtualMachine))
			{
				DeleteExistingService(executor, virtualMachine);
			}
		}

		private static bool AzureServiceExists(IPowershellExecutor executor, VirtualMachine virtualMachine)
		{
			var getAzureServiceCommand = new GetAzureServiceCommand
			{
				ServiceName = virtualMachine.Name,
				ErrorAction = ErrorAction.Ignore
			};

			var results = executor.Execute(getAzureServiceCommand);

			var result = results.Any();

			return result;
		}

		private static void DeleteExistingService(IPowershellExecutor executor, VirtualMachine virtualMachine)
		{
			var removeAzureServiceCommand = new RemoveAzureServiceCommand
			{
				ServiceName = virtualMachine.Name,
				Force = true,
				DeleteAll = true
			};

			executor.Execute(removeAzureServiceCommand);
		}
	}
}