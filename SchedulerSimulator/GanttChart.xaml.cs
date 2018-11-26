using System;
using System.Collections.Generic;
using System.Globalization;
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
        private double BlockFactor => Context.Last().EndTime > 0 ? ActualWidth / Context.Last().EndTime : ActualWidth;

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

                var beginBar = new Line
                {
                    X1 = p.DispatchTime * BlockFactor,
                    Y1 = 0,
                    X2 = p.DispatchTime * BlockFactor,
                    Y2 = ActualHeight + 6,
                    Stroke = new SolidColorBrush(Colors.Black),
                };
                var begin = new Label
                {
                    Content = p.DispatchTime.ToString(),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                };
                Canvas.SetLeft(begin, p.DispatchTime * BlockFactor);
                Canvas.SetTop(begin, ActualHeight);
                Mover.SetMoveToMiddle(begin, true);
                canvas.Children.Add(beginBar);
                canvas.Children.Add(begin);

                var endBar = new Line
                {
                    X1 = p.EndTime * BlockFactor,
                    Y1 = 0,
                    X2 = p.EndTime * BlockFactor,
                    Y2 = ActualHeight + 6,
                    Stroke = new SolidColorBrush(Colors.Black),
                };
                var end = new Label
                {
                    Content = p.EndTime.ToString(),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                };
                Canvas.SetLeft(end, p.EndTime * BlockFactor);
                Canvas.SetTop(end, ActualHeight);
                Mover.SetMoveToMiddle(end, true);
                canvas.Children.Add(endBar);
                canvas.Children.Add(end);
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            ProcessControlBlock lastProcess = null;
            var begin = true;
            foreach (FrameworkElement c in canvas.Children)
            {
                if (c.Tag is ProcessControlBlock pcb)
                {
                    lastProcess = pcb;
                    begin = true;
                    c.Width = pcb.BurstTime * BlockFactor;
                    c.Height = ActualHeight;
                    Canvas.SetLeft(c, pcb.DispatchTime * BlockFactor);
                }
                else if (begin)
                {
                    if (c is Line line)
                    {
                        line.X2 = line.X1 = lastProcess.DispatchTime * BlockFactor;
                        line.Y2 = ActualHeight + 6;
                    }
                    else
                    {
                        begin = false;
                        Canvas.SetLeft(c, lastProcess.DispatchTime * BlockFactor);
                        Canvas.SetTop(c, ActualHeight);
                    }
                }
                else
                {
                    if (c is Line line)
                    {
                        line.X2 = line.X1 = lastProcess.EndTime * BlockFactor;
                        line.Y2 = ActualHeight + 6;
                    }
                    else
                    {
                        Canvas.SetLeft(c, lastProcess.EndTime * BlockFactor);
                        Canvas.SetTop(c, ActualHeight);
                    }
                }
            }
        }
    }
}
