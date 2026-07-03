using CommandLib;
namespace FileSystemCommands;
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