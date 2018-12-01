using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class RoundRobin : Scheduler
    {
        /// <summary>
        /// 시간 할당량
        /// </summary>
        public int TimeQuantum { get; set; }

        private readonly Queue<ProcessControlBlock> readyQueue = new Queue<ProcessControlBlock>();

        protected override bool IsBusy => base.IsBusy || readyQueue.Any();
        protected override bool ShouldDispatch => 
            workingPcb == null
            || workingPcb.BurstTime == TimeQuantum;

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
            if (workingPcb != null)
            {
                if (workingPcb.RemainingBurstTime > 0)
                {
                    readyQueue.Enqueue(workingPcb.Clone());
                }
                Timeout();
            }
            OnDispatch(readyQueue.Dequeue());
        }
    }
}
