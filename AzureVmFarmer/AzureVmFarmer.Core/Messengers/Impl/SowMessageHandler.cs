using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Threading;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Core.PowershellCommandExecutor;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class SowMessageHandler : IMessageHandler
	{
		private readonly IPowershellExecutor _executor;

		public SowMessageHandler(IPowershellExecutor executor)
		{
			_executor = executor;
		}

		public void HandleMessage(BrokeredMessage message)
		{
			if (message.GetMessageType() == "Create")
			{
				var virtualMachine = message.GetObject<VirtualMachine>();
				var imageName = CloudConfigurationManager.GetSetting("VirtualMachineBaseImageName");
				var dataDiskBase = CloudConfigurationManager.GetSetting("DataDiskName");
				var dataDiskName = String.Format("{0}-{1}", dataDiskBase, virtualMachine.Name);
				var sourceVhdName = String.Format("{0}.vhd", dataDiskBase);
				var vhdContainerName = CloudConfigurationManager.GetSetting("VhdContainerName"); //TODO: does this work with multiple storage accounts?

				//var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
				//var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
				//var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
				//var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

				//TODO: find a subscription?

				//TODO: get storage account?

				if (AzureVmExists(_executor, virtualMachine) == false)
				{
					CreateNewVirtualMachine(_executor, virtualMachine, imageName, dataDiskName, sourceVhdName, vhdContainerName);
				}
			}
			else
			{
				throw new ArgumentException("Invalid Message Type.", "message");
			}
		}

		private static bool AzureVmExists(IPowershellExecutor executor, VirtualMachine virtualMachine)
		{
			var getAzureVmCommand = new GetAzureVmCommand
			{
				Name = virtualMachine.Name
			};

			var results = executor.Execute(getAzureVmCommand);

			var result = results.Any();

			return result;
		}

		private static void CreateNewVirtualMachine(IPowershellExecutor executor, VirtualMachine virtualMachine, string imageName, string dataDiskName, string sourceVhdName, string vhdContainerName)
		{
			PrepareAzureDrive(executor, dataDiskName, sourceVhdName, vhdContainerName);

			ProvisionVirtualMachine(executor, virtualMachine, imageName);

			AttachAzureDrive(executor, virtualMachine, dataDiskName);
		}

		private static void PrepareAzureDrive(IPowershellExecutor executor, string dataDiskName, string sourceVhdName, string vhdContainerName)
		{
			CopyVirtualHardDrive(executor, dataDiskName, sourceVhdName, vhdContainerName);

			CreateDisk(executor, dataDiskName);
		}

		private static void ProvisionVirtualMachine(IPowershellExecutor executor, VirtualMachine virtualMachine, string imageName)
		{
			var newAzureVmConfigCommand = new NewAzureVmConfigCommand
			{
				Name = virtualMachine.Name,
				ImageName = imageName,
				InstanceSize = virtualMachine.Size
			};

			var addAzureProvisioningConfigCommand = new AddAzureProvisioningConfigCommand
			{
				Windows = true,
				AdminPassword = virtualMachine.AdminPassword,
				AdminUsername = virtualMachine.AdminUserName,
				TimeZone = virtualMachine.TimeZone
			};

			var newAzureVmCommand = new NewAzureVmCommand
			{
				Location = virtualMachine.Location,
				ServiceName = virtualMachine.Name,
				WaitForBoot = false
			};

			var results = executor.Execute(newAzureVmConfigCommand, addAzureProvisioningConfigCommand, newAzureVmCommand);
		}

		private static void AttachAzureDrive(IPowershellExecutor executor, VirtualMachine virtualMachine, string dataDiskName)
		{
			var getAzureVm = new GetAzureVmCommand
			{
				Name = virtualMachine.Name,
				ServiceName = virtualMachine.Name
			};

			var addAzureDataDisk = new AddAzureDataDiskCommand
			{
				Import = true,
				DiskName = dataDiskName,
				LogicalUnitNumber = 0
			};

			var updateAzureVm = new UpdateAzureVmCommand();

			var result = executor.Execute(getAzureVm, addAzureDataDisk, updateAzureVm);
		}

		private static void CopyVirtualHardDrive(IPowershellExecutor executor, string dataDiskName, string sourceVhdName, string vhdContainerName)
		{
			var azureStorageBlobCopyCommand = new StartAzureStorageBlobCopyCommand
			{
				SrcBlob = sourceVhdName,
				SrcContainer = vhdContainerName,
				DestContainer = vhdContainerName,
				DestBlob = String.Format("{0}.vhd", dataDiskName),
				Force = true
			};

			var results = executor.Execute(azureStorageBlobCopyCommand);

			if (results.Any())
			{
				var copyPending = false;

				var getAzureStorageBlobCopyStateCommand = new GetAzureStorageBlobCopyStateCommand();

				do
				{
					var copyResult = executor.Execute(results, getAzureStorageBlobCopyStateCommand)
						.OfType<PSObject>()
						.Select(x => x.BaseObject)
						.FirstOrDefault();

					if (copyResult != null)
					{
						var type = copyResult.GetType();

						var property = type.GetProperty("Status", BindingFlags.Instance | BindingFlags.Public);

						var value = String.Format("{0}", property.GetValue(copyResult));

						copyPending = (value == "Pending");
					}

					if (copyPending)
					{
						Thread.Sleep(10);
					}
				} while (copyPending);
			}

		}

		private static void CreateDisk(IPowershellExecutor executor, string dataDiskName)
		{
			var addAzureDiskCommand = new AddAzureDiskCommand
			{
				DiskName = dataDiskName,
				Label = "NuMatsDrive", //TODO: yup
				MediaLocation = String.Format("http://numats.blob.core.windows.net/vhds/{0}.vhd", dataDiskName) //TODO: for serious
			};

			var results = executor.Execute(addAzureDiskCommand);
		}
	}
}
