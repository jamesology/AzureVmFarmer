using System;
using System.Linq;
using System.Management.Automation.Runspaces;
using AzureVmFarmer.Core.Commands;
using NUnit.Framework;

namespace AzureVmFarmer.Core.Tests.Commands
{
	[TestFixture]
	class PowerShellCommandTests
	{
		private class PowerShellCommandTestHarness : PowerShellCommand
		{
			protected override Command BuildCommand()
			{
				return new Command(String.Empty);
			}
		}

		[Test]
		public void CommandOperator_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness();

			Command actual = expected;

			Assert.That(actual.Parameters, Is.Empty);
		}

		[Test]
		public void CommandOperator_DebugFalse_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				Debug = false
			};

			Command actual = expected;
			var debugParameter = actual.Parameters.FirstOrDefault(x => x.Name == "Debug");

			Assert.That(debugParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_DebugTrue_SetsDebug()
		{
			var expected = new PowerShellCommandTestHarness
			{
				Debug = true
			};

			Command actual = expected;
			var debugParameter = actual.Parameters.FirstOrDefault(x => x.Name == "Debug");

			Assert.That(debugParameter, Is.Not.Null);
		}

		[Test]
		public void CommandOperator_ErrorActionIsContinue_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorAction = ErrorAction.Continue
			};

			Command actual = expected;
			var errorActionParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorAction");

			Assert.That(errorActionParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_ErrorActionIsSomething_SetsErrorAction()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorAction = ErrorAction.Stop
			};

			Command actual = expected;
			var errorActionParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorAction");

			Assert.That(errorActionParameter, Is.Not.Null);
			Assert.That(errorActionParameter.Value, Is.EqualTo(expected.ErrorAction));
		}

		[Test]
		public void CommandOperator_ErrorVariableIsEmpty_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorVariable = String.Empty
			};

			Command actual = expected;
			var errorVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorVariable");

			Assert.That(errorVariableParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_ErrorVariableIsSomething_SetsErrorVariable()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorVariable = "SomeVariable"
			};

			Command actual = expected;
			var errorVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorVariable");

			Assert.That(errorVariableParameter, Is.Not.Null);
			Assert.That(errorVariableParameter.Value, Is.EqualTo(expected.ErrorVariable));
		}

		[Test]
		public void CommandOperator_ErrorVariableIsEmptyErrorVariableAppendIsTrue_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorVariable = String.Empty,
				ErrorVariableAppend = true
			};

			Command actual = expected;
			var errorVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorVariable");

			Assert.That(errorVariableParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_ErrorVariableIsSomethingErrorVariableAppendIsFalse_SetsErrorVariable()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorVariable = "SomeVariable",
				ErrorVariableAppend = false
			};

			Command actual = expected;
			var errorVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorVariable");

			Assert.That(errorVariableParameter, Is.Not.Null);
			Assert.That(errorVariableParameter.Value, Is.EqualTo(expected.ErrorVariable));
		}

		[Test]
		public void CommandOperator_ErrorVariableIsSomethingErrorVariableAppendIsTrue_SetsErrorVariableWithAppend()
		{
			var expected = new PowerShellCommandTestHarness
			{
				ErrorVariable = "SomeVariable",
				ErrorVariableAppend = true
			};

			Command actual = expected;
			var errorVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "ErrorVariable");

			Assert.That(errorVariableParameter, Is.Not.Null);
			Assert.That(errorVariableParameter.Value, Is.EqualTo(String.Format("+{0}", expected.ErrorVariable)));
		}

		[Test]
		public void CommandOperator_OutBufferIsZero_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutBuffer = 0
			};

			Command actual = expected;
			var outBufferParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutBuffer");

			Assert.That(outBufferParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_OutBufferIsSomething_SetsOutBuffer()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutBuffer = 10
			};

			Command actual = expected;
			var outBufferParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutBuffer");

			Assert.That(outBufferParameter, Is.Not.Null);
			Assert.That(outBufferParameter.Value, Is.EqualTo(expected.OutBuffer));
		}

		[Test]
		public void CommandOperator_OutVariableIsEmpty_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutVariable = String.Empty
			};

			Command actual = expected;
			var outVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutVariable");

			Assert.That(outVariableParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_OutVariableIsSomething_SetsOutVariable()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutVariable = "SomeVariable"
			};

			Command actual = expected;
			var outVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutVariable");

			Assert.That(outVariableParameter, Is.Not.Null);
			Assert.That(outVariableParameter.Value, Is.EqualTo(expected.OutVariable));
		}

		[Test]
		public void CommandOperator_OutVariableIsEmptyOutVariableAppendIsTrue_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutVariable = String.Empty,
				OutVariableAppend = true
			};

			Command actual = expected;
			var outVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutVariable");

			Assert.That(outVariableParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_OutVariableIsSomethingOutVariableAppendIsFalse_SetsOutVariable()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutVariable = "SomeVariable",
				OutVariableAppend = false
			};

			Command actual = expected;
			var outVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutVariable");

			Assert.That(outVariableParameter, Is.Not.Null);
			Assert.That(outVariableParameter.Value, Is.EqualTo(expected.OutVariable));
		}

		[Test]
		public void CommandOperator_OutVariableIsSomethingOutVariableAppendIsTrue_SetsOutVariableWithAppend()
		{
			var expected = new PowerShellCommandTestHarness
			{
				OutVariable = "SomeVariable",
				OutVariableAppend = true
			};

			Command actual = expected;
			var outVariableParameter = actual.Parameters.FirstOrDefault(x => x.Name == "OutVariable");

			Assert.That(outVariableParameter, Is.Not.Null);
			Assert.That(outVariableParameter.Value, Is.EqualTo(String.Format("+{0}", expected.OutVariable)));
		}

		[Test]
		public void CommandOperator_VerboseIsFalse_SetsNothing()
		{
			var expected = new PowerShellCommandTestHarness
			{
				Verbose = false
			};

			Command actual = expected;
			var verboseParameter = actual.Parameters.FirstOrDefault(x => x.Name == "Verbose");

			Assert.That(verboseParameter, Is.Null);
		}

		[Test]
		public void CommandOperator_VerboseIsTrue_SetsVerbose()
		{
			var expected = new PowerShellCommandTestHarness
			{
				Verbose = true
			};

			Command actual = expected;
			var verboseParameter = actual.Parameters.FirstOrDefault(x => x.Name == "Verbose");

			Assert.That(verboseParameter, Is.Not.Null);
		}
	}
}
