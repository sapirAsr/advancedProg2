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
    /// Interaction logic for ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : UserControl
    {
        public ConnectionWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).model.disconnect();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ip = ip_insert.Text;
            string port = port_insert.Text;
            if (port == "Enter port" || ip == "Enter ip")
            {
                (Application.Current as App).model.connect("localhost", 5402);
            }
            else
            {
                (Application.Current as App).model.connect(ip, Int32.Parse(port));
            }
            (Application.Current as App).model.start();
        }
    }
}
