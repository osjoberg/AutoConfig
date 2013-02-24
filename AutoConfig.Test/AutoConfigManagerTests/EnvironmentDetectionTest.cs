using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.NetworkInformation;

namespace AutoConfig.Test.AutoConfigManagerTests
{
    [TestClass]
    public class EnvironmentDetectionTest
    {
        [TestMethod]
        public void IsIdentifyingCorrectEnvironmentByHost()
        {
            AutoConfigManager.Initialize(new MachineInfo("TestHost", "", null, null));
            Assert.AreEqual("Environment2", AutoConfigManager.Environment.Name);
        }

        [TestMethod]
        public void SupportsDefaultEnvironment()
        {
            AutoConfigManager.Initialize(new MachineInfo("UnknownMachine", "", null, null));
            Assert.AreEqual(EnvironmentInfo.DefaultEnvironment, AutoConfigManager.Environment);
        }

        [TestMethod]
        public void IsIdentifyingCorrectEnvironmentByIpAddress()
        {
            AutoConfigManager.Initialize(new MachineInfo("UnknownMachine", "", new[] { new IPAddress(new byte[] { 127, 0, 0, 1 }) }, null));
            Assert.AreEqual("Environment3", AutoConfigManager.Environment.Name);
        }

        [TestMethod]
        public void IsIdentifyingCorrectEnvironmentByDomain()
        {
            AutoConfigManager.Initialize(new MachineInfo("UnknownMachine", "TestDomain", null, null));
            Assert.AreEqual("Environment4", AutoConfigManager.Environment.Name);
        }

        [TestMethod]
        public void IsIdentifyingCorrectEnvironmentByMacAddress()
        {
            AutoConfigManager.Initialize(new MachineInfo("UnknownMachine", "", null, new[] { new PhysicalAddress(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }) } ));
            Assert.AreEqual("Environment5", AutoConfigManager.Environment.Name);
        }

		[TestMethod, ExpectedExceptionMessage(typeof(AutoConfigException), ExceptionMessage.OnlyOneMatchingEnvironmentMessage)]
		public void MultipleMatchingEnvironmentsThrowsException()
		{
			AutoConfigManager.Initialize(new MachineInfo("UnknownMachine", "TestDomain", null, new[] { new PhysicalAddress(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }) }));			
		}

		[TestMethod, ExpectedExceptionMessage(typeof(AutoConfigException), ExceptionMessage.CouldNotLoadEnvironmentSpecificConfigurationFile, "AutoConfig.Test.MissingFile.config")]
		public void EnvironmentMissingConfigurationFileThrowsException()
		{
			AutoConfigManager.Initialize(new MachineInfo("MissingFile", null, null, null));
		}
    }
}
