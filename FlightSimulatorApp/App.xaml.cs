using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public VMControlers VmC { get; internal set; }
        public VMDashboard VmDash { get; internal set; }
        public MapVM MapVm { get; internal set; }
        private void Application_Stratup(object sender, StartupEventArgs e)
        {
            Model model = new Model(new TelnetClient());
            VmDash = new VMDashboard(model);
            VmC = new VMControlers(model);
            MapVm = new MapVM(model);
            model.connect("localhost", 5402);
            model.start();
        }
    }
}
