using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Threading;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace AzureVmFarmer.Core.Messengers.Impl
{
	public class SowMessageHandler : IMessageHandler
	{
		public void HandleMessage(BrokeredMessage message)
		{
			using (var runspace = RunspaceFactory.CreateRunspace())
			{
				runspace.Open();

				var virtualMachine = message.GetObject<VirtualMachine>();
				var imageName = CloudConfigurationManager.GetSetting("VirtualMachineBaseImageName");
				var dataDiskBase = CloudConfigurationManager.GetSetting("DataDiskName");
				var dataDiskName = String.Format("{0}-{1}", dataDiskBase, virtualMachine.Name);
				var sourceVhdName = String.Format("{0}.vhd", dataDiskBase);

				//var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
				//var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
				//var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
				//var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

				//TODO: find a subscription?

				//TODO: get storage account?

				if (AzureVmExists(runspace, virtualMachine) == false)
				{
					CreateNewVirtualMachine(runspace, virtualMachine, imageName, dataDiskName, sourceVhdName);
				}

				//TODO: attach disk?

				runspace.Close();
			}
		}

		private static bool AzureVmExists(Runspace runspace, VirtualMachine virtualMachine)
		{
			bool result;

			using (var pipeline = runspace.CreatePipeline())
			{
				var getAzureVmCommand = new GetAzureVmCommand
				{
					Name = virtualMachine.Name
				};

				pipeline.Commands.Add(getAzureVmCommand);

				var results = pipeline.Execute();

				result = results.Any();
			}

			return result;
		}

		private static void CreateNewVirtualMachine(Runspace runspace, VirtualMachine virtualMachine, string imageName, string dataDiskName, string sourceVhdName)
		{
			PrepareAzureDrive(runspace, virtualMachine, dataDiskName, sourceVhdName);

			ProvisionVirtualMachine(runspace, virtualMachine, imageName);

			AttachAzureDrive(runspace, virtualMachine, dataDiskName);
		}

		private static void PrepareAzureDrive(Runspace runspace, VirtualMachine virtualMachine, string dataDiskName, string sourceVhdName)
		{
			CopyVirtualHardDrive(runspace, virtualMachine, dataDiskName, sourceVhdName);

			CreateDisk(runspace, virtualMachine, dataDiskName);
		}

		private static void ProvisionVirtualMachine(Runspace runspace, VirtualMachine virtualMachine, string imageName)
		{
			using (var pipeline = runspace.CreatePipeline())
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

				pipeline.Commands.Add(newAzureVmConfigCommand);
				pipeline.Commands.Add(addAzureProvisioningConfigCommand);
				pipeline.Commands.Add(newAzureVmCommand);

				var results = pipeline.Execute();
			}
		}

		private static void AttachAzureDrive(Runspace runspace, VirtualMachine virtualMachine, string dataDiskName)
		{
			using (var pipeline = runspace.CreatePipeline())
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

				pipeline.Commands.Add(getAzureVm);
				pipeline.Commands.Add(addAzureDataDisk);
				pipeline.Commands.Add(updateAzureVm);

				var result = pipeline.Execute();
			}
		}

		private static void CopyVirtualHardDrive(Runspace runspace, VirtualMachine virtualMachine, string dataDiskName, string sourceVhdName)
		{
			using (var pipeline = runspace.CreatePipeline())
			{
				var azureStorageBlobCopyCommand = new StartAzureStorageBlobCopyCommand
				{
					SrcBlob = sourceVhdName,
					SrcContainer = "vhds", //TODO: ditto
					DestContainer = "vhds", //TODO: double ditto
					DestBlob = String.Format("{0}.vhd", dataDiskName),
					Force = true
				};

				pipeline.Commands.Add(azureStorageBlobCopyCommand);

				var results = pipeline.Execute();

				using (var statusPipeline = runspace.CreatePipeline())
				{
					var copyPending = false;

					var getAzureStorageBlobCopyStateCommand = new GetAzureStorageBlobCopyStateCommand();

					do
					{
						statusPipeline.Commands.Add(getAzureStorageBlobCopyStateCommand);

						var copyResult = statusPipeline.Execute(results)
							.Cast<PSObject>()
							.Select(x => x.BaseObject)
							.FirstOrDefault();

						if (copyResult != null)
						{
							var type = copyResult.GetType();

							var property = type.GetProperty("Status", BindingFlags.Instance | BindingFlags.Public);

							var value = String.Format("{0}", property.GetValue(copyResult));

							copyPending = value == "Pending";
						}

						if (copyPending)
						{
							Thread.Sleep(10);
						}
					} while (copyPending);
				}
			}
		}

		private static void CreateDisk(Runspace runspace, VirtualMachine virtualMachine, string dataDiskName)
		{
			using (var pipeline = runspace.CreatePipeline())
			{
				var addAzureDiskCommand = new AddAzureDiskCommand
				{
					DiskName = dataDiskName,
					Label = "NuMatsDrive", //TODO: yup
					MediaLocation = String.Format("http://numats.blob.core.windows.net/vhds/{0}.vhd", dataDiskName)
					//TODO: for serious
				};

				pipeline.Commands.Add(addAzureDiskCommand);

				var results = pipeline.Execute();
			}
		}
	}
}
