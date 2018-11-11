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
        protected readonly List<ProcessControlBlock> result = new List<ProcessControlBlock>();

        public Action<ProcessControlBlock> Dispatched;
        public Action<ProcessControlBlock> Timeout;
        public Action<ProcessControlBlock> Completed;

        /// <summary>
        /// CPU가 처리하는 프로세스가 변경되었을 때 실행할 메서드
        /// </summary>
        public Action<ProcessControlBlock> ProcessChanged;

        /// <summary>
        /// CPU가 처리하는 프로세스가 변경되면 result에 PCB를
        /// 추가하고 ProcessChanged를 실행한다.
        /// </summary>
        /// <param name="pcb"></param>
        protected void OnProcessChanged(ProcessControlBlock pcb)
        {
            result.Add(pcb);
            ProcessChanged?.Invoke(pcb);
        }

        /// <summary>
        /// 준비 큐에 프로세스가 남았는지 확인한다.
        /// </summary>
        protected abstract bool Busy { get; }

        /// <summary>
        /// CPU가 프로세스를 처리하도록 준비 큐에서 PCB를 뽑아
        /// 실행 상태로 바꾸고 스케줄링 알고리즘을 시현한다.
        /// </summary>
        protected abstract void Dispatch();

        /// <summary>
        /// 준비 큐에 CPU가 처리할 프로세스를 추가한다.
        /// </summary>
        /// <param name="process"></param>
        public abstract void Push(Process process);

        /// <summary>
        /// 스케줄링 알고리즘 시현 결과를 반환한다.
        /// </summary>
        /// <returns></returns>
        public List<ProcessControlBlock> GetResult()
        {
            // 처리 대기 중인 프로세스를 마저 처리
            while (Busy)
            {
                Dispatch();
            }
            // 시현 결과 반환
            return result;
        }
    }
}
