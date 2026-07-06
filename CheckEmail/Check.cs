namespace CheckEmail;
using Plugin;
[PluginLoad(new[] {"Session"})]
public class Check
{
    public void Execute()
    {
        Console.WriteLine("you dont have new emails");
    }
}
