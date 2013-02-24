using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	public static class StaticConfigruation
	{
		public static int Int32 { get; set; }
	}

	public class NonStaticConfiguration
	{
		public int Int32 { get; set; }
	}

	[TestClass]
	public class DeserializeTest
	{
		[TestMethod]
		public void CanDeserializeNonStaticConfiguration()
		{
			var nonStaticConfiguration = ConfigurationSerializer.Deserialize<NonStaticConfiguration>();
			Assert.AreEqual(42, nonStaticConfiguration.Int32);
		}

		[TestMethod]
		public void CanDeserializeStaticConfiguration()
		{
			ConfigurationSerializer.Deserialize(typeof (StaticConfigruation));
			Assert.AreEqual(42, StaticConfigruation.Int32);
		}		
	}
}
