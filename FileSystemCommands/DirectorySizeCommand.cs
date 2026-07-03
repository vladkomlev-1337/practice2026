using System;
using System.IO;
using CommandLib;
using task07;
namespace FileSystemCommands;
[DisplayName("Command for calculating directory size")]
[Version(1,0)]
public class DirectorySizeCommand : ICommand
{
    public string Path;
    public long Size {get; set;}
    public DirectorySizeCommand(string path)
    {
        Path = path;
    }
    public void Execute()
    {
        long size = 0;
        foreach(var file in Directory.GetFiles(Path, "*.*", SearchOption.AllDirectories))
        {
            FileInfo filee = new FileInfo(file);
            size+= filee.Length;
        }
        Size = size;
        Console.WriteLine($"Directory size is {size} lenght");
    }
}