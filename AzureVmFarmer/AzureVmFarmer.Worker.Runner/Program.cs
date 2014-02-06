namespace AzureVmFarmer.Worker.Runner
{
	class Program
	{
		static void Main(string[] args)
		{
			var worker = new WorkerRole();

			worker.OnStart();

			worker.Run();

			worker.OnStop();
		}
	}
}
