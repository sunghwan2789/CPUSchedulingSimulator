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
            readyQueue.Enqueue(new ProcessControlBlock
            {
                Process = process,
            });
        }

        protected override void Dispatch()
        {
            var before = readyQueue.Dequeue();
            var pcb = (ProcessControlBlock)before.Clone();
            var process = pcb.Process;

            // 프로세스 도착 시간까지 현재 시간을 진행
            if (currentTime < process.ArrivalTime)
            {
                currentTime = process.ArrivalTime;
            }

            if (pcb.ResponseTime == null)
            {
                pcb.ResponseTime = currentTime - process.ArrivalTime;
            }
            if (pcb.WaitingTime == null)
            {
                pcb.WaitingTime = currentTime - process.ArrivalTime;
            }
            else
            {
                // TODO
                pcb.WaitingTime = before.WaitingTime;
            }

            currentTime += process.BurstTime;
            pcb.BurstTime = process.BurstTime;

            pcb.TurnaroundTime = currentTime - process.ArrivalTime;

            OnProcessChanged(pcb);
        }
    }
}
