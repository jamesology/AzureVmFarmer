using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureVmFarmer.Controllers
{
	public class VirtualMachinesController : ApiController
	{
		public IEnumerable<string> Get()
		{
			return new[] {"under construction"};
		}

		public string Get(int id)
		{
			return "under construction";
		}

		public HttpResponseMessage Post([FromBody] string value)
		{
			var result = new HttpResponseMessage(HttpStatusCode.Accepted);

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
			var result = new HttpResponseMessage(HttpStatusCode.Forbidden);

			return result;
		}
	}
}
