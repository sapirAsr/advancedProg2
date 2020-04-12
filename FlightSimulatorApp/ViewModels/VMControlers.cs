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
        public string VM_Rudder{ set { rudder = value; model.SetRudder(value); } }
        private string aileron;
        public string VM_Aileron { set { aileron = value; model.SetAileron(value); } }
        private string throttle;
        public string VM_Throttle { set { throttle = value; model.SetThrottle(value); } }
        private string elevator;
        public string VM_Elevator { set { elevator = value; model.SetElevator(value); } }
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