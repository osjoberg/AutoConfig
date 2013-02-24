using System.IO;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoConfig.Test.AutoConfigManagerTests
{
    [TestClass]
    public class EncryptionTest
    {
        public EncryptionTest()
        {
            // Encrypt appSettings section.
            var map = new ExeConfigurationFileMap { ExeConfigFilename = "AutoConfig.Test.Encryption.config" };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            var section = configuration.GetSection("appSettings");
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
		public void CanDecryptConfigurationSection()
        {
            Assert.AreEqual("It is decrypted.", ConfigurationManager.AppSettings["encrypted"]);
        }
    }
}
