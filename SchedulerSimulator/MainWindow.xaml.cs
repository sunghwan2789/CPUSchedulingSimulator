using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchedulerSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dataFilename { get; set;}
        private Scheduler scheduler;
        private Data data;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                dataFilename = dlg.FileName;
                textBox3.Text = dataFilename;
                var dp = new DataParser(dataFilename);
                data = dp.Parse();

                tabControl_SelectionChanged(null, null);
            }

        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tab = (TabItem)tabControl.SelectedItem;
            switch (tab.Name)
            {
                case "fcfs":
                    scheduler = new FirstComeFirstServed();
                    break;
                case "sjf":
                    scheduler = new ShortestJobFirst();
                    break;
                case "srt":
                    scheduler = new ShortestRemainingTimeFirst();
                    break;
                case "npp":
                    scheduler = new NonPreemptivePriority();
                    break;
                case "pp":
                    scheduler = new PreemptivePriority();
                    break;
                case "hrn":
                    scheduler = new HighestResponseRatioNext();
                    break;
                case "rr":
                    scheduler = new RoundRobin
                    {
                        TimeQuantum = data.TimeQuantum,
                    };
                    break;
            }
            if (data == null)
            {
                return;
            }

            var colors = new Dictionary<Process, Color>();
            var rand = new Random();
            foreach (var p in data.Processes)
            {
                p.Color = Color.FromRgb((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255));
                scheduler.Push(p);
            }

            canvaaas.DataContext = scheduler.GetResult();
            listView.ItemsSource = scheduler.GetFinalResult();
            lblAverage.Text = $@"평균 대기 시간: {scheduler.GetAverageWaitingTime()}
평균 반환 시간: {scheduler.GetAverageTurnaroundTime()}
평균 응답 시간: {scheduler.GetAverageResponseTime()}";
        }
    }
}
