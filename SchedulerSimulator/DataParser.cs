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
                var processCount = int.Parse(sr.ReadLine());
                ret.Processes = new List<Process>();
                for (var i = 0; i < processCount; i++)
                {
                    ret.Processes.Add(Process.Parse(sr.ReadLine()));
                }
                ret.TimeQuantum = int.Parse(sr.ReadLine());
            }
            return ret;
        }
    }
}
