namespace UserSession;
using Plugin;
[PluginLoad(new[] {"CaptchaValid", "Auth"})]
public class Session
{
    public void Execute()
    {
        Console.WriteLine("creating user session");
    }
}
