using System;

namespace AzureVmFarmer.Objects
{
	public class VirtualMachine
	{
		public string Name { get; set; }

		public static bool IsValid(VirtualMachine virtualMachine)
		{
			var result = !String.IsNullOrWhiteSpace(virtualMachine.Name);

			return result;
		}
	}
}
