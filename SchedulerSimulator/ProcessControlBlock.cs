using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class ProcessControlBlock : ICloneable
    {
        /// <summary>
        /// 원본 프로세스
        /// </summary>
        public Process Process { get; set; }

        /// <summary>
        /// 첫 번째 실행 여부
        /// </summary>
        public bool Initial { get; private set; } = true;

        /// <summary>
        /// 작업을 실행하기 시작한 시각
        /// </summary>
        public int DispatchTime { get; set; }

        /// <summary>
        /// CPU가 작업을 처리한 시간
        /// </summary>
        public int BurstTime { get; set; }

        /// <summary>
        /// 작업 처리를 중단하거나 완료한 시각
        /// </summary>
        public int EndTime => DispatchTime + BurstTime;

        /// <summary>
        /// 남은 작업 실행 시간
        /// </summary>
        public int RemainingBurstTime { get; set; }

        /// <summary>
        /// 응답 시간, 작업을 처음 실행하기까지 대기한 시간
        /// </summary>
        public int ResponseTime { get; set; }

        /// <summary>
        /// 대기 시간, CPU가 실행하지 않은 시간들을 합한 시간
        /// </summary>
        public int WaitingTime { get; set; }

        /// <summary>
        /// 반환 시간, 작업 종료 시각에서 도착 시간을 뺀 시간
        /// </summary>
        public int TurnaroundTime
        {
            get => turnaroundTime != null
                ? turnaroundTime.Value
                : EndTime - Process.ArrivalTime;
            set => turnaroundTime = value;
        }

        private int? turnaroundTime = null;

        public object Clone() => new ProcessControlBlock
        {
            Process = Process,
            Initial = false,
            DispatchTime = DispatchTime,
            BurstTime = BurstTime,
            RemainingBurstTime = RemainingBurstTime,
            ResponseTime = ResponseTime,
            WaitingTime = WaitingTime,
        };
    }
}
