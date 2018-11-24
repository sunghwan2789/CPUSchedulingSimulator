using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SchedulerSimulator
{
    public class Process
    {
        /// <summary>
        /// 프로세스 ID
        /// </summary>
        public string ProcessId { get; set; }

        /// <summary>
        /// 도착 시간
        /// </summary>
        public int ArrivalTime { get; set; }

        /// <summary>
        /// 서비스 시간
        /// </summary>
        public int BurstTime { get; set; }

        /// <summary>
        /// 우선순위
        /// </summary>
        public double Priority { get; set; }

        /// <summary>
        /// 프로세스 표시 색상
        /// </summary>
        public Brush Color { get; set; }

        private static Random RNG = new Random();

        /// <summary>
        /// 데이터 줄에서 프로세스 정보를 읽는다.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Process Parse(string line)
        {
            var data = line.Split(' ');
            var rgb = new byte[3];
            RNG.NextBytes(rgb);
            if (data.Length > 4)
            {
                rgb = data[4].Split(' ').Select(byte.Parse).ToArray();
            }
            return new Process
            {
                ProcessId = data[0],
                ArrivalTime = int.Parse(data[1]),
                BurstTime = int.Parse(data[2]),
                Priority = double.Parse(data[3]),
                Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb(rgb[0], rgb[1], rgb[2])),
        };
        }
    }
}
