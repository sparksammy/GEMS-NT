using System;
using System.Collections.Generic;
using System.Text;

namespace GEMSNT.SamLangParser
{
    class SamLangParser
    {
        public static void parseSamLang(string line)
        {
            var samlangArgs = line.Split(' ');

            if (line.ToString().StartsWith("print-nlb"))
            {
                var printingNLB = line.ToString().Remove(0, 11);
                Console.Write(printingNLB);
            }
            else if (line.ToString().StartsWith("print"))
            {
                var printing = line.ToString().Remove(0, 6);
                Console.WriteLine(printing);
            }
            else if (line.ToString().StartsWith("beep"))
            {
                Console.Beep(1000, 530);
            }
            else if (line.ToString().StartsWith("beep-custom"))
            {
                Console.Beep(int.Parse(samlangArgs[1]), int.Parse(samlangArgs[2]));
            }
            else if (line.ToString().StartsWith("math+"))
            {
                Console.WriteLine(int.Parse(samlangArgs[1]) + int.Parse(samlangArgs[2]));
            }
            else if (line.ToString().StartsWith("math/"))
            {
                Console.WriteLine(int.Parse(samlangArgs[1]) / int.Parse(samlangArgs[2]));
            }
            else if (line.ToString().StartsWith("math*"))
            {
                Console.WriteLine(int.Parse(samlangArgs[1]) * int.Parse(samlangArgs[2]));
            }
            else if (line.ToString().StartsWith("math-"))
            {
                Console.WriteLine(int.Parse(samlangArgs[1]) - int.Parse(samlangArgs[2]));
            }
            else
            {
                Console.WriteLine("!!! ERROR - COMMAND NOT FOUND!!!");
            }
        }
    }
}
