using System.IO;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test
{
    [TestClass]
    public class EncryptionTest
    {
        public EncryptionTest()
        {
            // Encrypt appSettings section.
            ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = "AutoConfig.Test.Encryption.config" };
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            ConfigurationSection section = configuration.GetSection("appSettings");
            section.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
            configuration.Save();

            AutoConfigManager.Initialize(new MachineInfo("EncryptedHost", "", null, null));
        }

        [TestMethod]
        public void IsConfigurationSectionEncrypted()
        {
            Assert.IsFalse(File.ReadAllText("AutoConfig.Test.Encryption.config").Contains("It is decrypted."));
        }

        [TestMethod]
        public void IsDecryptingConfigurationSectionPossible()
        {
            Assert.AreEqual("It is decrypted.", ConfigurationManager.AppSettings["encrypted"]);
        }
    }
}
