namespace task17;
public class HardStopCommand : ICommand
{
    private ServerThread _serverThread;
    public HardStopCommand(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }
    public void Execute()
    {
        if (Thread.CurrentThread.ManagedThreadId != _serverThread.ThreadId)
        {
            throw new InvalidOperationException();
        }
        _serverThread.ExecuteHardStop();
    }
}