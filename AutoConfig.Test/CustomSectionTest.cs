using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace AutoConfig.Test
{
    [TestClass]
    public class CustomSectionTest
    {
        public CustomSectionTest()
        {
            AutoConfigManager.Initialize(new MachineInfo("TestHost", "", null, null));
        }

        [TestMethod]
        public void FindsNotOverriddenConfigurationSection()
        {
            Assert.AreEqual("It is not overridden.", ((TestConfigurationSection)ConfigurationManager.GetSection("notOverridden")).Test);
        }

        [TestMethod]
        public void FindsOverriddenConfigurationSection()
        {
            Assert.AreEqual("It is overridden.", ((TestConfigurationSection)ConfigurationManager.GetSection("overridden")).Test);
        }

        [TestMethod]
        public void FindsEnvironmentSpecificConfigurationSection()
        {
            Assert.AreEqual("It is environment specific.", ((TestConfigurationSection)ConfigurationManager.GetSection("environmentSpecific")).Test);
        }
    }
}
