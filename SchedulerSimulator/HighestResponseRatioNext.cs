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
            base.Push(process);
            readyQueue.Add(new ProcessControlBlock
            {
                Process = process,
            });
        }

        private double Calculate(ProcessControlBlock pcb) => (double)(currentTime - pcb.Process.ArrivalTime) / pcb.Process.BurstTime;

        protected override void Dispatch()
        {
            readyQueue.Sort((a, b) => Calculate(b).CompareTo(Calculate(a)));

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
    }
}
