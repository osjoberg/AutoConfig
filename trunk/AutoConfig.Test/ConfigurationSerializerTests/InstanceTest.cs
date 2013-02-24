using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	class BaseClass
	{
	}

	class DerivedClass : BaseClass
	{
	}

	class NotDerivedClass
	{
	}

	[TestClass]
	public class InstanceTest
	{	
		class ConfigurationInstance
		{
			public BaseClass ValidType { get; private set; }
			public BaseClass EmptyString { get; private set; }
		}

		class ConfigurationType
		{
			public Type ValidType { get; private set; }
		}

		class ConfigurationFactoryInstance
		{
			public Factory<BaseClass> ValidType { get; private set; }
		}

		class ConfigurationNotDerivedClass
		{
			public NotDerivedClass ValidType { get; private set; }
		}
 
		[TestMethod]
		public void ReturnsInstance()
		{
			var configuration = ConfigurationSerializer.Deserialize<ConfigurationInstance>(null, new[] { "ValidType" }, null);
			Assert.AreEqual(typeof(DerivedClass), configuration.ValidType.GetType());
		}

		[TestMethod]
		public void ReturnsNullInstance()
		{
			var configuration = ConfigurationSerializer.Deserialize<ConfigurationInstance>(null, new[] { "EmptyString" }, null);
			Assert.IsNull(configuration.EmptyString);
		}

		[TestMethod]
		public void ReturnsType()
		{
			var configuration = ConfigurationSerializer.Deserialize<ConfigurationType>();
			Assert.AreEqual(typeof(DerivedClass), configuration.ValidType);
		}

		[TestMethod]
		public void ReturnsFactoryInstance()
		{
			var configuration = ConfigurationSerializer.Deserialize<ConfigurationFactoryInstance>();
			Assert.AreEqual(typeof(DerivedClass), configuration.ValidType.CreateInstance().GetType());
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.CouldNotCastObjectOfTypeToType, typeof(NotDerivedClass), typeof(DerivedClass))]
		public void ThrowsExceptionWhenIncompatibleInstance()
		{
			ConfigurationSerializer.Deserialize<ConfigurationNotDerivedClass>();
		}
	}
}
