using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class CultureTest
	{
		class Configuration
		{
			public double DoubleSwedishCulture { get; private set; }
		}

		[TestMethod]
		public void ParsesCultureSpecificValue()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(new CultureInfo("sv-se"), null, new TestConfigurationProvider("42,42"));
			Assert.AreEqual(42.42, configuration.DoubleSwedishCulture);
		}
	}
}
