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
        private ProcessControlBlock lastDispatch = null;

        protected override bool Busy => readyQueue.Any() || lastDispatch != null;

        public override void Push(Process process)
        {
            base.Push(process);
            readyQueue.Enqueue(new ProcessControlBlock
            {
                Process = process,
                RemainingBurstTime = process.BurstTime,
            });
        }

        protected override void Dispatch()
        {
            if (lastDispatch != null)
            {
                readyQueue.Enqueue(lastDispatch);
                lastDispatch = null;
            }

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
                pcb.WaitingTime += currentTime - before.EndTime;
            }

            pcb.DispatchTime = currentTime;

            pcb.BurstTime = Math.Min(TimeQuantum, pcb.RemainingBurstTime);
            currentTime += pcb.BurstTime;
            pcb.RemainingBurstTime -= pcb.BurstTime;

            OnProcessChanged(pcb);

            if (pcb.RemainingBurstTime > 0)
            {
                lastDispatch = pcb;
            }
        }
    }
}
