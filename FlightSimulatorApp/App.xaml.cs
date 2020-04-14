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
        
        public VMControlers VmC { get; private set; }
        public VMDashboard VmDash { get; private set; }
        public MapVM MapVm { get; private set; }

        public Model model;
        private void Application_Stratup(object sender, StartupEventArgs e)
        {
            model = new Model(new TelnetClient());
            VmDash = new VMDashboard(model);
            VmC = new VMControlers(model);
            MapVm = new MapVM(model);
           
        }
    
    }
}
