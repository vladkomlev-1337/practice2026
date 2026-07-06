namespace SendEmail;
using Plugin;
[PluginLoad(new[] {"Session"})]
public class Send
{
    public void Execute()
    {
        Console.WriteLine("Email was sended successfully");
    }
}
