using SchedulerSimulator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest
{
    class ConcurrentProcess
    {
        // PID, 도착 시간, 실행 시간, 우선순위
        public const string ex01 = @"4
P0 0 7 3
P1 0 3 3
P2 2 1 4
P3 4 7 1
2";
        public static Process[] Processes;
        public static int TimeQuantum;

        static ConcurrentProcess()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ex01)))
            {
                var dp = new DataParser(ms);
                var data = dp.Parse();
                Processes = data.Processes.ToArray();
                TimeQuantum = data.TimeQuantum;
            }
        }
    }
}
