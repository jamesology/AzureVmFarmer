using System.Linq;
using AzureVmFarmer.Objects;

namespace AzureVmFarmer.Core.Repositories
{
	public interface IVirtualMachineRepository
	{
		void Create(VirtualMachine machine);

		IQueryable<VirtualMachine> Read();

		void Delete(string name);
	}
}
