using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class PreemptivePriority : Scheduler
    {
        private readonly SortedList<int, ProcessControlBlock> readyQueue = new SortedList<int, ProcessControlBlock>(new DuplicateKeyComparer<int>());

        protected override bool IsBusy => base.IsBusy || readyQueue.Any();
        protected override bool ShouldDispatch =>
            workingPcb == null
            || workingPcb.Process.Priority > readyQueue.FirstOrDefault().Value?.Process.Priority;

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
            if (workingPcb != null)
            {
                if (workingPcb.RemainingBurstTime > 0)
                {
                    readyQueue.Add(workingPcb.Process.Priority, (ProcessControlBlock)workingPcb.Clone());
                }
                Timeout();
            }
            OnDispatch(readyQueue.First().Value);
            readyQueue.RemoveAt(0);
        }
    }
}
