using System.Net.NetworkInformation;
using System.Collections.ObjectModel;
using System.Net;
using System.Collections.Generic;

namespace AutoConfig
{
    class MachineInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public MachineInfo(string host, string domain, IPAddress[] ipAddresses, PhysicalAddress[] physicalAddresses)
        {
            Host = host;
            Domain = domain;
            IPAdresses = new ReadOnlyCollection<IPAddress>(ipAddresses ?? new IPAddress[] { });
            MacAddresses = new ReadOnlyCollection<PhysicalAddress>(physicalAddresses ?? new PhysicalAddress[] { });
        }

        /// <summary>
        /// Returns current machine information.
        /// </summary>
        public static MachineInfo Current
        {
            get
            {  
                // Get host name, domain name and IP addresses.
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                IPAddress[] ipAdresses = Dns.GetHostEntry(ipProperties.HostName).AddressList;

                // Get MAC addresses.
                var physicalAddresses = new List<PhysicalAddress>();
                foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                    if (!physicalAddress.Equals(PhysicalAddress.None))
                        physicalAddresses.Add(physicalAddress);
                }

                return new MachineInfo(ipProperties.HostName, ipProperties.DomainName, ipAdresses, physicalAddresses.ToArray());
            }
        }

        /// <summary>
        /// Host name.
        /// </summary>
        public string Host { get; private set; }

        /// <summary>
        /// Domain name.
        /// </summary>
        public string Domain { get; private set; }

        /// <summary>
        /// IP-adresses.
        /// </summary>
        public ReadOnlyCollection<IPAddress> IPAdresses { get; private set; }

        /// <summary>
        /// MAC-adresses.
        /// </summary>
        public ReadOnlyCollection<PhysicalAddress> MacAddresses { get; private set; }
    }
}
