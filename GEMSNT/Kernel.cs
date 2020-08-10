﻿using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.IO;

namespace GEMSNT
{
    public class Kernel : Sys.Kernel
    {
        string versionSTR = "0.5prebeta";

        Sys.FileSystem.CosmosVFS fs;

        string current_directory = "0:\\";
        string currDir = "";

        protected override void BeforeRun()
        {
            fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.Clear();
            Console.WriteLine("Welcome to GEMS NT!");
        }

        protected override void Run()
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
                    if (fs_type != null)
                    {
                        Console.WriteLine("File System Type on main drive: " + fs_type);
                        Console.WriteLine("Available Free Space on main drive: " + available_space);
                    } else
                    {
                        Console.WriteLine("File System Type on main drive: NONE");
                        Console.WriteLine("Available Free Space on main drive: 0");
                    }
                } else if (cmd == "rainbowConnection") {
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
                } else if (cmd == "dir") {
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
                } else if (cmd.ToString().StartsWith("echo")) {
                    var echoing = cmd.ToString().Remove(0, 5);
                    Console.WriteLine(echoing);
                } else if (cmd.ToString().StartsWith("cd")) {
                    var dirCD = cmd.ToString().Remove(0, 3);
                    if (dirCD == "..") {
                        string prevDirTemp = current_directory.ToString().Replace(currDir.ToString() + "\\", "\\");
                        Console.WriteLine(prevDirTemp.ToString());
                        current_directory = prevDirTemp.ToString();
                        currDir = fs.GetDirectory(prevDirTemp).mName.ToString();
                    } else {
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
                } else if (cmd.ToString().StartsWith("del")) {
                    var delFile = cmd.ToString().Remove(0, 4);
                    File.Delete(delFile);
                } else if (cmd.ToString().StartsWith("ddir")) {
                    var delDir = cmd.ToString().Remove(0, 5);
                    try {
                        fs.DeleteDirectory(fs.GetDirectory(delDir));
                    } catch (Exception e) {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().StartsWith("2file")) {
                    var writeContents = cmd.ToString().Remove(0, 6);
                    Console.Write("Filename> ");
                    var echoFile = Console.ReadLine();
                    try
                    {
                        using (StreamWriter w = File.AppendText(current_directory + echoFile))
                        {
                            w.WriteLine(writeContents);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().StartsWith("mkdir")) {
                    var makeDir = cmd.ToString().Remove(0, 6);
                    try
                    {
                        fs.CreateDirectory(makeDir);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().StartsWith("mkfile")) {
                    var makeFile = cmd.ToString().Remove(0, 7);
                    try
                    {
                        File.Create(current_directory + makeFile);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                } else if (cmd.ToString().StartsWith("dog")) {
                    var dogFile = cmd.ToString().Remove(0, 4);
                    try
                    {
                        var hello_file = fs.GetFile(current_directory + dogFile);
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
                    Console.WriteLine("rainbowConnection - Muppets reference");
                    Console.WriteLine("echo - echos back value you pass to it");
                    Console.WriteLine("2file - writes a specified value to a file.");  //renable once file creation is back.
                    Console.WriteLine("mkfile - creates a file at specified directory"); //renable once file creation is back.
                    Console.WriteLine("mkdir - creates the specified directory");
                    Console.WriteLine("del - deletes a file, NOT DIRECTORIES.");  //renable once file creation is back.
                    //Console.WriteLine("ddir - deletes a directory, NOT FILES.");
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
