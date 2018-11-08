using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    abstract class Scheduler
    {
        protected int currentTime = 0;

        public Action<ProcessControlBlock> Dispatched;
        public Action<ProcessControlBlock> Timeout;
        public Action<ProcessControlBlock> Completed;
        public Action<ProcessControlBlock> ProcessChanged;

        protected abstract void Dispatch();
        public abstract void Push(Process process);
    }
}
