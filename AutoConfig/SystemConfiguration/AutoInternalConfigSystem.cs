using System.Configuration;
using System.Configuration.Internal;
using System.Collections.Specialized;

namespace AutoConfig.SystemConfiguration
{
    /// <summary>
    /// Proxy used to hook calls to the internal System.Configuration config system.
    /// </summary>
    class AutoInternalConfigSystem : IInternalConfigSystem
    {
        /// <summary>
        /// Internal config system reference.
        /// </summary>
        private readonly IInternalConfigSystem _internalConfigSystem;

        /// <summary>
        /// Environment configuration reference.
        /// </summary>
        private readonly Configuration _environmentConfiguration;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="internalConfigSystem">Internal config system reference.</param>
        /// <param name="environmentConfiguration">Environment configuration reference.</param>
        public AutoInternalConfigSystem(IInternalConfigSystem internalConfigSystem, Configuration environmentConfiguration)
        {
            this._internalConfigSystem = internalConfigSystem;
            this._environmentConfiguration = environmentConfiguration;
        }

        /// <summary>
        /// Refresh configuration.
        /// </summary>
        /// <param name="sectionName">Section name to refresh.</param>
        public void RefreshConfig(string sectionName)
        {
            _internalConfigSystem.RefreshConfig(sectionName);
        }

        /// <summary>
        /// Supports user config.
        /// </summary>
        public bool SupportsUserConfig
        {
            get { return _internalConfigSystem.SupportsUserConfig; }
        }

        /// <summary>
        /// Get configuration section.
        /// </summary>
        /// <param name="configKey">Key of the section to get.</param>
        /// <returns>Section for the corresponding key.</returns>
        public object GetSection(string configKey)
        {
            if (configKey == "appSettings")
                return BuildAppSettings();
            if (configKey == "connectionStrings")
                return BuildConnectionStrings();
            if (configKey == "system.codedom")
                return _internalConfigSystem.GetSection(configKey);

            return _environmentConfiguration.GetSection(configKey) ?? _internalConfigSystem.GetSection(configKey);
        }

        /// <summary>
        /// Get internal application settings.
        /// </summary>
        /// <returns>Application settings collection.</returns>
        private NameValueCollection GetInternalAppSettings()
        {
            return (NameValueCollection)_internalConfigSystem.GetSection("appSettings");
        }

        /// <summary>
        /// Get environment specific application settings.
        /// </summary>
        /// <returns>Application settings collection.</returns>
        private KeyValueConfigurationCollection GetEnvironmentAppSettings()
        {
            return _environmentConfiguration.AppSettings.Settings;
        }

        /// <summary>
        /// Build application settings by combining application settings 
        /// with environment specific application settings.
        /// </summary>
        /// <returns>Application settings collection.</returns>
        private NameValueCollection BuildAppSettings()
        {
            var newAppSettings = new NameValueCollection(GetInternalAppSettings());
            foreach (KeyValueConfigurationElement setting in GetEnvironmentAppSettings())
                newAppSettings[setting.Key] = setting.Value;

            return newAppSettings;
        }

        /// <summary>
        /// Get internal connection strings.
        /// </summary>
        /// <returns>Internal connection string.</returns>
        private ConnectionStringSettingsCollection GetInternalConnectionStrings()
        {
            var connectionStringsSection = _internalConfigSystem.GetSection("connectionStrings");
            return ((ConnectionStringsSection)connectionStringsSection).ConnectionStrings;
        }

        /// <summary>
        /// Get environment specific connection strings.
        /// </summary>
        /// <returns></returns>
        private ConnectionStringSettingsCollection GetEnvironmentConnectionStrings()
        {
            return _environmentConfiguration.ConnectionStrings.ConnectionStrings;
        }
        
        /// <summary>
        /// Build connection strings.
        /// </summary>
        /// <returns>Connection strings collection.</returns>
        private object BuildConnectionStrings()
        {
            // Create a new section.
            var newSection = new ConnectionStringsSection();
            foreach (ConnectionStringSettings setting in GetInternalConnectionStrings())
                newSection.ConnectionStrings.Add(setting);

            // Append new section information.
            foreach (ConnectionStringSettings setting in GetEnvironmentConnectionStrings())
            {
                if (newSection.ConnectionStrings[setting.Name] != null)
                    newSection.ConnectionStrings.Remove(setting.Name);

                newSection.ConnectionStrings.Add(setting);
            }

            return newSection;
        }
    }
}
