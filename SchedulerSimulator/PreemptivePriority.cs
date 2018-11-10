using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class PreemptivePriority : Scheduler
    {
        private readonly SortedList<int, ProcessControlBlock> readyQueue = new SortedList<int, ProcessControlBlock>();

        public override void Push(Process process) => throw new NotImplementedException();
        protected override void Dispatch() => throw new NotImplementedException();
    }
}
