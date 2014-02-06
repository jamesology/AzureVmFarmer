using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace AzureVmFarmer.Core
{
	public static class PipelineExtensions
	{
		public static IEnumerable<object> Execute(this Pipeline pipeline, IEnumerable input = null)
		{
			var results = (input == null)
				? pipeline.Invoke()
				: pipeline.Invoke(input);

			var errors = pipeline.Error.ReadToEnd()
				.Cast<PSObject>()
				.Select(x => x.BaseObject)
				.Cast<ErrorRecord>()
				.Select(x => x.Exception)
				.ToList();

			if (errors.Any())
			{
				throw new AggregateException(errors);
			}

			return results;
		}
	}
}
