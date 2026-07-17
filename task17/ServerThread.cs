using System;
using System.Collections.Concurrent;
namespace task17;
public class ServerThread
{
    private BlockingCollection<ICommand> _queue = new BlockingCollection<ICommand>();
    private Thread _thread;
    private ExceptionHandler _exceptionHandler;
    private bool _isHardStop = false;
    public bool IsAlive => _thread.IsAlive;
    public int ThreadId => _thread.ManagedThreadId;
    public IScheduler Scheduler {get;}
    public ServerThread(ExceptionHandler exceptionHandler, IScheduler scheduler)
    {
        _exceptionHandler = exceptionHandler;
        Scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
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
        while (!_isHardStop)
        {
            ICommand command = null;
            if (Scheduler.HasCommand())
            {
                command = Scheduler.Select();
            }
            else if (_queue.TryTake(out var externalCommand))
            {
                command = externalCommand;
            }
            else if (_queue.IsCompleted)
            {
                break;
            }
            else
            {
                Thread.Sleep(1);
                continue;
            }
            if (command == null)
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