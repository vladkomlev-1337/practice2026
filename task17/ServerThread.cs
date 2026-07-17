using System;
using System.Collections.Concurrent;
using System.Windows.Input;
namespace task17;
public class ServerThread
{
    private BlockingCollection<ICommand> _queue = new BlockingCollection<ICommand>();
    private Thread _thread;
    private ExceptionHandler _exceptionHandler;
    private bool _isHardStop = false;
    public bool IsAlive => _thread.IsAlive;
    public int ThreadId => _thread.ManagedThreadId;
    public ServerThread(ExceptionHandler exceptionHandler)
    {
        _exceptionHandler = exceptionHandler;
        _thread = new Thread(Run);
    }
    public void Start() => _thread.Start();
    public void QueueCommand(ICommand command)
    {
        if (!_queue.IsAddingCompleted)
        {
            _queue.Add(command);
        }
    }
    public void ExecuteHardStop()
    {
        _isHardStop = true;
        _queue.CompleteAdding();
    }
    public void ExecuteSoftStop()
    {
        _queue.CompleteAdding();
    }
    public void Run()
    {
        foreach(var command in _queue.GetConsumingEnumerable())
        {
            if (_isHardStop)
            {
                break;
            }
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                _exceptionHandler.Handle(ex, command);
            }
        }
    }
}