using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;

namespace AzureVmFarmer.Core.PowershellCommandExecutor.Impl
{
	public class PowershellExecutor : IPowershellExecutor
	{
		public IEnumerable<PSObject> Execute(params PowerShellCommand[] commands)
		{
			var result = Execute(null, commands);
			return result;
		}

		public IEnumerable<PSObject> Execute(IEnumerable input, params PowerShellCommand[] commands)
		{
			IEnumerable<PSObject> result;

			using (var runspace = RunspaceFactory.CreateRunspace())
			{
				runspace.Open();

				using (var pipeline = runspace.CreatePipeline())
				{
					foreach (var powerShellCommand in commands)
					{
						pipeline.Commands.Add(powerShellCommand);
					}

					result = pipeline.Execute(input).Cast<PSObject>();
				}

				runspace.Close();
			}

			return result;
		}
	}
}
