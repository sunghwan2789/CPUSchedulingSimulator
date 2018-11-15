using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerSimulator
{
    public class DataParser
    {
        private Stream stream;

        public DataParser(string filename)
        {
            stream = File.OpenRead(filename);
        }

        public DataParser(Stream stream)
        {
            this.stream = stream;
        }

        public Data Parse()
        {
            var ret = new Data();
            using (var sr = new StreamReader(stream))
            {
                ret.Processes = new Process[int.Parse(sr.ReadLine())];
                for (var i = 0; i < ret.Processes.Length; i++)
                {
                    var process = Process.Parse(sr.ReadLine());
                    var k = i;
                    for (var j = i - 1; j >= 0; j--)
                    {
                        if (ret.Processes[j].ArrivalTime > process.ArrivalTime)
                        {
                            ret.Processes[j + 1] = ret.Processes[j];
                            k = j;
                        }
                    }
                    ret.Processes[k] = process;
                }
                ret.TimeQuantum = int.Parse(sr.ReadLine());
            }
            return ret;
        }
    }
}
