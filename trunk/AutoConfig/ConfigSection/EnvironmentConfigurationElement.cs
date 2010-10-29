using System.Configuration;

namespace AutoConfig.ConfigSection
{
    /// <summary>
    /// Responsible for serialization/deserialization of the AutoConfig XML environment element.
    /// </summary>
    class EnvironmentConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Environment name.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name { get { return (string)this["name"]; } }

        /// <summary>
        /// Host filter.
        /// </summary>
        [ConfigurationProperty("host", IsRequired = false)]
        public string Host { get { return (string)this["host"]; } }

        /// <summary>
        /// Domain filter.
        /// </summary>
        [ConfigurationProperty("domain", IsRequired = false)]
        public string Domain { get { return (string)this["domain"]; } }

        /// <summary>
        /// IP address filter.
        /// </summary>
        [ConfigurationProperty("ipAddress", IsRequired = false)]
        public string IPAddress { get { return (string)this["ipAddress"]; } }

        /// <summary>
        /// Mac address filter.
        /// </summary>
        [ConfigurationProperty("macAddress", IsRequired = false)]
        public string MacAddress { get { return (string)this["macAddress"]; } }

        /// <summary>
        /// Configuration file.
        /// </summary>
        [ConfigurationProperty("file", IsRequired = true)]
        public string File { get { return (string)this["file"]; } }
    }
}