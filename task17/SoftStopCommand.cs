using System.Collections.Concurrent;
using System.Windows.Input;
namespace task17;
public class SoftStopCommand : ICommand
{
    private ServerThread _serverThread;
    public SoftStopCommand(ServerThread serverThread) {
        _serverThread = serverThread;
    }
    public void Execute()
    {
        if (Thread.CurrentThread.ManagedThreadId != _serverThread.ThreadId)
        {
            throw new InvalidOperationException();
        }
        _serverThread.ExecuteSoftStop();
    }
}