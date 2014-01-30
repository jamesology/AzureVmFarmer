using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureVmFarmer.Controllers;

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

			// Act
			var result = controller.Get();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.Count());
			Assert.AreEqual("under construction", result.ElementAt(0));
		}

		[TestMethod]
		public void GetById()
		{
			// Arrange
			var controller = new VirtualMachinesController();

			// Act
			string result = controller.Get(5);

			// Assert
			Assert.AreEqual("under construction", result);
		}

		[TestMethod]
		public void Post()
		{
			// Arrange
			var controller = new VirtualMachinesController();

			// Act
			var response = controller.Post("value");

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
			Assert.AreEqual(HttpStatusCode.Forbidden, result.StatusCode);
		}
	}
}
