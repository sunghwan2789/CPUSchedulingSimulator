using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    class Process
    {
        public string ProcessId { get; private set; }
        public int ArrivedTime { get; private set; }
        public int BurstTime { get; private set; }
        public int Priority { get; private set; }
    }
}
