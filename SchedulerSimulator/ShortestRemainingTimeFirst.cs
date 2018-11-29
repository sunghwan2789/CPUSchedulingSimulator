using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class ShortestRemainingTimeFirst : Scheduler
    {
        private readonly SortedList<int, ProcessControlBlock> readyQueue = new SortedList<int, ProcessControlBlock>(new DuplicateKeyComparer<int>());
        private ProcessControlBlock lastProcess = null;

        protected override bool Busy => readyQueue.Any();

        public override void Push(Process process)
        {
            base.Push(process);
            readyQueue.Add(process.BurstTime, new ProcessControlBlock
            {
                Process = process,
                RemainingBurstTime = process.BurstTime,
            });
        }
        protected override void Dispatch()
        {
            if (lastProcess != null)
            {
                if (lastProcess.RemainingBurstTime > readyQueue.First().Value.RemainingBurstTime)
                {
                    readyQueue.RemoveAt(readyQueue.IndexOfValue(lastProcess));
                    if (lastProcess.RemainingBurstTime > 0)
                    {
                        readyQueue.Add(lastProcess.RemainingBurstTime, (ProcessControlBlock)lastProcess.Clone());
                    }

                    OnProcessChanged(lastProcess);
                    lastProcess = null;
                }
                else if (lastProcess.RemainingBurstTime == 0)
                {
                    readyQueue.RemoveAt(readyQueue.IndexOfValue(lastProcess));

                    OnProcessChanged(lastProcess);
                    lastProcess = null;
                }
                else
                {
                    lastProcess.BurstTime++;
                    currentTime++;
                    lastProcess.RemainingBurstTime--;
                }
                return;
            }

            var pcb = lastProcess = readyQueue.First().Value;
            var process = pcb.Process;

            // 프로세스 도착 시간까지 현재 시간을 진행
            if (currentTime < process.ArrivalTime)
            {
                currentTime = process.ArrivalTime;
            }

            if (pcb.Initial)
            {
                pcb.ResponseTime = currentTime - process.ArrivalTime;
                pcb.WaitingTime = currentTime - process.ArrivalTime;
            }
            else
            {
                pcb.WaitingTime += currentTime - pcb.EndTime;
            }

            pcb.DispatchTime = currentTime;

            pcb.BurstTime = 0;
        }
    }
}
