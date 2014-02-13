using System;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core.Commands
{
	public abstract class PowerShellCommand
	{
		private const string VerboseParameter = "Verbose";
		private const string DebugParameter = "Debug";
		private const string ErrorActionParameter = "ErrorAction";
		private const string ErrorVariableParameter = "ErrorVariable";
		private const string OutBufferParameter = "OutBuffer";
		private const string OutVariableParameter = "OutVariable";

		public bool Debug { get; set; }
		public ErrorAction ErrorAction { get; set; }
		public string ErrorVariable { get; set; }
		public bool ErrorVariableAppend { get; set; }
		public int OutBuffer { get; set; }
		public string OutVariable { get; set; }
		public bool OutVariableAppend { get; set; }
		public bool Verbose { get; set; }

		protected abstract Command BuildCommand();

		public static implicit operator Command(PowerShellCommand command)
		{
			var result = command.BuildCommand();

			if (command.Debug)
			{
				result.Parameters.Add(DebugParameter);
			}

			if (command.Verbose)
			{
				result.Parameters.Add(VerboseParameter);
			}

			if (command.ErrorAction > ErrorAction.Continue)
			{
				result.Parameters.Add(ErrorActionParameter, command.ErrorAction);
			}

			if (String.IsNullOrWhiteSpace(command.ErrorVariable) == false)
			{
				var value = string.Format("{0}{1}", command.ErrorVariableAppend ? "+" : String.Empty, command.ErrorVariable);
				result.Parameters.Add(ErrorVariableParameter, value);
			}

			if (command.OutBuffer > 0)
			{
				result.Parameters.Add(OutBufferParameter, command.OutBuffer);
			}

			if (String.IsNullOrWhiteSpace(command.OutVariable) == false)
			{
				var value = string.Format("{0}{1}", command.OutVariableAppend ? "+" : String.Empty, command.OutVariable);
				result.Parameters.Add(OutVariableParameter, value);
			}

			return result;
		}
	}
}
