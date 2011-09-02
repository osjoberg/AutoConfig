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
        [ExpectedException(typeof(FormatException))]
        public void TestEnumThrowsExceptionWhenValueIsEmptyString()
        {
            Assert.AreEqual(-1, Configuration.TestEnumThrowsExceptionWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestEnumReturnsValue()
        {
            Assert.AreEqual(TestEnum.One, Configuration.TestEnumReturnsValue);
        }

        [TestMethod]
        public void TestNullableEnumReturnsNullWhenValueIsEmptyString()
        {
            Assert.IsNull(Configuration.TestNullableEnumReturnsNullWhenValueIsEmptyString);
        }

        [TestMethod]
        public void TestNullableEnumReturnsValue()
        {
            Assert.AreEqual(TestEnum.One, Configuration.TestNullableEnumReturnsValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingEnumThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingEnumThrowsException);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestMissingNullableEnumThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestMissingNullableEnumThrowsException);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestEnumOutOfRangeThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestEnumOutOfRange);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestEnumInvalidValueThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestEnumInvalidValue);
        }

        [TestMethod]
        public void TestEnumReturnsValueWhenInt()
        {
            Assert.AreEqual(TestEnum.One, Configuration.TestEnumReturnsValueWhenInt);
        }

        [TestMethod]
        public void TestEnumReturnsValueWhenCasesMismatch()
        {
            Assert.AreEqual(TestEnum.One, Configuration.TestEnumReturnsValueWhenCasesMismatch);
        }


        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestEnumUndefinedValueThrowsException()
        {
            Assert.AreEqual(-1, Configuration.TestEnumUndefinedValueThrowsException);
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
