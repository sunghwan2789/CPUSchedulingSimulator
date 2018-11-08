using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    class FirstComeFirstServed : Scheduler
    {
        private readonly Queue<ProcessControlBlock> readyQueue = new Queue<ProcessControlBlock>();

        public override void Push(Process process)
        {
            readyQueue.Enqueue(new ProcessControlBlock
            {
                Process = process,
            });
            Dispatch();
        }

        protected override void Dispatch()
        {
            while (readyQueue.Any())
            {
                var before = readyQueue.Dequeue();
                var pcb = (ProcessControlBlock) before.Clone();
                var process = pcb.Process;

                // 프로세스 도착 시간까지 현재 시간을 진행
                if (currentTime < process.ArrivedTime)
                {
                    currentTime = process.ArrivedTime;
                }

                pcb.ResponseTime = currentTime - process.ArrivedTime;

                pcb.BurstTime = process.BurstTime;
                currentTime += process.BurstTime;

                pcb.TurnaroundTime = currentTime - pcb.ResponseTime;
                pcb.WaitingTime = currentTime - before.TurnaroundTime;

                ProcessChanged?.Invoke(pcb);
            }
        }
    }
}
