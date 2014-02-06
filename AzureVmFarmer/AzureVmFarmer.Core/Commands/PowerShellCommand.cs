using System;
using System.Collections;
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
		public string ErrorAction { get; set; } //TODO: Enum this sucka
		public string ErrorVariable { get; set; } //TODO: Allow append
		public int OutBuffer { get; set; }
		public string OutVariable { get; set; } //TODO: Allow append
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

			if (String.IsNullOrWhiteSpace(command.ErrorAction) == false)
			{
				result.Parameters.Add(ErrorActionParameter, command.ErrorAction);
			}

			if (String.IsNullOrWhiteSpace(command.ErrorVariable) == false)
			{
				result.Parameters.Add(ErrorVariableParameter, command.ErrorVariable);
			}

			if (command.OutBuffer > 0)
			{
				result.Parameters.Add(OutBufferParameter, command.OutBuffer);
			}

			if (String.IsNullOrWhiteSpace(command.OutVariable))
			{
				result.Parameters.Add(OutVariableParameter, command.OutVariable);
			}

			return result;
		}
	}
}
