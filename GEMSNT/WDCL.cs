using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//Windows-like Dos Compatibility Layer

namespace GEMSNT.WDCL
{
    public class WDCL
    {
        public static void launchExeOld(byte bin)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(bin);
        }


        public static void launchExe(byte[] bin)
        {
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(bin);
            while (true)
            {
                var callzz = memoryStream.ToString();
                twentyoneHandler(callzz);
            }
        }

        public static void twentyoneHandler(string callz)
        {
            if (callz.Contains("00h"))
            {
                Cosmos.System.Power.Reboot();
            }
            else if (callz.Contains("01h"))
            {
                Console.Read();
            }
            else if (callz.Contains("02h"))
            {
                Console.Write(callz.Replace("02h", ""));
            }
            else if (callz.Contains("09h"))
            {
                Console.WriteLine(callz.Replace("09h", ""));
            }
            else if (callz.Contains("0Fh"))
            {
                var noCall = callz.Replace("0Fh", "");
                File.ReadAllText(noCall);
            }
        }
    }
}
