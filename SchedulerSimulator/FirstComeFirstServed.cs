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

        protected override bool Busy => readyQueue.Any();

        public override void Push(Process process)
        {
            base.Push(process);
            readyQueue.Enqueue(new ProcessControlBlock
            {
                Process = process,
            });
        }

        protected override void Dispatch()
        {
            var before = readyQueue.Dequeue();
            var pcb = (ProcessControlBlock) before.Clone();
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
