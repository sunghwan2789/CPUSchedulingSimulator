using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class FirstComeFirstServed : Scheduler
    {
        private readonly Queue<ProcessControlBlock> readyQueue = new Queue<ProcessControlBlock>();

        protected override bool IsBusy => base.IsBusy || readyQueue.Any();
        protected override bool ShouldDispatch => workingPcb == null;

        public override void Push(Process process)
        {
            OnPush(process);
            readyQueue.Enqueue(new ProcessControlBlock
            {
                Process = process,
                RemainingBurstTime = process.BurstTime,
            });
        }

        protected override void Dispatch()
        {
            OnDispatch(readyQueue.Dequeue());
        }
    }
}
