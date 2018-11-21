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

            if (before.Initial)
            {
                pcb.ResponseTime = currentTime - process.ArrivalTime;
                pcb.WaitingTime = currentTime - process.ArrivalTime;
            }
            else
            {
                pcb.WaitingTime = currentTime - before.EndTime;
            }

            pcb.DispatchTime = currentTime;
            if (pcb.RemainingBurstTime > TimeQuantum)
            {
                pcb.BurstTime = TimeQuantum;
            }
            else
            {
                pcb.BurstTime = pcb.RemainingBurstTime;
            }
            currentTime += pcb.BurstTime;
            pcb.RemainingBurstTime -= pcb.BurstTime;

            OnProcessChanged(pcb);

            if (pcb.RemainingBurstTime > 0)
            {
                readyQueue.Enqueue(pcb);
            }
        }
    }
}
