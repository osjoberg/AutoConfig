using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class CustomSectionTest
	{
		class Configuration
		{
			public AutoConfigManagerTests.TestConfigurationSection NotOverridden { get; private set; }
			public AutoConfigManagerTests.TestConfigurationSection Missing { get; private set; }
		}

		[TestMethod]
		public void CanDeserializeCustomSection()
		{			
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "NotOverridden" }, null);
			Assert.AreEqual("It is not overridden.", configuration.NotOverridden.Test);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.ConfigSectionNotFound, "Missing")]
		public void MissingCustomSectionThrowsException()
		{
			ConfigurationSerializer.Deserialize<Configuration>();
		}
	}
}
