using System;
using System.Xml.Serialization;

namespace AzureVmFarmer.Core.Commands
{
	[Serializable]
	public enum ErrorAction
	{
		[XmlEnum]
		Continue,
		[XmlEnum]
		Ignore,
		[XmlEnum]
		Inquire,
		[XmlEnum]
		SilentlyContinue,
		[XmlEnum]
		Stop,
		[XmlEnum]
		Suspend
	}
}
