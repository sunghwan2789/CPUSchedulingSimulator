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
        /// 대기 시간, CPU가 실행하지 않은 시간들을 합한 시간
        /// </summary>
        public int WaitingTime { get; set; }
        
        /// <summary>
        /// 응답 시간, 작업이 처음 실행되기까지 걸린 시간<br />
        /// 첫 대기 시간
        /// </summary>
        public int ResponseTime { get; set; }
        
        /// <summary>
        /// 반환 시간, 실행 시간과 대기 시간을 모두 합한 시간으로 작업이 완료될 때까지 걸린 시간
        /// </summary>
        public int TurnaroundTime { get; set; }
        
        /// <summary>
        /// 서비스 시간, CPU가 작업을 처리하는 시간을 합한 시간
        /// </summary>
        public int BurstTime { get; set; }

        public object Clone() => new ProcessControlBlock
        {
            Process = Process,
            WaitingTime = WaitingTime,
            ResponseTime = ResponseTime,
            TurnaroundTime = TurnaroundTime,
            BurstTime = BurstTime,
        };
    }
}
