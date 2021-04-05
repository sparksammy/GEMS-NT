using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GEMSNT.GEMSBinaryLoader
{
    class GemsBin
    {
        public static bool LoadGemsBin(string path)
        {
            try
            {
                if (path.EndsWith(".bin"))
                {
                    byte[] bytes = File.ReadAllBytes(path);
                    return true;
                } else
                {
                    Console.WriteLine("Not a .bin file");
                    return false;
                }
            } catch
            {
                Console.WriteLine("Generic error. (Too big?)");
                return false;
            }
        }
    }
}
