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
            foreach (var p in Testdata.Processes)
            {
                s.Push(p);
            }
            var result = s.GetResult();
            Assert.AreEqual(expected.Length, result.Count, "프로세스 처리 불충분");
            for (var i = 0; i < expected.Length; i++)
            {
                var e = expected[i];
                var r = result[i];
                Assert.AreEqual(e.Process.ProcessId, r.Process.ProcessId, "프로세스 ID 불일치");
                Assert.AreEqual(e.ResponseTime, r.ResponseTime, $"{e.Process.ProcessId} 응답 시간 불일치");
                Assert.AreEqual(e.WaitingTime, r.WaitingTime, $"{e.Process.ProcessId} 대기 시간 불일치");
                Assert.AreEqual(e.BurstTime, r.BurstTime, $"{e.Process.ProcessId} 서비스 시간 불일치");
                Assert.AreEqual(e.TurnaroundTime, r.TurnaroundTime, $"{e.Process.ProcessId} 반환 시간 불일치");
            }
        }
    }
}
