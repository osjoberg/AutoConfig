using System.Configuration;
using AutoConfig.ConfigSection;
using AutoConfig.SystemConfiguration;
using System.IO;
using System.Web.Hosting;

namespace AutoConfig
{
    /// <summary>
    /// AutoConfigManager is responsible for initialization of the AutoConfig framework.
    /// </summary>
    public static class AutoConfigManager
    {
        /// <summary>
        /// Lock used block parallell calls of the Initialize method.
        /// </summary>
        private static readonly object InitializeLock = new object();

        /// <summary>
        /// Configuration section information.
        /// </summary>
        private static readonly AutoConfigConfigurationSection ConfigurationSection = AutoConfigConfigurationSection.GetSection();

        /// <summary>
        /// Current environment information.
        /// </summary>
        public static EnvironmentInfo Environment { get; private set; }

        /// <summary>
        /// Initializes the framework.
        /// </summary>
        public static void Initialize()
        {
            Initialize(MachineInfo.Current);
        }
        
        /// <summary>
        /// Initializes the framework.
        /// </summary>
        /// <param name="machineInfo">Machine information to use for detection of environment.</param>
        internal static void Initialize(MachineInfo machineInfo)
        {
            ArgumentValidator.IsNotNull("machineInfo", machineInfo);

            if (ConfigurationSection != null)
                Initialize(EnvironmentInfo.AutoDetect(machineInfo, ConfigurationSection.Environments));
            else
                Initialize(EnvironmentInfo.DefaultEnvironment);
        }

        /// <summary>
        /// Initializes the framework.
        /// </summary>
        /// <param name="environmentName">Environment name to use for configuration.</param>
        public static void Initialize(string environmentName)
        {
            ArgumentValidator.IsNotNullAndNotEmpty("environmentName", environmentName);

            var environment = ConfigurationSection.Environments[environmentName];
            Initialize(EnvironmentInfo.FromEnvironmentConfigurationElement(environment));
        }

        /// <summary>
        /// Initializes the framework.
        /// </summary>
        /// <param name="environmentInfo">Environment information to use for configuration.</param>
        public static void Initialize(EnvironmentInfo environmentInfo)
        {
            ArgumentValidator.IsNotNull("environmentInfo", environmentInfo);

            lock (InitializeLock)
            {
                // Get original implementation instance.
                var original = InternalConfigSystem.GetOrginal();

                // If we are running on the default environment, make sure
                // we reset back to the original internal config system.
                if (environmentInfo == EnvironmentInfo.DefaultEnvironment)
                {
                    InternalConfigSystem.Install(original);
                }
                else
                {
                    // Get configuration.
                    var configuration = OpenMappedExeConfiguration(environmentInfo.File);

                    // Make sure the environment specific configuration file exists.
                    if (!configuration.HasFile)
                        throw new AutoConfigException(ExceptionMessage.CouldNotLoadEnvironmentSpecificConfigurationFile, environmentInfo.File);

                    // Install auto internal config system.
                    var auto = new AutoInternalConfigSystem(original, configuration);
                    InternalConfigSystem.Install(auto);
                }

                Environment = environmentInfo;
            }
        }

        /// <summary>
        /// Open mapped exe configuration.
        /// </summary>
        /// <param name="configurationFileName">Configuration file to open.</param>
        /// <returns>Configuration instance.</returns>
        private static Configuration OpenMappedExeConfiguration(string configurationFileName)
        {
            // If AppDomain is bein hosted in a web environment the 
            // current directory may not be set to application root.
            if (!Path.IsPathRooted(configurationFileName) && HostingEnvironment.IsHosted)                
                configurationFileName = HostingEnvironment.MapPath(configurationFileName);

            // Open mapped configuration.
            var map = new ExeConfigurationFileMap { ExeConfigFilename = configurationFileName };
            const ConfigurationUserLevel userLevel = ConfigurationUserLevel.None;
            return ConfigurationManager.OpenMappedExeConfiguration(map, userLevel);
        }
    }
}