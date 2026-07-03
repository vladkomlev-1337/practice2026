using System;
using System.IO;
using System.Reflection;
using CommandLib;

namespace CommandRunner;

class Program
{
    static void Main()
    {
        string dllpath = Path.Combine(Directory.GetCurrentDirectory(), "FileSystemCommands", "bin", "Debug", "net10.0", "FileSystemCommands.dll");
        string testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "5byte");
        File.WriteAllText(Path.Combine(testDir, "file2.txt"), "aaa");
        File.WriteAllText(Path.Combine(testDir, "file3.log"), "abcdqwerty");
        Assembly assembly = Assembly.LoadFrom(dllpath);
        Type t1 = assembly.GetType("FileSystemCommands.DirectorySizeCommand");
        Type t2 = assembly.GetType("FileSystemCommands.FindFilesCommand");
        ICommand command1 = (ICommand)Activator.CreateInstance(t1, new object[] {testDir});
        ICommand command2 = (ICommand)Activator.CreateInstance(t2, new object[] {testDir, "*.txt"});
        command1.Execute();
        command2.Execute();
        Directory.Delete(testDir, true);
    }
}