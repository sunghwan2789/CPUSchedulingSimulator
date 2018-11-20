using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class NonPreemptivePriority : Scheduler
    {
        private readonly SortedList<double, ProcessControlBlock> readyQueue = new SortedList<double, ProcessControlBlock>(new DuplicateKeyComparer<double>());

        protected override bool Busy => readyQueue.Any();

        public override void Push(Process process)
        {
            if (
               readyQueue.Any()
               && process.ArrivalTime > readyQueue.First().Value.Process.ArrivalTime
             )
            {
                while (readyQueue.Any() && process.ArrivalTime > currentTime)
                {
                    Dispatch();
                }
            }
            readyQueue.Add(process.Priority, new ProcessControlBlock
            {
                Process = process,
            });
        }
        protected override void Dispatch()
        {
            var before = readyQueue.First().Value;
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

            currentTime += process.BurstTime;
            pcb.BurstTime = process.BurstTime;

            pcb.TurnaroundTime = currentTime - process.ArrivalTime;

            OnProcessChanged(pcb);
        }
    }
}
