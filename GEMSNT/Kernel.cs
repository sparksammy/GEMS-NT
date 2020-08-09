using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;

namespace GEMSNT
{
    public class Kernel : Sys.Kernel
    {
        string versionSTR = "0.43a";

        Sys.FileSystem.CosmosVFS fs;
        string current_directory = "0:\\";

        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("Welcome to GEMS NT!");
        }

        protected override void Run()
        {
            while (1 == 1)
            {
                Console.Write(current_directory + " $> ");
                var cmd = Console.ReadLine();
                //Console.Write("*RUSHELL DEBUG* Command typed: ");
                //Console.WriteLine(cmd);
                if (cmd == "clear") {
                    Console.Clear();
                } else if (cmd == "about") {
                    long available_space = fs.GetAvailableFreeSpace("0:/");
                    string fs_type = fs.GetFileSystemType("0:/");
                    Console.WriteLine("GEMS NT Version: " + versionSTR);
                    Console.WriteLine("File System Type on main drive: " + fs_type);
                    Console.WriteLine("Available Free Space on main drive: " + available_space);
                } else if (cmd == "dir") {
                    string[] dirs = GetDirFadr(current_directory);
                    foreach (var item in dirs)
                    {
                        Console.WriteLine(item);
                    }
                } else if (cmd.ToString().Contains("echo")) {
                    var echoing = cmd.ToString().Remove(0, 5);
                    Console.WriteLine(echoing);
                } else if (cmd.ToString().Contains("cd")) {
                    var dirCD = cmd.ToString().Remove(0, 3);
                    Directory.SetCurrentDirectory(dirCD);
                } else if (cmd.ToString().Contains("del")) {
                    var delFile = cmd.ToString().Remove(0, 4);
                    File.Delete(delFile);
                } else if (cmd.ToString().Contains("2file")) {
                    var writeContents = cmd.ToString().Remove(0, 6);
                    Console.Write("File Path> ");
                    var echoFile = Console.ReadLine();
                    try
                    {
                        var hello_file = fs.GetFile(echoFile);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanWrite)
                        {
                            byte[] text_to_write = Encoding.ASCII.GetBytes(writeContents);
                            hello_file_stream.Write(text_to_write, 0, text_to_write.Length);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().Contains("mkdir")) {
                    var makeDirPath = cmd.ToString().Remove(0, 6);
                    try
                    {
                        fs.CreateDirectory(makeDirPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().Contains("mkfile")) {
                    var makeFilePath = cmd.ToString().Remove(0, 7);
                    try
                    {
                        fs.CreateFile(makeFilePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().Contains("dog")) {
                    var dogFile = cmd.ToString().Remove(0, 4);
                    try
                    {
                        var hello_file = fs.GetFile(dogFile);
                        var hello_file_stream = hello_file.GetFileStream();

                        if (hello_file_stream.CanRead)
                        {
                            byte[] text_to_read = new byte[hello_file_stream.Length];
                            hello_file_stream.Read(text_to_read, 0, (int)hello_file_stream.Length);
                            Console.WriteLine(Encoding.Default.GetString(text_to_read));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd == "cmds" || cmd == "help") {
                    Console.WriteLine("GEMS NT - Commands");
                    Console.WriteLine("---");
                    Console.WriteLine("about - about this copy of GEMS.");
                    Console.WriteLine("echo - echos back value you pass to it");
                    Console.WriteLine("2file - writes a specified value to a file.");
                    Console.WriteLine("mkfile - creates a file at specified directory");
                    Console.WriteLine("mkdir - creates the specified directory");
                    Console.WriteLine("del - deletes a file");
                    Console.WriteLine("cd - changes directory to passed directory");
                    Console.WriteLine("dir - list contents of directory");
                    Console.WriteLine("clear - clears screen");
                    Console.WriteLine("cmds/help - this.");
                    Console.WriteLine("dog - get contents of file.");
                } else {
                    Console.WriteLine("Command not found. (Pro tip: cmds for commands!)");
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
