using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    class Process
    {
        /// <summary>
        /// 프로세스 ID
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 도착 시간
        /// </summary>
        public int ArrivedTime { get; set; }

        /// <summary>
        /// 서비스 시간
        /// </summary>
        public int BurstTime { get; set; }

        /// <summary>
        /// 우선순위
        /// </summary>
        public int Priority { get; set; }
    }
}
