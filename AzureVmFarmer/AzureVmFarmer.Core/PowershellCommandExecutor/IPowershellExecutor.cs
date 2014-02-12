using System.Collections;
using System.Collections.Generic;
using AzureVmFarmer.Core.Commands;

namespace AzureVmFarmer.Core.PowershellCommandExecutor
{
	public interface IPowershellExecutor
	{
		IEnumerable<object> Execute(params PowerShellCommand[] commands);

		IEnumerable<object> Execute(IEnumerable input, params PowerShellCommand[] commands);
	}
}
