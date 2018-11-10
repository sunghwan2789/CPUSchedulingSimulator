using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class ShortestJobFirst : Scheduler
    {
        private readonly SortedList<int, ProcessControlBlock> readyQueue = new SortedList<int, ProcessControlBlock>();

        public override void Push(Process process)
        {
            readyQueue.Add(process.BurstTime, new ProcessControlBlock
            {
                Process = process,
            });
            Dispatch();
        }
        protected override void Dispatch() => throw new NotImplementedException();
    }
}
