using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Generic;
using AutoConfig.ConfigSection;

namespace AutoConfig
{
    /// <summary>
    /// Supports environment information and automatic environment detection.
    /// </summary>
    public class EnvironmentInfo
    {
        /// <summary>
        /// Default environment.
        /// </summary>
        private static readonly EnvironmentInfo defaultEnvironment = new EnvironmentInfo("<Default Environment>");

        /// <summary>
        /// Internal constructor.
        /// </summary>
        internal EnvironmentInfo(string name, string host, string domain, string ipAddress, string macAddress, string file) : this(name)
        {
            Domain = domain;
            Host = host;
            IPAddress = ipAddress;
            MacAddress = macAddress;
            File = file;
        }

        /// <summary>
        /// Internal constructor.
        /// </summary>
        internal EnvironmentInfo(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Default environment.
        /// </summary>
        public static EnvironmentInfo DefaultEnvironment { get { return defaultEnvironment; } }

        /// <summary>
        /// Environment name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Regular expression for matching host name with environment.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Regular expression for matching domain name with environment.
        /// </summary>
        public string Domain { get; private set; }
        
        /// <summary>
        /// Regular expression for matching IP address name with environment.
        /// </summary>
        public string IPAddress { get; private set; }

        /// <summary>
        /// Regular expression for matching Mac address name with environment.
        /// </summary>
        public string MacAddress { get; private set; }

        /// <summary>
        /// Configuration file name for environment.
        /// </summary>
        public string File { get; private set; }

        /// <summary>
        /// Convert an EnvironmentConfigurationElement to an EnvironmentInfo instance.
        /// </summary>
        /// <param name="environment">Instance to convert.</param>
        /// <returns>Converted instance.</returns>
        internal static EnvironmentInfo FromEnvironmentConfigurationElement(EnvironmentConfigurationElement environment)
        {
            if (environment == null)
                return DefaultEnvironment;

            return new EnvironmentInfo(environment.Name, environment.Domain, environment.Host, 
                                       environment.IPAddress, environment.MacAddress, environment.File);
        }

        /// <summary>
        /// Auto detect environment.
        /// </summary>
        /// <param name="machineInformation">Machine ifnormation.</param>
        /// <param name="environments">Environment information.</param>
        /// <returns>Detected environment.</returns>
        internal static EnvironmentInfo AutoDetect(MachineInfo machineInformation, 
                                                   EnvironmentConfigurationElementCollection environments)
        {
            EnvironmentConfigurationElement determinedEnvironment = null;

            // Process all environments...
            foreach (EnvironmentConfigurationElement environment in environments)
            {
                // Match environment filters with machine properties.
                if (!IsMatch(new[] { machineInformation.Host }, environment.Host))
                    continue;
                if (!IsMatch(new[] { machineInformation.Domain }, environment.Domain))
                    continue;
                if (!IsMatch(machineInformation.IPAdresses, environment.IPAddress))
                    continue;
                if (!IsMatch(machineInformation.MacAddresses, environment.MacAddress))
                    continue;

                // Make sure there are not more than one matching environment.
                if (determinedEnvironment != null)
                    throw new ConfigurationErrorsException(Resources.OnlyOneMatchingEnvironmentMessage);

                determinedEnvironment = environment;
            }

            return FromEnvironmentConfigurationElement(determinedEnvironment);
        }

        /// <summary>
        /// Match an array of inputs with a regular expression.
        /// </summary>
        /// <typeparam name="TType">Type of input array.</typeparam>
        /// <param name="inputs">Inputs to match.</param>
        /// <param name="pattern">Pattern to match with.</param>
        /// <returns>Returns true if there is a match of if no pattern is set.</returns>
        internal static bool IsMatch<TType>(IEnumerable<TType> inputs, string pattern)
        {
            if (pattern.Length == 0)
                return true; 

            foreach (TType input in inputs)
                if (Regex.IsMatch(input.ToString(), pattern))
                    return true;

            return false;
        }
    }
}
