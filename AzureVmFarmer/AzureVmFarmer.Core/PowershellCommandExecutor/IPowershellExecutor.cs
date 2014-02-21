using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using AzureVmFarmer.Core.Commands;

namespace AzureVmFarmer.Core.PowershellCommandExecutor
{
	public interface IPowershellExecutor
	{
		IEnumerable<PSObject> Execute(params PowerShellCommand[] commands);

		IEnumerable<PSObject> Execute(IEnumerable input, params PowerShellCommand[] commands);
	}
}
