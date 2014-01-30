using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using AzureVmFarmer.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureVmFarmer.Controllers;
using Newtonsoft.Json;

namespace AzureVmFarmer.Tests.Controllers
{
	[TestClass]
	public class VirtualMachinesControllerTest
	{
		[TestMethod]
		public void Get()
		{
			// Arrange
			var controller = new VirtualMachinesController();
			controller.Post(new VirtualMachine {Name = "Machine1"});

			// Act
			var result = controller.Get();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("Machine1", result.ElementAt(0).Name);
		}

		[TestMethod]
		public void GetById()
		{
			// Arrange
			var controller = new VirtualMachinesController();
			controller.Post(new VirtualMachine { Name = "Machine1" });
			controller.Post(new VirtualMachine { Name = "Machine2" });

			// Act
			var response = controller.Get(2);
			var task = response.Content.ReadAsAsync<VirtualMachine>();
			while(task.IsCompleted == false)
			{ Thread.Sleep(5);}
			var result = task.Result;

			// Assert
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			Assert.AreEqual("Machine2", result.Name);
		}

		[TestMethod]
		public void Post()
		{
			// Arrange
			var controller = new VirtualMachinesController();

			// Act
			var response = controller.Post(new VirtualMachine {Name = "Post Test"});

			// Assert
			Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
		}

		[TestMethod]
		public void Put()
		{
			// Arrange
			var controller = new VirtualMachinesController();

			// Act
			var result = controller.Put(5, "value");

			// Assert
			Assert.AreEqual(HttpStatusCode.NotImplemented, result.StatusCode);
		}

		[TestMethod]
		public void Delete()
		{
			// Arrange
			var controller = new VirtualMachinesController();

			// Act
			var result = controller.Delete(5);

			// Assert
			Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
		}
	}
}
