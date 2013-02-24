using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class StringTest
	{
		class Configuration
		{
			public string EmptyString { get; private set; }
			public string String { get; private set; }
			public string MissingString { get; private set; }
		}

		[TestMethod]
		public void StringReturnsValueWhenValueIsEmptyString()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "EmptyString" }, null);
			Assert.AreEqual(string.Empty, configuration.EmptyString);
		}

		[TestMethod]
		public void StringReturnsValue()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "String" }, null);
			Assert.AreEqual("forty-two", configuration.String);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.AppSettingsKeyNotFound, "MissingString")]
		public void MissingStringThrowsException()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "MissingString" }, null);
		}
	}
}
