using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerSimulator;

namespace SchedulerTest
{
    [TestClass]
    public class FirstComeFirstServedTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new FirstComeFirstServed();
            var expected = new[]
            {
                new ProcessControlBlock
                {
                    Process = new Process
                    {
                        ProcessId = "P0",
                    },
                    ResponseTime = 0,
                    WaitingTime = 0,
                    BurstTime = 7,
                    TurnaroundTime = 7 - 0,
                },
                new ProcessControlBlock
                {
                    Process = new Process
                    {
                        ProcessId = "P1",
                    },
                    ResponseTime = 7 - 2,
                    WaitingTime = 7 - 2,
                    BurstTime = 4,
                    TurnaroundTime = 7 - 2 + 4,
                },
                new ProcessControlBlock
                {
                    Process = new Process
                    {
                        ProcessId = "P2",
                    },
                    ResponseTime = (7 - 2 + 4) - 3,
                    WaitingTime = (7 - 2 + 4) - 3,
                    BurstTime = 1,
                    TurnaroundTime = (7 - 2 + 4) - 3 + 1,
                },
                new ProcessControlBlock
                {
                    Process = new Process
                    {
                        ProcessId = "P3",
                    },
                    ResponseTime = ((7 - 2 + 4) - 3 + 1) - 6,
                    WaitingTime = ((7 - 2 + 4) - 3 + 1) - 6,
                    BurstTime = 3,
                    TurnaroundTime = ((7 - 2 + 4) - 3 + 1) - 6 + 3,
                },
            };
            var seq = 0;
            s.ProcessChanged = (pcb) =>
            {
                var e = expected[seq++];
                Assert.AreEqual(e.Process.ProcessId, pcb.Process.ProcessId, "프로세스 ID 불일치");
                Assert.AreEqual(e.WaitingTime, pcb.WaitingTime, "대기 시간 불일치");
                Assert.AreEqual(e.BurstTime, pcb.BurstTime, "서비스 시간 불일치");
                Assert.AreEqual(e.ResponseTime, pcb.ResponseTime, "응답 시간 불일치");
                Assert.AreEqual(e.TurnaroundTime, pcb.TurnaroundTime, "반환 시간 불일치");
            };
            foreach (var p in Testdata.Processes)
            {
                s.Push(p);
            }
        }
    }
}
