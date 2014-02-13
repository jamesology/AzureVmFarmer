using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AzureVmFarmer.Objects
{
	public class VirtualMachine
	{
		public string Name { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public AzureVirtualMachineSize Size { get; set; }

		public string AdminUserName { get; set; }

		public string AdminPassword { get; set; }

		public string TimeZone { get; set; }

		public string Location { get; set; }

		public static bool IsValid(VirtualMachine virtualMachine)
		{
			var result = true;

			if (virtualMachine == null)
			{
				result = false;
			}
			else if (String.IsNullOrWhiteSpace(virtualMachine.Name))
			{
				result = false;
			}
			else if (virtualMachine.Size == AzureVirtualMachineSize.None)
			{
				result = false;
			}
			else if (String.IsNullOrWhiteSpace(virtualMachine.AdminUserName))
			{
				result = false;
			}
			else if (String.IsNullOrWhiteSpace(virtualMachine.AdminPassword))
			{
				result = false;
			}
			else if (String.IsNullOrWhiteSpace(virtualMachine.TimeZone))
			{
				result = false;
			}
			else if (String.IsNullOrWhiteSpace(virtualMachine.Location))
			{
				result = false;
			}

			return result;
		}
	}
}
