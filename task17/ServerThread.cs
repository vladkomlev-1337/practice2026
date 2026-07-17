using System.Collections.Concurrent;

namespace task17;
public class ServerThread
{
    private readonly BlockingCollection<ICommand> Queue;
    private readonly Thread thread;
    private volatile bool isHardStopRequested;
    private volatile bool isSoftStopRequested;
    private readonly int threadId;
    public ServerThread()
    {
        Queue = new BlockingCollection<ICommand>(new ConcurrentQueue<ICommand>());
        thread = new Thread(Run);
        threadId = thread.ManagedThreadId;
    }
    public void Start()
    {
        thread.Start();
    }
    public void AddCommand(ICommand command)
    {
        if (isHardStopRequested)
        {
            throw new InvalidOperationException("Поток остановлен");
        }
        Queue.Add(command);
    }
    public void Run()
    {
        while (!isHardStopRequested)
        {
            
            ICommand? command;
            
            if (!Queue.TryTake(out command, 100))
            {
                if (isSoftStopRequested && Queue.Count == 0)
                {
                    break;
                }   
                continue;
            }
            try
            {
                ExecuteCommand(command);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(command,ex);
            }
        
    }
  
    }
    public void ExecuteCommand(ICommand command)
    {
        if (command is HardStopCommand hardStop)
        {
            if (Thread.CurrentThread.ManagedThreadId != threadId)
            {
                throw new InvalidOperationException("Hardstop должна быть исполнена в ааа");
            }
            isHardStopRequested = true;
            hardStop.Execute();
            return;
        }
        if (command is SoftStopCommand softStop)
        {
            if (Thread.CurrentThread.ManagedThreadId != threadId)
            {
                throw new InvalidOperationException("Hardstop должна быть исполнена в ааа");
            }
            isSoftStopRequested = true;
            softStop.Execute();
            return;
        }
        else
        {
            command.Execute();
        }
    }
        
        public void Join()
    {
        thread.Join();
    }
    public bool IsAlive => thread.IsAlive;
}
    
