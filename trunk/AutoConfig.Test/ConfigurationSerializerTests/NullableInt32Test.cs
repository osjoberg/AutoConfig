using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class NullableInt32Test
	{
		class Configuration
		{
			public int? Int32 { get; private set; }
			public int? EmptyString { get; private set; }
			public int? MissingInt32 { get; private set; }
		}

		[TestMethod]
		public void NullableInt32ReturnsValue()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "Int32" }, null);
			Assert.AreEqual(42, configuration.Int32);
		}

		[TestMethod]
		public void NullableInt32ReturnsNullWhenValueIsEmptyString()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "EmptyString" }, null);
			Assert.IsNull(configuration.EmptyString);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.AppSettingsKeyNotFound, "MissingInt32")]
		public void MissingNullableInt32ThrowsException()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "MissingInt32" }, null);
		}
	}
}
