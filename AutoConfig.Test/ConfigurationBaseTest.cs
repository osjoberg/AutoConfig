using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.Common;
using System.Data.Odbc;
using System.Collections.Generic;
using System;

namespace AutoConfig.Test
{
    [TestClass]
    public class ConfigurationBaseTest
    {
        public ConfigurationBaseTest()
        {
            AutoConfigManager.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestInt32ThrowsExceptionWhenValueIsEmptyString()
        {
            Assert.AreEqual(-1, Configuration.TestInt32ThrowsExceptionWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestInt32ReturnsValue()
        {
            Assert.AreEqual(42, Configuration.TestInt32ReturnsValue);
        }

        [TestMethod]
        public void TestNullableInt32ReturnsNullWhenValueIsEmptyString()
        {
            Assert.IsNull(Configuration.TestNullableInt32ReturnsNullWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestNullableInt32ReturnsValue()
        {
            Assert.AreEqual(42, Configuration.TestNullableInt32ReturnsValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingInt32ThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingInt32ThrowsException);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingNullableInt32ThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingNullableInt32ThrowsException);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDoubleThrowsExceptionWhenValueIsEmptyString()
        {
            Assert.AreEqual(-1, Configuration.TestDoubleThrowsExceptionWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestDoubleReturnsValue()
        {
            Assert.AreEqual(42.42, Configuration.TestDoubleReturnsValue);
        }

        [TestMethod]
        public void TestDoubleReturnsValueOtherCulture()
        {
            Configuration.Initialize(new CultureInfo("sv-se"));
            Assert.AreEqual(42.42, Configuration.TestDoubleReturnsValueOtherCulture);
            AutoConfigManager.Initialize();
        }

        [TestMethod]
        public void TestNullableDoubleReturnsNullWhenValueIsEmptyString()
        {
            Assert.IsNull(Configuration.TestNullableDoubleReturnsNullWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestNullableDoubleReturnsValue()
        {
            Assert.AreEqual(42.42, Configuration.TestNullableDoubleReturnsValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingDoubleThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingDoubleThrowsException);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingNullableDoubleThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingNullableDoubleThrowsException);
        }
        
        [TestMethod]
        public void TestStringReturnsValueWhenValueIsEmptyString()
        {
            Assert.AreEqual(string.Empty, Configuration.TestStringReturnsValueWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestStringReturnsValue()
        {
            Assert.AreEqual("forty-two", Configuration.TestStringReturnsValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingStringThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingStringThrowsException);
        }

        [TestMethod]
        public void TestConnectionStringInstance()
        {
            DbConnection instance = Configuration.TestConnectionString;
            Assert.IsInstanceOfType(instance, typeof(OdbcConnection));
            Assert.AreEqual("Driver=ODBCDriver;server=TestServer;", instance.ConnectionString);
        }
    }
}
