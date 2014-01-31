namespace AzureVmFarmer.Core.Messengers.Impl
{
	class Messenger : IMessenger
	{
		private static int _messageCount = 0;
		public void QueueMessage()
		{
			_messageCount += 1;
		}
	}
}
