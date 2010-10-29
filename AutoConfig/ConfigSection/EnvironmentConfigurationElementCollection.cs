using System.Configuration;

namespace AutoConfig.ConfigSection
{
    /// <summary>
    /// Responsible for serialization/deserialization of the list of XML environment elements.
    /// </summary>
    class EnvironmentConfigurationElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Create a new configuration element.
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentConfigurationElement();
        }

        /// <summary>
        /// Retrieves an elements key.
        /// </summary>
        /// <param name="element">Element.</param>
        /// <returns>Key of element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentConfigurationElement)element).Name;
        }

        /// <summary>
        /// Shadows the original this implementation in order to be type safe.
        /// </summary>
        /// <param name="name">Key of the configuration element to retrieve.</param>
        /// <returns>Configuration element instance.</returns>
        new public EnvironmentConfigurationElement this[string name]
        {
            get { return (EnvironmentConfigurationElement)BaseGet(name); }
        }
    }
}
