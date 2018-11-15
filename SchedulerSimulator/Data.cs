using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class Data
    {
        public Process[] Processes { get; set; }
        public int TimeQuantum { get; set; }
    }
}
