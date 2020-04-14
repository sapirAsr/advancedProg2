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
        
        public Model model;
        public event PropertyChangedEventHandler PropertyChanged;

        public string VM_heading { get { return model.Heading; } }
        public string VM_verticalSpeed { get { return model.VerticalSpeed; } }
        public string VM_groundSpeed { get { return model.GroundSpeed; } }
        public string VM_airSpeed {  get { return model.AirSpeed; } }
        public string VM_altitude {  get { return model.Altitude; } }
        public string VM_roll { get { return model.Roll; } }
        public string VM_pitch { get { return model.Pitch; } }
        public string VM_altimeter { get { return model.Altimeter; } }
      
        public VMDashboard(Model model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                Console.WriteLine("***");
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
