using SchedulerSimulator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerTest
{
    class Testdata
    {
        // PID, 도착 시간, 실행 시간, 우선순위
        public const string ex01 = @"4
P0 0 7 3
P1 2 4 2
P2 3 1 4
P3 6 3 1
2";
        public static Process[] Processes;
        public static int TimeQuantum;

        static Testdata()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ex01)))
            {
                var dp = new DataParser(ms);
                var data = dp.Parse();
                Processes = data.Processes;
                TimeQuantum = data.TimeQuantum;
            }
        }
    }
}
