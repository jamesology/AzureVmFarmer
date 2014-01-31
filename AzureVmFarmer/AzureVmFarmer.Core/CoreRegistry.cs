using System;
using AzureVmFarmer.Core.Repositories;
using StructureMap.Configuration.DSL;

namespace AzureVmFarmer.Core
{
	public class CoreRegistry : Registry
	{
		public CoreRegistry()
		{
			Scan(x =>
			{
				x.Assembly("AzureVmFarmer.Core");
				x.WithDefaultConventions();
			});
		}
	}
}
