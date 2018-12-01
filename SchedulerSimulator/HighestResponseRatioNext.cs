using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class HighestResponseRatioNext : Scheduler
    {
        private readonly List<ProcessControlBlock> readyQueue = new List<ProcessControlBlock>();

        protected override bool IsBusy => base.IsBusy || readyQueue.Any();
        protected override bool ShouldDispatch => workingPcb == null;

        public override void Push(Process process)
        {
            OnPush(process);
            readyQueue.Add(new ProcessControlBlock
            {
                Process = process,
                RemainingBurstTime = process.BurstTime,
            });
        }

        private double Calculate(ProcessControlBlock pcb)
        {
            var waitingTime = currentTime - pcb.Process.ArrivalTime;
            return (double)(pcb.Process.BurstTime + waitingTime) / pcb.Process.BurstTime;
        }

        protected override void Dispatch()
        {
            readyQueue.Sort((a, b) => Calculate(b).CompareTo(Calculate(a)));
            OnDispatch(readyQueue.First());
            readyQueue.RemoveAt(0);
        }
    }
}
