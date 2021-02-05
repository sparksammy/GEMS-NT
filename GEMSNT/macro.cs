using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Micro4Win
{
    class Program
    {
        public class Micro
        {
            public static string contents = "";
            public static void welcome(string path)
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
                micro(path);

            }

            public static void startMicro(string pathb)
            {
                if (File.Exists(pathb))
                {
                    microLoader(pathb);
                }
                else
                {
                    welcome(pathb);
                }
            }

            public static void microLoader(string pathb)
            {
                //here we will open files and load them into the contents string.
                try
                {
                    var hello_text = File.ReadAllText(pathb);
                    contents = hello_text;
                    Console.WriteLine(hello_text);
                    micro(pathb);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            public static String micro(string path)
            {
                while (true)
                {
                    var keyInf = Console.ReadKey(true);
                    var currentKey = keyInf.Key;
                    var editmode = true;
                    var posLeftR = 0;
                    var posTopB = 0;
                    if (currentKey == ConsoleKey.Escape)
                    {
                        editmode = false;
                        Console.Clear();
                        Console.Write("CMD>");
                        var inputCMD = Console.ReadLine();
                        if (inputCMD == "q")
                        {
                            //quit
                            Environment.Exit(0);
                        }
                        else if (inputCMD == "w")
                        {
                            //write the contents to the file
                            File.WriteAllText(path, contents);
                            editmode = true;
                        }
                        else
                        {
                            //do nothing
                        }
                        Console.Clear();
                        Console.WriteLine(contents);
                    }
                    editmode = true;
                    if (editmode == true) {
                        if (keyInf.Key == ConsoleKey.Enter) {
                            Console.Write(Environment.NewLine);
                            posTopB += 1;
                            contents = contents + Environment.NewLine;
                        } else if (keyInf.Key == ConsoleKey.Backspace) {
                            contents = string.Concat(contents.SkipLast(3));
                            Console.Clear();
                            Console.WriteLine(contents);
                        } else {
                            Console.Write(keyInf.KeyChar);
                            posLeftR += 1;
                            contents = contents + keyInf.KeyChar;
                        }
                    }
                }
                return "micro=Nano+Vi"; //hacky fix to earlier problem, just in case it returns.
            }
            static void Main(string[] args)
            {
                Console.Write("File name:");
                var fPath = Console.ReadLine();
                startMicro(fPath);
            }
        }
    }
}


