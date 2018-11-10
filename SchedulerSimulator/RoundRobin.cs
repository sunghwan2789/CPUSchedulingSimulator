using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class RoundRobin : Scheduler
    {
        /// <summary>
        /// 시간 할당량
        /// </summary>
        public int TimeQuantum { get; set; }

        private readonly Queue<ProcessControlBlock> readyQueue = new Queue<ProcessControlBlock>();

        public override void Push(Process process) => throw new NotImplementedException();
        protected override void Dispatch() => throw new NotImplementedException();
    }
}
