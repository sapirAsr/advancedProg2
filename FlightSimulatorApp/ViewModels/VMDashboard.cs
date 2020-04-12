using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    public class VMDashboard : INotifyPropertyChanged
    {
        //
        public Model model;
        public string VM_Heading { get { return model.Heading; } }
        public string VM_VerticalSpeed { get { return model.VerticalSpeed; } }
        public string VM_GroundSpeed { get { return model.GroundSpeed; } }
        public string VM_AirSpeed {  get { return model.AirSpeed; } }
        public string VM_Altitude {  get { return model.Altitude; } }
        public string VM_Roll { get { return model.Roll; } }
        public string VM_Pitch { get { return model.Pitch; } }
        public string VM_Altimeter { get { return model.Altimeter; } }
      
        public VMDashboard(Model m)
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
