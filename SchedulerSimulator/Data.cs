using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class Data
    {
        public List<Process> Processes { get; set; } = new List<Process>();
        public int TimeQuantum { get; set; } = 1;
    }
}
