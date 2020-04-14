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

namespace FlightSimulatorApp.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        //
        private Point startPoint = new Point();
        public static readonly DependencyProperty RudderProperty = DependencyProperty.Register("Rudder",
            typeof(double), typeof(Joystick));
        public static readonly DependencyProperty ElevatorProperty = DependencyProperty.Register("Elevator",
            typeof(double), typeof(Joystick));

        public double Rudder
        {
            get { return (double)GetValue(RudderProperty); }
            set { SetValue(RudderProperty, value); }
        }

        public double Elevator
        {
            get { return (double)GetValue(ElevatorProperty); }
            set { SetValue(ElevatorProperty, value); }
        }

        private double x;
        private double y;

        public Joystick()
        {
            InitializeComponent();
        }



        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                x = e.GetPosition(this).X - startPoint.X;
                y = e.GetPosition(this).Y - startPoint.Y;
                if (Math.Sqrt(x * x + y * y) < Base.Width / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;

                }

                double rudder = x / (Base.Width / 2);
                double elevator = y / (Base.Width / 2);

                if (rudder > 1) { rudder = 1; }
                else if (rudder < -1) { rudder = -1; }

                if (elevator > 1) { elevator = 1; }
                else if (elevator < -1) { elevator = -1; }

                Rudder = rudder;
                Elevator = elevator;
            }
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIElement joystick = (UIElement)sender;
            joystick.CaptureMouse();
            
            if (e.ChangedButton == MouseButton.Left)
            {
                startPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UIElement joystick = (UIElement)sender;
            joystick.ReleaseMouseCapture();
            // Mouse.Capture(null);
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Rudder = 0;
            Elevator = 0;
        }
    
    }  
}
