using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    public class MapVM
    {
        public Model model;
        public string VM_Longitude { get { return model.Longitude; } }
        public string VM_Latitude { get { return model.Latitude; } }

        public Location VM_Location { get { return new Location(Double.Parse(model.Latitude), Double.Parse(model.Longitude)); } }
       
        public MapVM(Model m)
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
