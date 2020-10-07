using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;
using GEMSNT.Networking;
using GEMSNT.PCSpeaker;
using GEMSNT.Wait;
using cc = GEMSNT.ConsoleColorz;
using System.ComponentModel;
using Cosmos.System.Graphics;
using System.Drawing;
using mouse = Cosmos.System.MouseManager;

namespace GEMSNT
{
    public class Kernel : Sys.Kernel
    {
        string versionSTR = "0.532prebeta";

        Sys.FileSystem.CosmosVFS fs;

        string current_directory = "0:\\";
        string currDir = "";
        public static string file;

        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();
        }

        public void dir()
        {
            string[] dirs = Directory.GetDirectories(current_directory);
            string[] files = Directory.GetFiles(current_directory);
            Console.WriteLine("Dirs:");
            Console.WriteLine("---");
            foreach (var item in dirs)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---");
            Console.WriteLine("Files:");
            Console.WriteLine("---");
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---");
        }

        public void mkdir(string dir2make)
        {
            try
            {
                fs.CreateDirectory(dir2make);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void cd(string dirCD)
        {
            if (fs.IsValidDriveId(dirCD.Replace(":\\", "")) || fs.IsValidDriveId(dirCD.Replace(":", "")))
            {
                current_directory = dirCD;
            }
            else
            {
                if (dirCD == "..")
                {
                    string prevDirTemp = current_directory.ToString().Replace(currDir.ToString() + "\\", "\\");
                    Console.WriteLine(prevDirTemp.ToString());
                    current_directory = prevDirTemp.ToString();
                    currDir = fs.GetDirectory(prevDirTemp).mName.ToString();
                }
                else
                {
                    currDir = dirCD.ToString();
                    if (fs.GetDirectory(current_directory + dirCD) != null)
                    {
                        current_directory = current_directory + dirCD + "\\";
                    }
                    else
                    {
                        Console.WriteLine("!!! Directory not found. !!!");
                    }
                }
            }
        }

        public void del(string fileOrDir)
        {
            try
            {
                fs.DeleteDirectory(fs.GetDirectory(fileOrDir));
            }
            catch (Exception e)
            {
                try
                {
                    File.Delete(fileOrDir);
                }
                catch
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public string readFile(string dogFile)
        {
            try
            {
                var hello_file = fs.GetFile(current_directory + dogFile);
                var hello_file_stream = hello_file.GetFileStream();

                if (hello_file_stream.CanRead)
                {
                    byte[] text_to_read = new byte[hello_file_stream.Length];
                    hello_file_stream.Read(text_to_read, 0, (int)hello_file_stream.Length);
                    return Encoding.Default.GetString(text_to_read);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }

        public void listDisk()
        {
            foreach (var item in fs.GetVolumes())
            {
                Console.WriteLine(item);
            }
        }

        public string writeLineToFile(string echoFile, string writeContents)
        {
            try
            {
                using (StreamWriter w = File.AppendText(current_directory + echoFile))
                {
                    return writeContents;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "";
        }

        Canvas canvas;
        public void initGUI()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();

            canvas.Clear(Color.Blue);
        }

        public void clearGUI(System.Drawing.Color color)
        {
            canvas.Clear(color);
        }

        public void createCursor(System.Drawing.Color color)
        {
            try
            {
                Pen pen = new Pen(color);
                canvas.DrawRectangle(pen, mouse.X, mouse.Y, mouse.X - 5, mouse.Y - 5);
            }
            catch
            {
                Console.WriteLine("Error making cursor.");
            }
        }

        public bool isClicked(int x1, int y1, int x2, int y2, Sys.MouseState state)
        {
            //Example of state: Sys.MouseState.Left
            if (mouse.MouseState == state && mouse.X < x1 && mouse.X > x2 && mouse.Y < y1 && mouse.Y > y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void drawElement(string type, int x1, int y1, int x2, int y2, System.Drawing.Color color)
        {
            Pen pen = new Pen(color);
            if (type == "line")
            {
                try
                {
                    canvas.DrawLine(pen, x1, y1, x2, y2);
                }
                catch
                {
                    Console.WriteLine("Error drawing line. Unknown error.");
                }
            }
            else if (type == "rectangle")
            {
                try
                {
                    canvas.DrawRectangle(pen, x1, y1, x2, y2);
                }
                catch
                {
                    Console.WriteLine("Error rectangle line. Unknown error.");
                }
            }
            else
            {
                Console.WriteLine("Error drawing. Unknown type.");
            }
        }

        public void setRes(int width, int height, ColorDepth colordepth)
        {
            canvas.Mode = new Mode(width, height, ColorDepth.ColorDepth32); //COSMOS only supports 32bit color depth. This is why we must use it.
            //Also, we can *try* higher resolutions, but COSMOS community recommends sticking with 800x600 and 640x480.
        }

        public void drawImageFromBase64(string base64file, int x, int y)
        {
            Bitmap bmp = new Bitmap(Convert.FromBase64String(base64file));

            Image img = (Image)bmp;

            canvas.DrawImage(img, x, y);
        }

        protected override void Run()
        {
            PCSpeaker.PCSpeaker.play(PCSpeaker.PCSpeaker.notes.CS3, 400);
            Console.Clear();
            Console.WriteLine("Welcome to GEMS NT!");
            while (1 == 1)
            {
                Console.Write(current_directory + " $> ");
                var cmd = Console.ReadLine();
                var args = cmd.Split(' ');
                //Console.Write("*RUSHELL DEBUG* Command typed: ");
                //Console.WriteLine(cmd);
                if (cmd == "clear")
                {
                    Console.Clear();
                }
                else if (cmd == "lose")
                {
                    initGUI();
                    drawElement("rectangle",0,0,20,10,Color.Red);
                    createCursor(Color.Green);
                    if (isClicked(0,0,20,10,Sys.MouseState.Left))
                    {
                        clearGUI(Color.Red);
                    }
                }
                else if (cmd == "reboot")
                {
                    Console.Clear();
                    Console.WriteLine("System now rebooting...");
                    Cosmos.System.Power.Reboot();
                }
                else if (cmd.StartsWith("micro"))
                {
                    var path = args[1];
                    Micro.startMicro(path);
                }
                else if (cmd == "shutdown")
                {
                    Console.Clear();
                    Console.WriteLine("System now shutting down...");
                    Cosmos.System.Power.Shutdown();
                }
                else if (cmd == "time")
                {
                    var time = Cosmos.HAL.RTC.Hour.ToString() + ":" + Cosmos.HAL.RTC.Minute.ToString() + ":" + Cosmos.HAL.RTC.Second.ToString();
                    Console.WriteLine(time);
                }
                else if (cmd == "date")
                {
                    var time = Cosmos.HAL.RTC.Month.ToString() + "-" + Cosmos.HAL.RTC.DayOfTheMonth.ToString() + "-" + Cosmos.HAL.RTC.Year.ToString();
                    Console.WriteLine(time);
                }
                else if (cmd == "about")
                {
                    long available_space = fs.GetAvailableFreeSpace("0:/");
                    string fs_type = fs.GetFileSystemType("0:/");
                    Console.WriteLine("GEMS NT Version: " + versionSTR);
                    if (fs_type != null)
                    {
                        Console.WriteLine("File System Type on main drive: " + fs_type);
                        Console.WriteLine("Available Free Space on main drive: " + available_space);
                    }
                    else
                    {
                        Console.WriteLine("File System Type on main drive: NONE");
                        Console.WriteLine("Available Free Space on main drive: 0");
                    }
                }
                else if (cmd == "rainbowConnection")
                {
                    Console.WriteLine("Why are there so many songs about rainbows,");
                    Console.WriteLine("and what's on the other side?");
                    Console.WriteLine("--Kermit");
                    Console.Beep(700, 400);
                    Console.Beep(894, 400);
                    Console.Beep(990, 600);
                    Console.Beep(950, 400);
                    Console.Beep(900, 400);
                    Console.Beep(1000, 600);
                    Console.Beep(666, 800);
                    Console.Beep(700, 600);
                    Console.Beep(861, 600);
                    Console.Beep(925, 600);
                    Console.Beep(950, 400);
                    Console.Beep(975, 600);
                    Console.Beep(942, 400);
                    Console.Beep(875, 400);
                    Console.Beep(800, 600);
                    Console.Beep(942, 400);
                    Console.Beep(990, 600);
                    Console.Beep(800, 600);
                }
                else if (cmd == "dir")
                {
                    string[] dirs = Directory.GetDirectories(current_directory);
                    string[] files = Directory.GetFiles(current_directory);
                    Console.WriteLine("Dirs:");
                    Console.WriteLine("---");
                    foreach (var item in dirs)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("---");
                    Console.WriteLine("Files:");
                    Console.WriteLine("---");
                    foreach (var item in files)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("---");
                }
                else if (cmd.ToString().StartsWith("echo"))
                {
                    var echoing = cmd.ToString().Remove(0, 5);
                    Console.WriteLine(echoing);
                }
                else if (cmd.ToString().StartsWith("cd"))
                {
                    var dirCD = cmd.ToString().Remove(0, 3);
                    cd(dirCD);
                }
                else if (cmd.ToString().StartsWith("setvol"))
                {
                    cd(args[1]);
                }
                else if (cmd.ToString().StartsWith("listvol"))
                {
                    listDisk();
                }
                else if (cmd.ToString().StartsWith("del"))
                {
                    del(args[1]);
                }
                else if (cmd.ToString().StartsWith("ddir"))
                {
                    del(args[1]);
                }
                else if (cmd.ToString().StartsWith("2file"))
                {
                    var writeContents = cmd.ToString().Remove(0, 6);
                    Console.Write("Filename> ");
                    var echoFile = Console.ReadLine();
                    writeLineToFile(echoFile, writeContents);
                }
                else if (cmd.ToString().StartsWith("mkdir"))
                {
                    var makeDir = cmd.ToString().Remove(0, 6);
                    mkdir(makeDir);
                }
                else if (cmd.ToString().StartsWith("mkfile"))
                {
                    var makeFile = cmd.ToString().Remove(0, 7);
                    try
                    {
                        File.Create(current_directory + makeFile);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (cmd.ToString() == "getMACAddr")
                {
                    Console.WriteLine(Networking.Networking.GetMACAddress());
                }
                else if (cmd.ToString() == "netAvailable")
                {
                    Console.WriteLine(Networking.Networking.isNetworkingAvailable().ToString());
                }
                else if (cmd.ToString().StartsWith("color"))
                {
                    try
                    {
                        var fg = args[1].ToString();
                        var bg = args[2].ToString();
                        if (fg == bg)
                        {
                            Console.WriteLine("Foreground and background can't be equal!");
                        }
                        else if (fg == "help")
                        {
                            Console.WriteLine(@"Colors:
            Black
            (Dark)Gray 
            (Dark)Blue
            (Dark)Green
            (Dark)Cyan
            (Dark)Red
            (Dark)Magenta
            (Dark)Yellow
            White");
                        }
                        else
                        {
                            Console.WriteLine("Will be available once we fix this.");
                            Console.WriteLine("(Not sure if it's me or Cosmos.)");
                            //Console.ForegroundColor = (ConsoleColor)cc.getColor(fg);
                            //Console.BackgroundColor = (ConsoleColor)cc.getColor(bg);
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if (cmd.ToString().StartsWith("dog")) {
                    var dogGarnFile = cmd.ToString().Remove(0, 4);
                    readFile(dogGarnFile);
                }
                else if (cmd.ToString().StartsWith("samlang"))
                {
                    var samlangFile = cmd.ToString().Remove(0, 8);
                    string samlangFileContents = "";
                    try
                    {
                        var hello_file = fs.GetFile(current_directory + samlangFile);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanRead)
                        {
                            byte[] text_to_read = new byte[hello_file_stream.Length];
                            hello_file_stream.Read(text_to_read, 0, (int)hello_file_stream.Length);
                            samlangFileContents = Encoding.Default.GetString(text_to_read);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }

                    foreach (var line in samlangFileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
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
                        else
                        {
                            Console.WriteLine("!!! ERROR - COMMAND NOT FOUND!!!");
                        }
                    }
                }
                else if (cmd == "math")
                {
                    int ans = 0;
                    Console.WriteLine("(M)ultiply");
                    Console.WriteLine("(A)dd");
                    Console.WriteLine("(S)ubtract");
                    Console.WriteLine("(D)ivide");
                    Console.Write("MATH> ");
                    string mathOperation = Console.ReadLine().ToLower();
                    Console.Write("First number> ");
                    string FirstNumMath = Console.ReadLine();
                    Console.Write("Second number> ");
                    string SecondNumMath = Console.ReadLine();
                    int first = Convert.ToInt32(FirstNumMath);
                    int second = Convert.ToInt32(SecondNumMath);
                    if (mathOperation == "m")
                    {
                        ans = first * second;
                        Console.WriteLine(ans.ToString());
                    }
                    else if (mathOperation == "a")
                    {
                        ans = first + second;
                        Console.WriteLine(ans.ToString());
                    }
                    else if (mathOperation == "s")
                    {
                        ans = first - second;
                        Console.WriteLine(ans.ToString());
                    }
                    else if (mathOperation == "d")
                    {
                        ans = first / second;
                        Console.WriteLine(ans.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Unknown operation!");
                    }
                }
                else if (cmd.StartsWith("cmds") || cmd.StartsWith("help"))
                {
                    try
                    {
                        if (args[1] == "1")
                        {
                            Console.WriteLine("GEMS NT - Commands");
                            Console.WriteLine("---");
                            Console.WriteLine("about - about this copy of GEMS.");
                            Console.WriteLine("rainbowConnection - Muppets reference");
                            Console.WriteLine("echo - echos back value you pass to it");
                            Console.WriteLine("2file - writes a specified value to a file.");
                            Console.WriteLine("mkfile - creates a file at specified directory");
                            Console.WriteLine("mkdir - creates the specified directory");
                            Console.WriteLine("del - deletes a file, NOT DIRECTORIES.");
                            //Console.WriteLine("ddir - deletes a directory, NOT FILES."); //re-enable once fixed
                            Console.WriteLine("cd - changes directory to passed directory");
                            Console.WriteLine("***MORE ON PAGE #2***");
                        }
                        else if (args[1] == "2")
                        {
                            Console.WriteLine("dir - list contents of directory");
                            Console.WriteLine("clear - clears screen");
                            Console.WriteLine("cmds/help - this.");
                            //Console.WriteLine("setvol - set volume"); //re-enable once fixed
                            //Console.WriteLine("listvol - list volumes"); //re-enable once fixed
                            Console.WriteLine("dog - get contents of file.");
                            Console.WriteLine("samlang - run a file as samlang");
                            Console.WriteLine("math - calculates a math operation with 2 numbers.");
                            Console.WriteLine("getMACAddr - gets your MAC address.");
                            Console.WriteLine("netAvailable - check to see if networking is available.");
                            Console.WriteLine("***MORE ON PAGE #3***");
                        }
                        else if (args[1] == "3")
                        {
                            Console.WriteLine("shutdown - shuts down your pc.");
                            Console.WriteLine("reboot - reboots your pc.");
                            Console.WriteLine("date - gets date in (M)M/DD/YY format.");
                            Console.WriteLine("time - gets time in 24 hour format.");
                            //Console.WriteLine("color [FG] [BG] - sets foreground/background color of console.");
                            //Console.WriteLine("color help - lists colors.");
                            Console.WriteLine("micro - MIV alt.");
                            Console.WriteLine("***END OF COMMANDS***");


                        }
                        else
                        {
                            Console.WriteLine("Invalid page! Try 'cmds 1'");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("No page! Try 'cmds 1'");
                    }
                }
                else
                {
                    Console.WriteLine("Command not found. (Pro tip: 'cmds 1' for commands!)");
                }
            }
        }
        private string[] GetDirFadr(string adr) // Get Directories From Address
        {
            var dirs = Directory.GetDirectories(adr);
            return dirs;
        }

    }
}
