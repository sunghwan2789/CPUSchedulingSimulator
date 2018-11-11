using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class NonPreemptivePriority : Scheduler
    {
        private readonly SortedList<int, ProcessControlBlock> readyQueue = new SortedList<int, ProcessControlBlock>();

        protected override bool Busy => throw new NotImplementedException();

        public override void Push(Process process) => throw new NotImplementedException();
        protected override void Dispatch() => throw new NotImplementedException();
    }
}
