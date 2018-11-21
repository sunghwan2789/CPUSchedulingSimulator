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

        protected override bool Busy => readyQueue.Any();

        public override void Push(Process process)
        {
            if (
               readyQueue.Any()
               && process.ArrivalTime > readyQueue.First().Process.ArrivalTime
             )
            {
                while (readyQueue.Any() && process.ArrivalTime > currentTime)
                {
                    Dispatch();
                }
            }
            readyQueue.Add(new ProcessControlBlock
            {
                Process = process,
            });
        }
        protected override void Dispatch()
        {
            foreach (var rpcb in readyQueue)
            {
                rpcb.Process.Priority = (double)(currentTime - rpcb.Process.ArrivalTime) / rpcb.Process.BurstTime;
            }
            readyQueue.Sort(new PriorityCompare());

            var before = readyQueue.First();
            readyQueue.RemoveAt(0);
            var pcb = (ProcessControlBlock)before.Clone();
            var process = pcb.Process;

            // 프로세스 도착 시간까지 현재 시간을 진행
            if (currentTime < process.ArrivalTime)
            {
                currentTime = process.ArrivalTime;
            }

            pcb.ResponseTime = currentTime - process.ArrivalTime;
            pcb.WaitingTime = currentTime - process.ArrivalTime;

            pcb.DispatchTime = currentTime;
            pcb.BurstTime = process.BurstTime;
            currentTime += pcb.BurstTime;

            OnProcessChanged(pcb);
        }

        private class PriorityCompare : Comparer<ProcessControlBlock>
        {
            public override int Compare(ProcessControlBlock x, ProcessControlBlock y)
            {
                if (x.Process.Priority == y.Process.Priority)
                {
                    return 0;
                }
                else if (x.Process.Priority < y.Process.Priority)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
