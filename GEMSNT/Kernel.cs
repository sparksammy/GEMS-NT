using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace GEMSNT
{
    public class Kernel : Sys.Kernel
    {
        string versionSTR = "0.3";
        protected override void BeforeRun()
        {
            Console.WriteLine("Welcome to GEMS NT!");
        }

        protected override void Run()
        {
            while (1 == 1)
            {
                Console.Write("RUSHELL> ");
                var cmd = Console.ReadLine();
                Console.Write("*RUSHELL DEBUG* Command typed: ");
                Console.WriteLine(cmd);
                if (cmd == "clear") {
                    Console.Clear();
                } else if (cmd == "about") {
                    Console.WriteLine("GEMS NT Version" + versionSTR);
                } else if (cmd.ToString().Contains("echo")) {
                    var echoing = cmd.ToString().Remove(0, 4);
                    Console.WriteLine(echoing);
                } else if (cmd == "cmds" || cmd == "help") {
                    Console.WriteLine("GEMS NT - Commands");
                    Console.WriteLine("---");
                    Console.WriteLine("about - about this copy of GEMS.");
                    Console.WriteLine("echo - echos back value you pass to it");
                    Console.WriteLine("clear - clears screen");
                    Console.WriteLine("cmds/help - this.");
                } else {
                    Console.WriteLine("Command not found. (Pro tip: cmds for commands!)");
                }
            }
        }
    }
}
