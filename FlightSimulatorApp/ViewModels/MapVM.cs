using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    public class MapVM : INotifyPropertyChanged
    {
        //
        private Model model;
        public event PropertyChangedEventHandler PropertyChanged;
        public Location VM_location
        {
            get
            {
                return model.Location;
            }
        }
       
        public MapVM(Model m)
        {
            this.model = m;
            m.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

        }

       
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                Console.WriteLine("***mapVM************");
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
            
        }

    }
}
