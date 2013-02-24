using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.AutoConfigManagerTests
{
    [TestClass]
    public class ConnectionStringsTest
    {
        public ConnectionStringsTest()
        {
            AutoConfigManager.Initialize(new MachineInfo("TestHost", "", null, null));
        }

        [TestMethod]
        public void FindsNotOverriddenConnectionString()
        {
            Assert.AreEqual("It is not overridden.", ConfigurationManager.ConnectionStrings["notOverriddenConnection"].ConnectionString);
        }

        [TestMethod]
        public void FindsOverriddenConnectionString()
        {
            Assert.AreEqual("It is overridden.", ConfigurationManager.ConnectionStrings["overriddenConnection"].ConnectionString);
        }

        [TestMethod]
        public void FindsEnvironmentSpecificConnectionString()
        {
            Assert.AreEqual("It is environment specific.", ConfigurationManager.ConnectionStrings["environmentSpecificConnection"].ConnectionString);
        }
	}
}
