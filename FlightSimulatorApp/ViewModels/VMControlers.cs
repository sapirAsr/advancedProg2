using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    public class VMControlers : INotifyPropertyChanged
    {
        //
        public Model model;
        private string rudder;
        private string aileron;
        private string throttle;
        private string elevator;
        public string VM_Rudder{ set { rudder = value; model.SetRudder(value); } }
        public string VM_aileron { set { aileron = value; model.SetAileron(value); } }
        public string VM_throttle { set { throttle = value; model.SetThrottle(value); } }
        public string VM_Elevator { set { elevator = value; model.SetElevator(value); } }
        public event PropertyChangedEventHandler PropertyChanged;

        public VMControlers(Model m)
        {
            this.model = m;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}