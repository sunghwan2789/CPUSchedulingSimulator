using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class HighestResponseRatioNext : Scheduler
    {
        private readonly List<ProcessControlBlock> readyQueue = new List<ProcessControlBlock>();

        public override void Push(Process process) => throw new NotImplementedException();
        protected override void Dispatch() => throw new NotImplementedException();
    }
}
