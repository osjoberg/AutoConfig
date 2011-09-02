using System.Reflection;
using System.Configuration;
using System.Configuration.Internal;

namespace AutoConfig.SystemConfiguration
{
    /// <summary>
    /// Responsible for installing / uninstalling the proxy in the System.Config internal config 
    /// system in order to hook the original functionality.
    /// </summary>
    class InternalConfigSystem
    {
        /// <summary>
        /// Field information for the internal config system.
        /// </summary>
        private static readonly FieldInfo Field = GetInternalConfigSystemField();

        /// <summary>
        /// Reference to the original IInternalConfigSystem instance.
        /// </summary>
        private static readonly IInternalConfigSystem Original = GetInternalConfigSystem();

        /// <summary>
        /// Get internal config system field.
        /// </summary>
        /// <returns></returns>
        private static FieldInfo GetInternalConfigSystemField()
        {
            return typeof(ConfigurationManager).GetField("s_configSystem", BindingFlags.Static | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Get current internal config system.
        /// </summary>
        /// <returns></returns>
        private static IInternalConfigSystem GetInternalConfigSystem()
        {
            return (IInternalConfigSystem)Field.GetValue(null);
        }

        /// <summary>
        /// Installs a IInternalConfig system.
        /// </summary>
        /// <param name="internalConfigSystem">IInternalConfigSystem reference.</param>
        public static void Install(IInternalConfigSystem internalConfigSystem)
        {
            Field.SetValue(null, internalConfigSystem);
        }

        /// <summary>
        /// Get orgiginal internal config system.
        /// </summary>
        /// <returns></returns>
        public static IInternalConfigSystem GetOrginal()
        {
            return Original;
        }

    }
}
