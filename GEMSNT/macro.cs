using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Sys = Cosmos.System;

namespace GEMSNT
{
        

        public class Micro
        {
            static string contents = "";
            static string path = "";
            public static void welcome()
            {
            Console.Clear();
            Console.WriteLine("~");
            Console.WriteLine("~");
            Console.WriteLine("~");
            Console.WriteLine("~");
            Console.WriteLine("~                             micro - GEMS Text Editor");
            Console.WriteLine("~                           A simple MIV-like text editor.");
            Console.WriteLine("~                                  version 1.0");
            Console.WriteLine("~                                 by Samuel Lord");
            Console.WriteLine("~                                 License - MIT");
            Console.WriteLine("~");
            Console.WriteLine("~                      use q<enter>             to exit");
            Console.WriteLine("~                      use w<enter>            save to file");
            Console.WriteLine("~                     press <escape>             use commands");
            Console.WriteLine("~");
            Console.WriteLine("~");
            Console.WriteLine("~");
            Console.WriteLine("~");
            Console.Write("~                                 Press any key to continue...");
            Console.ReadKey(true); //any key to continue
            Console.Clear();
            micro();
            

        }

        public static void startMicro(string pathb) {
            if (File.Exists(pathb))
            {
                microLoader(pathb);
                welcome();
            } else
            {
                welcome();
            }
        }

        public static void microLoader(string pathb)
        {
            //here we will open files and load them into the contents string.
            try
            {
                var hello_text = File.ReadAllText(pathb);
                contents = hello_text;
                path = pathb;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static String micro()
        {
            while (true)
            {
                var keyInf = Console.ReadKey(true);
                var currentKey = keyInf.Key;
                if (currentKey == ConsoleKey.Escape)
                {
                    var keyInfCMD = Console.ReadKey(true);
                    var currentKeyCMD = keyInf.Key;
                    if (currentKeyCMD == ConsoleKey.Q)
                    {
                        //quit
                        return null;
                    } else if (currentKeyCMD == ConsoleKey.W)
                    {
                        //write the contents to the file
                        File.WriteAllText(path, contents);
                    } else
                    {
                        //do nothing
                    }
                } else
                {
                    Console.Write(keyInf.KeyChar);
                    contents = contents + keyInf.KeyChar;
                }
            }
        }

            

            
    }
}
