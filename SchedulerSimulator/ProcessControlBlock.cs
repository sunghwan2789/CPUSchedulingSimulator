using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    class ProcessControlBlock
    {
        public Process Process { get; private set; }
        public int WaitTime { get; set; }
        public int ResponseTime { get; set; }
        public int TurnaroundTime { get; set; }
        public int BurstTime { get; set; }
    }
}
