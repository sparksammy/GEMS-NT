using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.HAL.Drivers;
using Cosmos.HAL.Drivers.PCI.Network;
using Cosmos.HAL.Network;

namespace GEMSNT.Networking
{
    public class Networking
    {
        byte[] mac = MACAddress.Broadcast.bytes;
        public static PCIDevice dev;
        public static bool netAvail = false;
        public static bool isNetworkingAvailable()
        {
            dev = PCI.GetDevice(VendorID.AMD, DeviceID.PCNETII);
            if (dev != null)
            {
                netAvail = true;
                return true;
            } else
            {
                netAvail = false;
                return false;
            }
        }

        public static string GetMACAddress()
        {
            if (netAvail) {
                AMDPCNetII nic = new AMDPCNetII(dev);
                return nic.MACAddress.ToString();
            } else
            {
                return "Fail. (Are you sure you have an AMD PCnet-PCI II installed?)";
            }
        }
    }
}
