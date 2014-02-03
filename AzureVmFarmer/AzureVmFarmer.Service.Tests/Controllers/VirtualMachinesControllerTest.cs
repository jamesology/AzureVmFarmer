using System.Linq;
using System.Web;
using AzureVmFarmer.Core.Messengers;
using AzureVmFarmer.Core.Repositories;
using AzureVmFarmer.Objects;
using AzureVmFarmer.Service.Controllers;
using NUnit.Framework;
using Rhino.Mocks;

namespace AzureVmFarmer.Service.Tests.Controllers
{
	[TestFixture]
	public class VirtualMachinesControllerTest
	{
		[Test]
		public void Get_NoVirtualMachines_ReturnsEmptyList()
		{
			// Arrange
			var machineList = new VirtualMachine[0];

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();
	
			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			var result = controller.Get();

			// Assert
			Assert.That(result.Any(), Is.False);
		}

		[Test]
		public void Get_RepositoryIsCalled()
		{
			// Arrange
			var machineList = new VirtualMachine[0];

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			controller.Get();

			// Assert
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Get_HasManyVirtualMachines_ReturnsPartialList()
		{
			// Arrange
			var machineList = new[]
			{
				new VirtualMachine {Name = "Machine1"},
				new VirtualMachine {Name = "Machine2"},
				new VirtualMachine {Name = "Machine3"},
				new VirtualMachine {Name = "Machine4"},
				new VirtualMachine {Name = "Machine5"},
				new VirtualMachine {Name = "Machine6"},
				new VirtualMachine {Name = "Machine7"},
				new VirtualMachine {Name = "Machine8"},
				new VirtualMachine {Name = "Machine9"},
				new VirtualMachine {Name = "Machine10"},
				new VirtualMachine {Name = "Machine11"},
				new VirtualMachine {Name = "Machine12"},
				new VirtualMachine {Name = "Machine13"},
				new VirtualMachine {Name = "Machine14"},
				new VirtualMachine {Name = "Machine15"},
				new VirtualMachine {Name = "Machine16"},
				new VirtualMachine {Name = "Machine17"},
				new VirtualMachine {Name = "Machine18"},
				new VirtualMachine {Name = "Machine19"},
				new VirtualMachine {Name = "Machine20"},
				new VirtualMachine {Name = "Machine21"},
				new VirtualMachine {Name = "Machine22"},
				new VirtualMachine {Name = "Machine23"},
				new VirtualMachine {Name = "Machine24"},
				new VirtualMachine {Name = "Machine25"},
				new VirtualMachine {Name = "Machine26"},
				new VirtualMachine {Name = "Machine27"}
			};

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			var result = controller.Get();

			// Assert
			Assert.That(result.Count(), Is.EqualTo(25));
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Get_HasVirtualMachines_ReturnsList()
		{
			// Arrange
			var machineList = new[]
			{
				new VirtualMachine {Name = "Machine1"},
				new VirtualMachine {Name = "Machine2"},
				new VirtualMachine {Name = "Machine3"}
			};

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			var result = controller.Get();

			// Assert
			Assert.That(result.Any(), Is.True);
			repository.VerifyAllExpectations();
		}

		[Test]
		public void GetByName_VirtualMachineDoesNotExist_ReturnsNull()
		{
			// Arrange
			var machineList = new[]
			{
				new VirtualMachine {Name = "Machine1"},
				new VirtualMachine {Name = "Machine2"},
				new VirtualMachine {Name = "Machine3"}
			};

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();
	
			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			var result = controller.Get("FakeMachine");

			// Assert
			Assert.That(result, Is.Null);
		}

		[Test]
		public void GetByName_VirtualMachineExists_ReturnsMachine()
		{
			// Arrange
			var machineList = new[]
			{
				new VirtualMachine {Name = "Machine1"},
				new VirtualMachine {Name = "Machine2"},
				new VirtualMachine {Name = "Machine3"}
			};

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			var result = controller.Get("Machine2");

			// Assert
			Assert.That(result.Name, Is.EqualTo("Machine2"));
		}

		[Test]
		public void GetByName_VirtualMachineExists_RepositoryIsCalled()
		{
			// Arrange
			var machineList = new[]
			{
				new VirtualMachine {Name = "Machine1"},
				new VirtualMachine {Name = "Machine2"},
				new VirtualMachine {Name = "Machine3"}
			};

			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(machineList.AsQueryable());

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			controller.Get("Machine2");

			// Assert
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Post_VirtualMachineHasNoName_ThrowsHttpException()
		{
			// Arrange
			var repository = MockRepository.GenerateStub<IVirtualMachineRepository>();
			var messenger = MockRepository.GenerateStub<IMessenger>();
			var controller = new VirtualMachinesController(repository, messenger);

			// Act

			// Assert
			Assert.That(() => controller.Post(new VirtualMachine()), Throws.InstanceOf<HttpException>());
		}

		[Test]
		public void Post_ValidVirtualMachine_ReturnsMachine()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(new EnumerableQuery<VirtualMachine>(Enumerable.Empty<VirtualMachine>()));
			var messenger = MockRepository.GenerateStub<IMessenger>();
			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			var result = controller.Post(new VirtualMachine{Name = "Machine"});

			// Assert
			Assert.That(result.Name, Is.EqualTo("Machine"));
		}

		[Test]
		public void Post_VirtualMachineAlreadyExists_ThrowsHttpException()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(new EnumerableQuery<VirtualMachine>(new[] {new VirtualMachine {Name = "Machine"}}));

			var messenger = MockRepository.GenerateStub<IMessenger>();
			var controller = new VirtualMachinesController(repository, messenger);

			// Act

			// Assert
			Assert.That(() => controller.Post(new VirtualMachine {Name = "Machine"}), Throws.InstanceOf<HttpException>());
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Post_VirtualMachineAlreadyExists_RepositoryCreateIsNotCalled()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(new EnumerableQuery<VirtualMachine>(new[] { new VirtualMachine { Name = "Machine" } }));
			repository.Expect(x => x.Create(Arg<VirtualMachine>.Is.TypeOf))
				.Repeat.Never();

			var messenger = MockRepository.GenerateStub<IMessenger>();
			var controller = new VirtualMachinesController(repository, messenger);

			// Act

			// Assert
			Assert.That(() => controller.Post(new VirtualMachine { Name = "Machine" }), Throws.InstanceOf<HttpException>());
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Post_ValidVirtualMachine_RepositoryIsCalled()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(new EnumerableQuery<VirtualMachine>(Enumerable.Empty<VirtualMachine>()));
			repository.Expect(x => x.Create(Arg<VirtualMachine>.Is.TypeOf));

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			var machine = new VirtualMachine
			{
				Name = "Post Test"
			};

			// Act
			controller.Post(machine);

			// Assert
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Post_InvalidVirtualMachine_RepositoryIsNotCalled()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Create(Arg<VirtualMachine>.Is.TypeOf))
				.Repeat.Never();

			var messenger = MockRepository.GenerateStub<IMessenger>();

			var controller = new VirtualMachinesController(repository, messenger);

			var machine = new VirtualMachine();

			// Act
			try
			{
				controller.Post(machine);
			}
			catch (HttpException) { }

			// Assert
			repository.VerifyAllExpectations();
		}

		[Test]
		public void Post_ValidVirtualMachine_MessageIsSent()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Read())
				.Repeat.Once()
				.Return(new EnumerableQuery<VirtualMachine>(Enumerable.Empty<VirtualMachine>()));

			var messenger = MockRepository.GenerateMock<IMessenger>();
			messenger.Expect(x => x.QueueCreateMessage(Arg<VirtualMachine>.Is.TypeOf));

			var controller = new VirtualMachinesController(repository, messenger);

			var machine = new VirtualMachine
			{
				Name = "Post Test"
			};

			// Act
			controller.Post(machine);

			// Assert
			messenger.VerifyAllExpectations();
		}

		[Test]
		public void Put_ThrowsHttpException()
		{
			// Arrange
			var repository = MockRepository.GenerateStub<IVirtualMachineRepository>();
			var messenger = MockRepository.GenerateStub<IMessenger>();
			var controller = new VirtualMachinesController(repository, messenger);

			// Act

			// Assert
			Assert.That(() => controller.Put("ExistingMachine", new VirtualMachine {Name = "ExistingMachine"}), Throws.InstanceOf<HttpException>());
			repository.VerifyAllExpectations();
			messenger.VerifyAllExpectations();
		}

		[Test]
		public void Delete_RepositoryIsCalled()
		{
			// Arrange
			var repository = MockRepository.GenerateMock<IVirtualMachineRepository>();
			repository.Expect(x => x.Delete(Arg<string>.Is.TypeOf))
				.Repeat.Once();

			var messenger = MockRepository.GenerateStub<IMessenger>();
			var controller = new VirtualMachinesController(repository, messenger);

			// Act
			controller.Delete("Machine");

			// Assert
			repository.VerifyAllExpectations();
		}
	}
}
