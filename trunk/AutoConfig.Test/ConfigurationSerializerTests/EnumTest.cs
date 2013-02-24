using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class EnumTest
	{
		enum TestEnum
		{
			One = 1,
		}

		class Configuration
		{
			public TestEnum MissingEnum { get; private set; }
			public TestEnum UndefinedNumericEnum { get; private set; }
			public TestEnum StringEnumWrongCase { get; private set; }
			public TestEnum StringEnum { get; private set; }
			public TestEnum NumericEnum { get; private set; }
			public TestEnum UndefinedStringEnum { get; private set; }
			public TestEnum TestEnumThrowsExceptionWhenValueIsEmptyString { get; private set; }
			public TestEnum TestMissingEnumThrowsException { get; private set; }
			public TestEnum EmptyString { get; private set; }
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.AppSettingsKeyNotFound, "MissingEnum")]
		public void MissingEnumThrowsException()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "MissingEnum" }, null);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.ValueIsNotDefinedInEnumOfType, "", typeof(TestEnum))]
		public void ThrowsExceptionWhenValueIsEmptyString()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "EmptyString" }, null);
		}

		[TestMethod]
		public void ReturnsValueWhenValueIsString()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "StringEnum" }, null);
			Assert.AreEqual(TestEnum.One, configuration.StringEnum);
		}

		[TestMethod]
		public void ReturnsValueWhenValueIsNumeric()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "NumericEnum" }, null);
			Assert.AreEqual(TestEnum.One, configuration.NumericEnum);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.ValueIsNotDefinedInEnumOfType, "3", typeof(TestEnum))]
		public void ThrowsExceptionWhenUndefinedNumericEnum()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "UndefinedNumericEnum" }, null);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.ValueIsNotDefinedInEnumOfType, "Three", typeof(TestEnum))]
		public void ThrowsExceptionWhenUndefinedStringEnum()
		{
			ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "UndefinedStringEnum" }, null);
		}

		[TestMethod]
		public void ReturnsValueWhenCasingMismatch()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "StringEnumWrongCase" }, null);
			Assert.AreEqual(TestEnum.One, configuration.StringEnumWrongCase);
		}
	}
}
