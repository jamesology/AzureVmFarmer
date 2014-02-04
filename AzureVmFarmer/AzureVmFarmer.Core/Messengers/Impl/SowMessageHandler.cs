using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Compute;
using Microsoft.WindowsAzure.Management.Compute.Models;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class SowMessageHandler : IMessageHandler
	{
		public void HandleMessage(BrokeredMessage message)
		{
			var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
			var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
			var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
			var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

			using (var manager = CloudContext.Clients.CreateComputeManagementClient(credentials))
			{
				var virtualMachine = message.GetObject<VirtualMachine>();

				//TODO: find a subscription

				//TODO: get storage account

				//TODO: generate credentials

				DeleteExistingMachine(manager, virtualMachine.Name);

				var serviceCreateStatus = CreateService(manager, virtualMachine.Name, virtualMachine.Location);

				var createStatus = CreateVirtualMachine(manager, virtualMachine);

				//TODO: attach disk?
			}
		}

		private static void DeleteExistingMachine(IComputeManagementClient manager, string virtualMachineName)
		{
			var getStatus = manager.VirtualMachines.Get(virtualMachineName, virtualMachineName, virtualMachineName);

			if (getStatus.StatusCode == HttpStatusCode.OK)
			{
				manager.HostedServices.Delete(virtualMachineName);
			}
		}

		private static OperationResponse CreateService(IComputeManagementClient manager, string serviceName, string location)
		{
			var serviceParameters = new HostedServiceCreateParameters
			{
				Label = serviceName,
				Location = location,
				ServiceName = serviceName,
			};

			var serviceCreateStatus = manager.HostedServices.Create(serviceParameters);

			return serviceCreateStatus;
		}

		private static ComputeOperationStatusResponse CreateVirtualMachine(IComputeManagementClient manager, VirtualMachine virtualMachine)
		{
			var configurations = new[]
			{
				new ConfigurationSet
				{
					AdminPassword = virtualMachine.AdminPassword,
					AdminUserName = virtualMachine.AdminUserName,
					ComputerName = virtualMachine.Name,
					ConfigurationSetType = "Windows",
					TimeZone = virtualMachine.TimeZone
				}
			};

			var osDrive = new OSVirtualHardDisk
			{
				SourceImageName = "NuMatsRunnerBase"
			};

			var vmParameters = new VirtualMachineCreateParameters
			{
				//TODO: Add data virtual hard disks
				ConfigurationSets = configurations,
				RoleName = virtualMachine.Name,
				RoleSize = virtualMachine.Size,
				OSVirtualHardDisk = osDrive
			};

			var createStatus = manager.VirtualMachines.Create(virtualMachine.Name, virtualMachine.Name, vmParameters);

			return createStatus;
		}
	}
}