using System.Configuration;
using System.Globalization;

namespace AutoConfig.ConfigSection
{
    /// <summary>
    /// Responsible for serialization/deserialization of the AutoConfig XML configuration section.
    /// </summary>
    class AutoConfigConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Get AutoConfig section.
        /// </summary>
        /// <returns>Deserialized AutoConfig section if it exists, or null if it does not exist.</returns>
        public static AutoConfigConfigurationSection GetSection()
        {
            return (AutoConfigConfigurationSection)ConfigurationManager.GetSection("autoConfig");
        }

        /// <summary>
        /// Environments.
        /// </summary>
        [ConfigurationProperty("environments", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(EnvironmentConfigurationElement))]
        public EnvironmentConfigurationElementCollection Environments
        {
            get { return (EnvironmentConfigurationElementCollection)this["environments"]; }
        }

        /// <summary>
        /// Configuration culture.
        /// </summary>
        [ConfigurationProperty("culture")]
        public CultureInfo Culture
        {
            get { return (CultureInfo)this["culture"]; }            
        }
    }
}
