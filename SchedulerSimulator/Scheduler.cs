using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public abstract class Scheduler
    {
        protected int currentTime = 0;
        protected int lastArrivalTime = 0;
        protected ProcessControlBlock workingPcb = null;
        protected readonly List<ProcessControlBlock> result = new List<ProcessControlBlock>();

        /// <summary>
        /// 준비 큐에 프로세스가 남았는지 확인한다.
        /// </summary>
        protected virtual bool IsBusy => workingPcb != null;

        /// <summary>
        /// Dispatch가 필요한지를 확인한다.
        /// </summary>
        protected abstract bool ShouldDispatch { get; }

        /// <summary>
        /// 준비 큐에 CPU가 처리할 프로세스를 추가한다.
        /// </summary>
        /// <param name="process"></param>
        public abstract void Push(Process process);

        protected void OnPush(Process process)
        {
            // 이전에 동시에 도착한 프로세스를 먼저 실행
            if (lastArrivalTime < process.ArrivalTime)
            {
                while (IsBusy && currentTime < process.ArrivalTime)
                {
                    if (ShouldDispatch)
                    {
                        Dispatch();
                    }
                    Execute();
                }
            }
            // 마지막 도착 시간을 설정하여 동시 도착 작업 처리
            lastArrivalTime = process.ArrivalTime;
        }

        /// <summary>
        /// CPU가 프로세스를 처리하도록 준비 큐에서 PCB를 뽑아
        /// 실행 상태로 바꾸고 스케줄링 알고리즘을 시현한다.
        /// </summary>
        protected abstract void Dispatch();

        protected virtual void OnDispatch(ProcessControlBlock pcb)
        {
            // 프로세스 도착 시간까지 현재 시간을 진행
            if (currentTime < pcb.Process.ArrivalTime)
            {
                currentTime = pcb.Process.ArrivalTime;
            }

            if (pcb.IsInitial)
            {
                pcb.ResponseTime = currentTime - pcb.Process.ArrivalTime;
                pcb.WaitingTime = pcb.ResponseTime;
            }
            else
            {
                pcb.WaitingTime += currentTime - pcb.EndTime;
            }

            pcb.DispatchTime = currentTime;
            pcb.BurstTime = 0;

            workingPcb = pcb;
        }

        /// <summary>
        /// Dispatch로 실행 상태로 바꾼 프로세스를 실행한다.
        /// </summary>
        protected void Execute()
        {
            workingPcb.BurstTime++;
            currentTime++;
            workingPcb.RemainingBurstTime--;
            // 남은 실행 시간이 없으면 실행 종료
            if (workingPcb.RemainingBurstTime == 0)
            {
                Timeout();
            }
        }

        /// <summary>
        /// CPU가 처리하는 프로세스가 종료되거나 준비 상태로 돌아가면
        /// result에 PCB를 추가하고 workingPcb를 비운다.
        /// </summary>
        /// <param name="pcb"></param>
        protected void Timeout()
        {
            result.Add(workingPcb);
            workingPcb = null;
        }

        /// <summary>
        /// 스케줄링 알고리즘 시현 결과를 반환한다.
        /// </summary>
        /// <returns></returns>
        public List<ProcessControlBlock> GetResult()
        {
            // 처리 대기 중인 프로세스를 마저 처리
            while (IsBusy)
            {
                if (ShouldDispatch)
                {
                    Dispatch();
                }
                Execute();
            }
            // 시현 결과 반환
            return result;
        }

        public IEnumerable<ProcessControlBlock> GetFinalResult() =>
            GetResult()
            .GroupBy(i => i.Process)
            .Select(i => new ProcessControlBlock
            {
                Process = i.Key,
                DispatchTime = i.First().DispatchTime,
                BurstTime = i.Sum(j => j.BurstTime),
                RemainingBurstTime = i.Last().RemainingBurstTime,
                ResponseTime = i.First().ResponseTime,
                WaitingTime = i.Last().WaitingTime,
                TurnaroundTime = i.Last().TurnaroundTime,
            })
            .OrderBy(i => i.Process.ArrivalTime);

        public double GetAverageResponseTime() => GetResult().Any() ? GetFinalResult().Average(i => i.ResponseTime) : double.NaN;
        public double GetAverageWaitingTime() => GetResult().Any() ? GetFinalResult().Average(i => i.WaitingTime) : double.NaN;
        public double GetAverageTurnaroundTime() => GetResult().Any() ? GetFinalResult().Average(i => i.TurnaroundTime) : double.NaN;
    }
}
