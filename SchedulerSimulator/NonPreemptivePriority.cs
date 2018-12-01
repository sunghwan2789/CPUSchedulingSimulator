using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class NonPreemptivePriority : Scheduler
    {
        private readonly SortedList<int, ProcessControlBlock> readyQueue = new SortedList<int, ProcessControlBlock>(new DuplicateKeyComparer<int>());

        protected override bool IsBusy => base.IsBusy || readyQueue.Any();
        protected override bool ShouldDispatch => workingPcb == null;

        public override void Push(Process process)
        {
            OnPush(process);
            readyQueue.Add(process.Priority, new ProcessControlBlock
            {
                Process = process,
                RemainingBurstTime = process.BurstTime,
            });
        }

        protected override void Dispatch()
        {
            OnDispatch(readyQueue.First().Value);
            readyQueue.RemoveAt(0);
        }
    }
}
