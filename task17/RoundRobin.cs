using System.Collections.Generic;
using task17;

namespace task17
{
    public class RoundRobin : IScheduler
    {
        private readonly Queue<ICommand> _commands = new Queue<ICommand>();

        public bool HasCommand() => _commands.Count > 0;

        public ICommand Select()
        {
            if (_commands.Count == 0) return null;
            return _commands.Dequeue();
        }

        public void Add(ICommand cmd)
        {
            if (cmd != null)
            {
                _commands.Enqueue(cmd);
            }
        }
    }
}