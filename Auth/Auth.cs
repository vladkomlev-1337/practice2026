namespace Auth;
using Plugin;
[PluginLoad(new[] {"CaptchaValid"})]
public class Auth
{
    public void Execute()
    {
        Console.WriteLine("Authentification was successfully");
    }
}
