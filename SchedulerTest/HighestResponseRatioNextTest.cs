using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerSimulator;

namespace SchedulerTest
{
    [TestClass]
    public class HighestResponseRatioNextTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new HighestResponseRatioNext();
            var processes = Testdata.Processes;
            var expected = new[]
            {
                new ProcessControlBlock
                {
                    Process = processes[0],
                    DispatchTime = 0,
                    BurstTime = 7,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 - 0,
                    WaitingTime = 0 - 0,
                },
                new ProcessControlBlock
                {
                    Process = processes[2],
                    DispatchTime = 0 + 7,
                    BurstTime = 1,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 + 7 - 3,
                    WaitingTime = 0 + 7 - 3,
                },
                 new ProcessControlBlock
                {
                    Process = processes[1],
                    DispatchTime = 0 + 7 + 1,
                    BurstTime = 4,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 + 7 + 1 - 2,
                    WaitingTime = 0 + 7 + 1 - 2,
                },
                new ProcessControlBlock
                {
                    Process = processes[3],
                    DispatchTime = 0 + 7 + 1 + 4,
                    BurstTime = 3,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 + 7 + 1 + 4 - 6,
                    WaitingTime = 0 + 7 + 1 + 4 - 6,
                },
            };
            foreach (var p in processes)
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

        [TestMethod]
        public void TestConcurrentProcess()
        {
            var s = new HighestResponseRatioNext();
            var processes = ConcurrentProcess.Processes;
            var expected = new[]
            {
                new ProcessControlBlock
                {
                    Process = processes[0],
                    DispatchTime = 0,
                    BurstTime = 7,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 - 0,
                    WaitingTime = 0 - 0,
                },
                new ProcessControlBlock
                {
                    Process = processes[2],
                    DispatchTime = 0 + 7,
                    BurstTime = 1,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 + 7 - 2,
                    WaitingTime = 0 + 7 - 2,
                },
                new ProcessControlBlock
                {
                    Process = processes[1],
                    DispatchTime = 0 + 7 + 1,
                    BurstTime = 3,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 + 7 + 1 - 0,
                    WaitingTime = 0 + 7 + 1 - 0,
                },
                new ProcessControlBlock
                {
                    Process = processes[3],
                    DispatchTime = 0 + 7 + 1 + 3,
                    BurstTime = 7,
                    RemainingBurstTime = 0,
                    ResponseTime = 0 + 7 + 1 + 3 - 4,
                    WaitingTime = 0 + 7 + 1 + 3 - 4,
                },
            };
            foreach (var p in processes)
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
