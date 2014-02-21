using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Authentication.ExtendedProtection;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

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

				var serviceName = CloudConfigurationManager.GetSetting("ServiceName");

				//var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
				//var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
				//var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
				//var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

				//TODO: find a subscription?

				//TODO: get storage account?

				DeleteExistingArtifacts(_executor, virtualMachine, serviceName);
			}
			else
			{
				throw new ArgumentException("Invalid Message Type.", "message");
			}
		}

		private static void DeleteExistingArtifacts(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName)
		{
			if (AzureVirtualMachineExists(executor, virtualMachine, serviceName))
			{
				var osDisk = GetAttachedOsDisks(executor, virtualMachine, serviceName);

				var dataDisks = GetAttachedDataDisks(executor, virtualMachine, serviceName);

				foreach (var dataDisk in dataDisks)
				{
					DeleteDataDisk(executor, virtualMachine, serviceName, dataDisk.Key, dataDisk.Value);
				}

				DeleteExistingVirtualMachine(executor, virtualMachine, serviceName);

				DeleteOsDisk(executor, osDisk);
			}
		}

		private static bool AzureVirtualMachineExists(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName)
		{
			var getAzureVmCommand = new GetAzureVmCommand
			{
				ServiceName = serviceName,
				Name = virtualMachine.Name
			};

			var results = executor.Execute(getAzureVmCommand);

			var result = results.Any();

			return result;
		}

		private static string GetAttachedOsDisks(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName)
		{
			var getAzureVmCommand = new GetAzureVmCommand
			{
				ServiceName = serviceName,
				Name = virtualMachine.Name
			};

			var getAzureOsDiskCommand = new GetAzureOsDiskCommand();

			var osDisk = executor.Execute(getAzureVmCommand, getAzureOsDiskCommand)
				.Select(x => x.Properties["DiskName"].Value)
				.Cast<string>()
				.FirstOrDefault();

			return osDisk;
		}

		private static IEnumerable<KeyValuePair<string, int>> GetAttachedDataDisks(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName)
		{
			var getAzureVmCommand = new GetAzureVmCommand
			{
				ServiceName = serviceName,
				Name = virtualMachine.Name
			};

			var getAzureDataDiskCommand = new GetAzureDataDiskCommand();

			var dataDisks = executor.Execute(getAzureVmCommand, getAzureDataDiskCommand)
				.ToDictionary(x => (string) x.Properties["DiskName"].Value, y => (int) y.Properties["LUN"].Value);
				//.Select(x => x.Properties["DiskName"].Value)
				//.Cast<string>();

			return dataDisks;
		}

		private static void DeleteDataDisk(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName, string diskName, int logicalUnitNumber)
		{
			DetachDiskFromVirtualMachine(executor, virtualMachine, serviceName, logicalUnitNumber);

			WaitForDiskToBeFree(executor, diskName);

			DeleteDisk(executor, diskName);
		}

		private static void DetachDiskFromVirtualMachine(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName, int logicalUnitNumber)
		{
			var getAzureVmCommand = new GetAzureVmCommand
			{
				ServiceName = serviceName,
				Name = virtualMachine.Name
			};

			var removeAzureDataDiskCommand = new RemoveAzureDataDiskCommand
			{
				LogicalUnitNumber = logicalUnitNumber
			};

			var updateAzureVmCommand = new UpdateAzureVmCommand();

			executor.Execute(getAzureVmCommand, removeAzureDataDiskCommand, updateAzureVmCommand);
		}

		private static void WaitForDiskToBeFree(IPowershellExecutor executor, string diskName)
		{
			var diskAvailable = false;

			do
			{
				var getAzureDiskCommand = new GetAzureDiskCommand
				{
					DiskName = diskName
				};

				var availability = executor.Execute(getAzureDiskCommand)
					.Select(x => x.Properties["AttachedTo"].Value)
					.FirstOrDefault();

				if (availability == null)
				{
					diskAvailable = true;
				}
			} while (diskAvailable == false);
		}

		private static void DeleteDisk(IPowershellExecutor executor, string diskName)
		{
			var removeAzureDiskCommand = new RemoveAzureDiskCommand
			{
				DiskName = diskName,
				DeleteVhd = true
			};

			executor.Execute(removeAzureDiskCommand);
		}

		private static void DeleteExistingVirtualMachine(IPowershellExecutor executor, VirtualMachine virtualMachine, string serviceName)
		{
			var removeAzureVmCommand = new RemoveAzureVmCommand
			{
				ServiceName = serviceName,
				Name = virtualMachine.Name
			};

			executor.Execute(removeAzureVmCommand);
		}

		private static void DeleteOsDisk(IPowershellExecutor executor, string osDisk)
		{
			WaitForDiskToBeFree(executor, osDisk);

			DeleteDisk(executor, osDisk);
		}
	}
}