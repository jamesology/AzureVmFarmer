using System.Collections.Generic;
using System.Linq;
using AzureVmFarmer.Objects;

namespace AzureVmFarmer.Core.Repositories.Impl
{
	//TODO: implement this for real.
	public class VirtualMachineRepository : IVirtualMachineRepository
	{
		private static readonly IDictionary<string, VirtualMachine> VirtualMachines = new Dictionary<string, VirtualMachine>();

		public void Create(VirtualMachine machine)
		{
			//VirtualMachines[machine.Name] = machine;
		}

		public IQueryable<VirtualMachine> Read()
		{
			return VirtualMachines.Values.AsQueryable();
		}

		public void Delete(string name)
		{
			VirtualMachines.Remove(name);
		}
	}
}
