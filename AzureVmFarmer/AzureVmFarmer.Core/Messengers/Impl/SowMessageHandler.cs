using System;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using AzureVmFarmer.Core.Commands;
using AzureVmFarmer.Objects;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Blob;

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
				var imageName = "NuMatsRunnerBase"; //TODO: Get from somewhere.

				//var subscriptionId = CloudConfigurationManager.GetSetting("Azure.SubscriptionId");
				//var managementCertificateString = CloudConfigurationManager.GetSetting("Azure.ManagementCertificate");
				//var managementCertificate = new X509Certificate2(Convert.FromBase64String(managementCertificateString));
				//var credentials = new CertificateCloudCredentials(subscriptionId, managementCertificate);

				//TODO: find a subscription?

				//TODO: get storage account?

				DeleteExistingArtifacts(runspace, virtualMachine);

				CreateNewVirtualMachine(runspace, virtualMachine, imageName);

				//TODO: attach disk?

				runspace.Close();
			}
		}

		private static void DeleteExistingArtifacts(Runspace runspace, VirtualMachine virtualMachine)
		{
			if (AzureServiceExists(runspace, virtualMachine) || AzureVmExists(runspace, virtualMachine))
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
					ServiceName = virtualMachine.Name
				};

				pipeline.Commands.Add(getAzureServiceCommand);

				var results = pipeline.Execute();

				result = results.Any();
			}

			return result;
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

		private static void CreateNewVirtualMachine(Runspace runspace, VirtualMachine virtualMachine, string imageName)
		{
			PrepareAzureDrive(runspace, virtualMachine);

			ProvisionVirtualMachine(runspace, virtualMachine, imageName);

			AttachAzureDrive(runspace, virtualMachine);
		}

		private static void PrepareAzureDrive(Runspace runspace, VirtualMachine virtualMachine)
		{
			CopyVirtualHardDrive(runspace, virtualMachine);

			CreateDisk(runspace, virtualMachine);
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

		private static void AttachAzureDrive(Runspace runspace, VirtualMachine virtualMachine)
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
					DiskName = String.Format("AzureTest-{0}", virtualMachine.Name), //TODO: Get from somewhere
					LogicalUnitNumber = 0
				};

				var updateAzureVm = new UpdateAzureVmCommand();

				pipeline.Commands.Add(getAzureVm);
				pipeline.Commands.Add(addAzureDataDisk);
				pipeline.Commands.Add(updateAzureVm);

				var result = pipeline.Execute();
			}
		}

		private static void CopyVirtualHardDrive(Runspace runspace, VirtualMachine virtualMachine)
		{
			using (var pipeline = runspace.CreatePipeline())
			{
				var azureStorageBlobCopyCommand = new StartAzureStorageBlobCopyCommand
				{
					SrcBlob = "AzureTest.vhd", //TODO: pass this in
					SrcContainer = "vhds", //TODO: ditto
					DestContainer = "vhds", //TODO: double ditto
					DestBlob = String.Format("AzureTest-{0}.vhd", virtualMachine.Name),
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

		private static void CreateDisk(Runspace runspace, VirtualMachine virtualMachine)
		{
			using (var pipeline = runspace.CreatePipeline())
			{
				var addAzureDiskCommand = new AddAzureDiskCommand
				{
					DiskName = String.Format("AzureTest-{0}", virtualMachine.Name), //TODO: you know the drill
					Label = "AzureTest", //TODO: yup
					MediaLocation = String.Format("http://numats.blob.core.windows.net/vhds/AzureTest-{0}.vhd", virtualMachine.Name)
					//TODO: for serious
				};

				pipeline.Commands.Add(addAzureDiskCommand);

				var results = pipeline.Execute();
			}
		}
	}
}
