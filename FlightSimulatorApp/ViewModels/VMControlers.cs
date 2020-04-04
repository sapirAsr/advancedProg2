using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class VMControlers : INotifyPropertyChanged
    {
        private Model model;
        private double rudder;
        public double VM_Rudder{ get { return rudder;}  set { rudder = value; model.SetRudder(value); } }
        private double aileron;
        public double VM_Aileron { get { return aileron; } set { aileron = value; model.SetAileron(value); } }
        private double throttle;
        public double VM_Throttle { get { return throttle;  } set { throttle = value; model.SetThrottle(value); } }
        private double elevator;
        public double VM_Elevator { get { return elevator; } set { elevator = value; model.SetElevator(value); } }
        public VMControlers(Model m)
        {
            this.model = m;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        }

    }
}
