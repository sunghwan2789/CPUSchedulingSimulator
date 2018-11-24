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
    /// Interaction logic for GanttChart.xaml
    /// </summary>
    public partial class GanttChart : UserControl
    {
        public GanttChart()
        {
            InitializeComponent();
            DataContextChanged += GanttChart_DataContextChanged;
        }

        private IEnumerable<ProcessControlBlock> Context => DataContext as IEnumerable<ProcessControlBlock>;
        private double BlockFactor => ActualWidth / Context.Last().EndTime;

        private void GanttChart_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is IEnumerable<ProcessControlBlock> context))
            {
                throw new ArgumentNullException();
            }

            canvas.Children.Clear();
            foreach (var p in context)
            {
                var tooltip = new StringBuilder();
                tooltip.AppendLine($"<< {p.Process.ProcessId} >>");
                tooltip.AppendLine($"시작: {p.DispatchTime}");
                tooltip.AppendLine($"종료: {p.EndTime}");
                tooltip.AppendLine($"처리 시간: {p.BurstTime}");
                tooltip.AppendLine($"남은 시간: {p.RemainingBurstTime}");
                var lbl = new TextBlock
                {
                    Tag = p,
                    Text = p.Process.ProcessId,
                    TextAlignment = TextAlignment.Center,
                    Width = p.BurstTime * BlockFactor,
                    Height = ActualHeight,
                    Background = p.Process.Color,
                    ToolTip = tooltip.ToString().Trim(),
                };
                Canvas.SetLeft(lbl, p.DispatchTime * BlockFactor);
                canvas.Children.Add(lbl);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            foreach (var c in canvas.Children)
            {
                if (c is TextBlock lbl)
                {
                    var pcb = (ProcessControlBlock)lbl.Tag;
                    lbl.Width = pcb.BurstTime * BlockFactor;
                    lbl.Height = ActualHeight;
                    Canvas.SetLeft(lbl, pcb.DispatchTime * BlockFactor);
                }
            }
        }
    }
}
