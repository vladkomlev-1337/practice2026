namespace task17
{
    public class LongRunningCommand : ICommand
    {
        private readonly ServerThread _serverThread;
        private int _stepsRemaining;

        public bool IsCompleted => _stepsRemaining <= 0;
        public int ExecutionCount { get; private set; } = 0;

        public LongRunningCommand(ServerThread serverThread, int steps)
        {
            _serverThread = serverThread;
            _stepsRemaining = steps;
        }

        public void Execute()
        {
            if (IsCompleted) return;
            _stepsRemaining--;
            ExecutionCount++;
            if (!IsCompleted)
            {
                _serverThread.Scheduler.Add(this);
            }
        }
    }
}