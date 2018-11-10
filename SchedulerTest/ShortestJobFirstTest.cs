using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerSimulator;

namespace SchedulerTest
{
    [TestClass]
    public class ShortestJobFirstTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new ShortestJobFirst();
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
                    TurnaroundTime = 7 + 4 - 2,
                },
                new ProcessControlBlock
                {
                    Process = new Process
                    {
                        ProcessId = "P2",
                    },
                    ResponseTime = (7 + 4) - 3,
                    WaitingTime = (7 + 4) - 3,
                    BurstTime = 1,
                    TurnaroundTime = (7 + 4) + 1 - 3,
                },
                new ProcessControlBlock
                {
                    Process = new Process
                    {
                        ProcessId = "P3",
                    },
                    ResponseTime = ((7 + 4) + 1) - 6,
                    WaitingTime = ((7 + 4) + 1) - 6,
                    BurstTime = 3,
                    TurnaroundTime = ((7 + 4) + 1) + 3 - 6,
                },
            };
            var seq = 0;
            s.ProcessChanged = (pcb) =>
            {
                var e = expected[seq++];
                Assert.AreEqual(e.Process.ProcessId, pcb.Process.ProcessId, "프로세스 ID 불일치");
                Assert.AreEqual(e.ResponseTime, pcb.ResponseTime, $"{e.Process.ProcessId} 응답 시간 불일치");
                Assert.AreEqual(e.WaitingTime, pcb.WaitingTime, $"{e.Process.ProcessId} 대기 시간 불일치");
                Assert.AreEqual(e.BurstTime, pcb.BurstTime, $"{e.Process.ProcessId} 서비스 시간 불일치");
                Assert.AreEqual(e.TurnaroundTime, pcb.TurnaroundTime, $"{e.Process.ProcessId} 반환 시간 불일치");
            };
            foreach (var p in Testdata.Processes)
            {
                s.Push(p);
            }
        }
    }
}
