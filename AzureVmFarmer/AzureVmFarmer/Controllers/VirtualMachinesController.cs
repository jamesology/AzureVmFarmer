using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using AzureVmFarmer.Objects;

namespace AzureVmFarmer.Controllers
{
	public class VirtualMachinesController : ApiController
	{
		private static readonly IList<VirtualMachine> Machines = new List<VirtualMachine>(); 
		public IEnumerable<VirtualMachine> Get()
		{
			return Machines;
		}

		public HttpResponseMessage Get(int id)
		{
			var result = new HttpResponseMessage();
			if (id >= Machines.Count())
			{
				result.StatusCode = HttpStatusCode.NotFound;
				result.Content = new StringContent("No virtual machine with that Id found.");
			}
			else
			{
				result.StatusCode = HttpStatusCode.OK;
				result.Content = new ObjectContent(typeof(VirtualMachine), Machines[id], new JsonMediaTypeFormatter());
			}

			return result;
		}

		public HttpResponseMessage Post([FromBody] VirtualMachine value)
		{
			var result = new HttpResponseMessage(HttpStatusCode.Accepted);
			result.Content = new ObjectContent(typeof(VirtualMachine), value, new JsonMediaTypeFormatter());

			Machines.Add(value);

			return result;
		}

		public HttpResponseMessage Put(int id, [FromBody] string value)
		{
			var result = new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.NotImplemented,
				Content = new StringContent("PUT is not allowed for VirtualMachines at this time.")
			};

			return result;
		}

		public HttpResponseMessage Delete(int id)
		{
			var result = new HttpResponseMessage();
			if (id >= Machines.Count())
			{
				result.StatusCode = HttpStatusCode.NotFound;
				result.Content = new StringContent("No virtual machine with that Id found.");
			}
			else
			{
				result.StatusCode = HttpStatusCode.OK;
			}

			return result;
		}
	}
}
