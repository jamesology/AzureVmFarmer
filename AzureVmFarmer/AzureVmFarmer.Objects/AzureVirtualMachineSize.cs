using System;
using System.Xml.Serialization;

namespace AzureVmFarmer.Objects
{
	[Serializable]
	public enum AzureVirtualMachineSize
	{
		[XmlEnum("None")]
		None = 0,
		[XmlEnum("ExtraSmall")]
		ExtraSmall,
		[XmlEnum("Small")]
		Small,
		[XmlEnum("Medium")]
		Medium,
		[XmlEnum("Large")]
		Large,
		[XmlEnum("ExtraLarge")]
		ExtraLarge,
		[XmlEnum("A5")]
		A5,
		[XmlEnum("A6")]
		A6,
		[XmlEnum("A7")]
		A7,
		[XmlEnum("A8")]
		A8,
		[XmlEnum("A9")]
		A9
	}
}
