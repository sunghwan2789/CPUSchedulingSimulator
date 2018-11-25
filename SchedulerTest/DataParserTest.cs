using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerSimulator;

namespace SchedulerTest
{
    [TestClass]
    public class DataParserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            const string ex01 = @"4
P0 0 7 3
P1 2 4 2
P2 3 1 4
P3 6 3 1
2";
            var expectedProcess = new[]
            {
                new Process
                {
                    ProcessId = "P0",
                    ArrivalTime = 0,
                    BurstTime = 7,
                    Priority = 3,
                },
                new Process
                {
                    ProcessId = "P1",
                    ArrivalTime = 2,
                    BurstTime = 4,
                    Priority = 2,
                },
                new Process
                {
                    ProcessId = "P2",
                    ArrivalTime = 3,
                    BurstTime = 1,
                    Priority = 4,
                },
                new Process
                {
                    ProcessId = "P3",
                    ArrivalTime = 6,
                    BurstTime = 3,
                    Priority = 1,
                },
            };
            var expectedTimeQuantum = 2;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ex01)))
            {
                var dp = new DataParser(ms);
                var data = dp.Parse();
                Assert.AreEqual(expectedProcess.Length, data.Processes.Count);
                for (var i = 0; i < expectedProcess.Length; i++)
                {
                    Assert.AreEqual(expectedProcess[i].ProcessId, data.Processes[i].ProcessId);
                    Assert.AreEqual(expectedProcess[i].ArrivalTime, data.Processes[i].ArrivalTime);
                    Assert.AreEqual(expectedProcess[i].BurstTime, data.Processes[i].BurstTime);
                    Assert.AreEqual(expectedProcess[i].Priority, data.Processes[i].Priority);
                }
                Assert.AreEqual(expectedTimeQuantum, data.TimeQuantum);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            const string ex01 = @"4
P0 0 7 3
P1 0 3 3
P2 2 1 4
P3 4 7 1
2";
            var expectedProcess = new[]
            {
                new Process
                {
                    ProcessId = "P0",
                    ArrivalTime = 0,
                    BurstTime = 7,
                    Priority = 3,
                },
                new Process
                {
                    ProcessId = "P1",
                    ArrivalTime = 0,
                    BurstTime = 3,
                    Priority = 3,
                },
                new Process
                {
                    ProcessId = "P2",
                    ArrivalTime = 2,
                    BurstTime = 1,
                    Priority = 4,
                },
                new Process
                {
                    ProcessId = "P3",
                    ArrivalTime = 4,
                    BurstTime = 7,
                    Priority = 1,
                },
            };
            var expectedTimeQuantum = 2;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ex01)))
            {
                var dp = new DataParser(ms);
                var data = dp.Parse();
                Assert.AreEqual(expectedProcess.Length, data.Processes.Count);
                for (var i = 0; i < expectedProcess.Length; i++)
                {
                    Assert.AreEqual(expectedProcess[i].ProcessId, data.Processes[i].ProcessId);
                    Assert.AreEqual(expectedProcess[i].ArrivalTime, data.Processes[i].ArrivalTime);
                    Assert.AreEqual(expectedProcess[i].BurstTime, data.Processes[i].BurstTime);
                    Assert.AreEqual(expectedProcess[i].Priority, data.Processes[i].Priority);
                }
                Assert.AreEqual(expectedTimeQuantum, data.TimeQuantum);
            }
        }

        [TestMethod]
        public void TestSortedByArrivalTime()
        {
            const string ex01 = @"4
P0 3 7 3
P1 0 3 3
P2 2 1 4
P3 4 7 1
2";
            var expectedProcess = new[]
            {
                new Process
                {
                    ProcessId = "P1",
                    ArrivalTime = 0,
                    BurstTime = 3,
                    Priority = 3,
                },
                new Process
                {
                    ProcessId = "P2",
                    ArrivalTime = 2,
                    BurstTime = 1,
                    Priority = 4,
                },
                new Process
                {
                    ProcessId = "P0",
                    ArrivalTime = 3,
                    BurstTime = 7,
                    Priority = 3,
                },
                new Process
                {
                    ProcessId = "P3",
                    ArrivalTime = 4,
                    BurstTime = 7,
                    Priority = 1,
                },
            };
            var expectedTimeQuantum = 2;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ex01)))
            {
                var dp = new DataParser(ms);
                var data = dp.Parse();
                Assert.AreEqual(expectedProcess.Length, data.Processes.Count);
                for (var i = 0; i < expectedProcess.Length; i++)
                {
                    Assert.AreEqual(expectedProcess[i].ProcessId, data.Processes[i].ProcessId);
                    Assert.AreEqual(expectedProcess[i].ArrivalTime, data.Processes[i].ArrivalTime);
                    Assert.AreEqual(expectedProcess[i].BurstTime, data.Processes[i].BurstTime);
                    Assert.AreEqual(expectedProcess[i].Priority, data.Processes[i].Priority);
                }
                Assert.AreEqual(expectedTimeQuantum, data.TimeQuantum);
            }
        }
    }
}
