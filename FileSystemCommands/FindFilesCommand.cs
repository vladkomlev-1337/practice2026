using CommandLib;
using task07;
//try: dotnet run --project ./MetadataConsoleApp/MetadataConsoleApp.csproj ./FileSystemCommands/bin/Debug/net10.0/FileSystemCommands.dll
namespace FileSystemCommands;
[DisplayName("Command for finding files by masks")]
[Version(1,0)]
public class FindFilesCommand : ICommand
{
    public string Path;
    public string Mask;
    public List<string> FoundFiles {get;set;}
    
    public FindFilesCommand(string path, string mask)
    {
        Path = path;
        Mask = mask;
        FoundFiles = new List<string>();
        
    }
    public void Execute()
    {
        foreach (var file in Directory.GetFiles(Path, $"{Mask}", SearchOption.AllDirectories))
        {
            FileInfo filee = new FileInfo(file);
            Console.WriteLine(filee.Name);
            FoundFiles.Add(file);
        }
    }
}