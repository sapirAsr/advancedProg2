using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class MainWindowVM
    {
        //
        public Model model;
        public VMControlers vmC;
        public VMDashboard vmDash;
        public MapVM mapvm;

        public MainWindowVM(Model m)
        {
            this.model = m;
            this.vmC = new VMControlers(m);
            this.vmDash = new VMDashboard(m);
            this.mapvm = new MapVM(m);
        }
        
    }
}
