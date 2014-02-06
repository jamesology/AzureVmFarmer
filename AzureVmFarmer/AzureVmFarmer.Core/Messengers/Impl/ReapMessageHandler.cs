using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class ReapMessageHandler : IMessageHandler
	{
		public void HandleMessage(BrokeredMessage message)
		{
			using (var runspace = RunspaceFactory.CreateRunspace())
			{
				runspace.Open();

				var virtualMachine = message.GetObject<VirtualMachine>();

				//var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
				//var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
				//var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
				//var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

				//TODO: find a subscription?

				//TODO: get storage account?

				DeleteExistingArtifacts(runspace, virtualMachine);

				runspace.Close();
			}
		}

		private static void DeleteExistingArtifacts(Runspace runspace, VirtualMachine virtualMachine)
		{
			if (AzureServiceExists(runspace, virtualMachine))
			{
				DeleteExistingService(runspace, virtualMachine);
			}
		}

		private static bool AzureServiceExists(Runspace runspace, VirtualMachine virtualMachine)
		{
			bool result;

			using (var pipeline = runspace.CreatePipeline())
			{
				var getAzureServiceCommand = new GetAzureServiceCommand
				{
					ServiceName = virtualMachine.Name,
					ErrorAction = "Ignore"
				};

				pipeline.Commands.Add(getAzureServiceCommand);

				var results = pipeline.Execute();

				result = results.Any();
			}

			return result;
		}

		private static void DeleteExistingService(Runspace runspace, VirtualMachine virtualMachine)
		{
			using (var pipeline = runspace.CreatePipeline())
			{
				var removeAzureServiceCommand = new RemoveAzureServiceCommand
				{
					ServiceName = virtualMachine.Name,
					Force = true,
					DeleteAll = true
				};

				pipeline.Commands.Add(removeAzureServiceCommand);

				pipeline.Execute();
			}
		}
	}
}