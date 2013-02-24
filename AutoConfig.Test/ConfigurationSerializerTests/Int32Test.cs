using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class Int32Test
	{
		class Configuration
		{
			public int Int32 { get; private set; }
			public int EmptyString { get; private set; }
			public int MissingInt32 { get; private set; }
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.ValueCouldNotBeConvertedToType, "", typeof(int))]
		public void Int32ThrowsExceptionWhenValueIsEmptyString()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "EmptyString" }, null);
		}

		[TestMethod]
		public void Int32ReturnsValue()
		{ 
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "Int32" }, null);
			Assert.AreEqual(42, configuration.Int32);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.AppSettingsKeyNotFound, "MissingInt32")]
		public void MissingInt32ThrowsException()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "MissingInt32" }, null);
		}
	}
}
