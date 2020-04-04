using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class VMDashboardv : INotifyPropertyChanged
    {
        private Model model;
      
        public double VM_heading { get { return model.heading; } }
        public double VM_verticalSpeed { get { return model.verticalSpeed; } }
        public double VM_groundSpeed { get { return model.groundSpeed; } }
        public double VM_airSpeed {  get { return model.airSpeed; } }
        public double VM_altitude {  get { return model.altitude; } }
        public double VM_roll { get { return model.roll; } }
        public double VM_pitch { get { return model.pitch; } }
        public double VM_altimeter { get { return model.altimeter; } }
      

        public VMDashboardv(Model m)
        {
            this.model = m;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }
    }
}
