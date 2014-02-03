using System;

namespace AzureVmFarmer.Objects
{
	public class VirtualMachine
	{
		public string Name { get; set; }

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

			return result;
		}
	}
}
