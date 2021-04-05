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
                var callzz = BitConverter.ToString(memoryStream.GetBuffer());
                twentyoneHandler(callzz);
            }
        }

        public static void twentyoneHandler(string callz)
        {
            string[] callzArray = callz.Split(" ");
            if (callz.Contains("0h"))
            {
                Cosmos.System.Power.Reboot();
            }
            else if (callz.Contains("1h"))
            {
                Console.Read();
            }
            else if (callz.Contains("2h"))
            {
                for (var i = 0; i < callzArray.Length; i++)
                {
                    if (callzArray[i] == "2h")
                    {
                        Console.Write(callzArray[i + 1]);
                    }
                }
            }
            else if (callz.Contains("9h"))
            {
                for (var i = 0; i < callzArray.Length; i++)
                {
                    if (callzArray[i] == "9h")
                    {
                        Console.Write(callzArray[i + 1]);
                    }
                }
            }
            
        }
    }
}
