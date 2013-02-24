using System.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.ConfigurationSerializerTests
{
	[TestClass]
	public class DbConnectionTest
	{
		class Configuration
		{
			public Factory<DbConnection> TestConnectionString { get; private set; }
			public Factory<DbConnection> MissingConnectionString { get; private set; }
		}

		[TestMethod]
		public void CreatesDbConnectionFactory()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] { "TestConnectionString" }, null);

			Assert.IsInstanceOfType(configuration.TestConnectionString, typeof(Factory<DbConnection>));
		}

		[TestMethod]
		public void CanInstanceDbConnectionFactory()
		{
			var configuration = ConfigurationSerializer.Deserialize<Configuration>(null, new[] {"TestConnectionString"}, null);
			var instance = configuration.TestConnectionString.CreateInstance();

			Assert.IsNotNull(instance);
			Assert.IsInstanceOfType(instance, typeof(DbConnection));
			Assert.AreEqual("Driver=ODBCDriver;server=TestServer;", instance.ConnectionString);
		}

		[TestMethod, ExpectedExceptionMessage(typeof(ConfigurationSerializerException), ExceptionMessage.ConnectionStringNameNotFound, "MissingConnectionString")]
		public void MissingConnectionStringsThrowsException()
		{
			ConfigurationSerializer.Deserialize<Configuration>();				
		}
	}
}
