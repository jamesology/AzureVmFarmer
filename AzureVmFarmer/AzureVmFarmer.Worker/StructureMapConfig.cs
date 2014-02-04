using AzureVmFarmer.Core;
using StructureMap;

namespace AzureVmFarmer.Worker
{
	public class StructureMapConfig : IBootstrapper
	{
		private static bool _hasStarted;
		public void BootstrapStructureMap()
		{
			ObjectFactory.Initialize(x => x.AddRegistry<CoreRegistry>());
		}

		public static void Restart()
		{
			if (_hasStarted)
			{
				ObjectFactory.ResetDefaults();
			}
			else
			{
				Bootstrap();
				_hasStarted = true;
			}
		}

		public static void Bootstrap()
		{
			new StructureMapConfig().BootstrapStructureMap();
		}
	}
}
