using System.Linq;
using System.Web;
using System.Web.Http;
using AzureVmFarmer.Core.Messengers;
using AzureVmFarmer.Core.Repositories;
using AzureVmFarmer.Objects;
using StructureMap;

namespace AzureVmFarmer.Service.Controllers
{
	public class VirtualMachinesController : ApiController
	{
		private readonly IVirtualMachineRepository _repository;
		private readonly IMessenger _messenger;

		public VirtualMachinesController() : this(ObjectFactory.GetInstance<IVirtualMachineRepository>(), ObjectFactory.GetInstance<IMessenger>())
		{
		}

		public VirtualMachinesController(IVirtualMachineRepository repository, IMessenger messenger)
		{
			_repository = repository;
			_messenger = messenger;
		}

		public IQueryable<VirtualMachine> Get()
		{
			var result = _repository.Read().Take(25);

			return result;
		}

		public VirtualMachine Get(string name)
		{
			var result = _repository.Read().FirstOrDefault(x => x.Name == name);
			return result;
		}

		public VirtualMachine Post([FromBody] VirtualMachine value)
		{
			if (VirtualMachine.IsValid(value) == false)
			{
				throw new HttpException(400, "Invalid object.");
			}

			if (_repository.Read().Any(x => x.Name == value.Name))
			{
				throw new HttpException(409, "A virtual machine already exists with that name.");
			}
			
			_repository.Create(value);

			_messenger.QueueCreateMessage(value);

			var result = value;

			return result;
		}

		public void Put(string name, [FromBody] VirtualMachine value)
		{
			throw new HttpException(405, "PUT is not allowed for VirtualMachines at this time.");
		}

		public void Delete(string name)
		{
			var virtualMachine = new VirtualMachine
			{
				AdminPassword = ".",
				AdminUserName = ".",
				Location = ".",
				Name = name,
				Size = ".",
				TimeZone = "."
			}; //TODO: This feels hacky

			_repository.Delete(name);
			_messenger.QueueDeleteMessage(virtualMachine);
		}
	}
}
