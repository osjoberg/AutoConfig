using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.AutoConfigManagerTests
{
    [TestClass]
    public class AppSettingsTest
    {
        public AppSettingsTest()
        {
            AutoConfigManager.Initialize(new MachineInfo("TestHost", "", null, null));
        }

        [TestMethod]
        public void FindsNotOverriddenAppSetting()
        {
            Assert.AreEqual("It is not overridden.", ConfigurationManager.AppSettings["notOverridden"]);
        }

        [TestMethod]
        public void FindsOverriddenAppSetting()
        {
            Assert.AreEqual("It is overridden.", ConfigurationManager.AppSettings["overridden"]);
        }

        [TestMethod]
        public void FindsEnvironmentSpecificAppSetting()
        {
            Assert.AreEqual("It is environment specific.", ConfigurationManager.AppSettings["environmentSpecific"]);
        }

    }
}
