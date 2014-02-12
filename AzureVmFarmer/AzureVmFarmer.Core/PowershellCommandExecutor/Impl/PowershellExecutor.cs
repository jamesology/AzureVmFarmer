using System.Collections;
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;

namespace AzureVmFarmer.Core.PowershellCommandExecutor.Impl
{
	public class PowershellExecutor : IPowershellExecutor
	{
		public IEnumerable<object> Execute(params PowerShellCommand[] commands)
		{
			var result = Execute(null, commands);
			return result;
		}

		public IEnumerable<object> Execute(IEnumerable input, params PowerShellCommand[] commands)
		{
			IEnumerable<object> result;

			using (var runspace = RunspaceFactory.CreateRunspace())
			{
				runspace.Open();

				using (var pipeline = runspace.CreatePipeline())
				{
					foreach (var powerShellCommand in commands)
					{
						pipeline.Commands.Add(powerShellCommand);
					}

					result = pipeline.Execute(input);
				}

				runspace.Close();
			}

			return result;
		}
	}
}
