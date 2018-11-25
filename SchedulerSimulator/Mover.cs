using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SchedulerSimulator
{
    public class Mover : DependencyObject
    {
        public static readonly DependencyProperty MoveToMiddleProperty =
            DependencyProperty.RegisterAttached("MoveToMiddle", typeof(bool), typeof(Mover),
            new PropertyMetadata(false, PropertyChangedCallback));

        public static void SetMoveToMiddle(UIElement element, bool value)
        {
            element.SetValue(MoveToMiddleProperty, value);
        }

        public static bool GetMoveToMiddle(UIElement element)
        {
            return (bool)element.GetValue(MoveToMiddleProperty);
        }

        private static void PropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is FrameworkElement element))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                element.SetBinding(UIElement.RenderTransformProperty, new Binding
                {
                    Converter = new CenterConverter(),
                    Path = new PropertyPath("ActualWidth"),
                    Source = element,
                });
            }
            else
            {
                element.ClearValue(UIElement.RenderTransformProperty);
            }
        }

    }
}
