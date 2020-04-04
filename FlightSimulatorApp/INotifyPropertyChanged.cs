﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlightSimulatorApp
{
    interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }
    public delegate void PropertyChangedEventHandler(Object sender, PropertyChangedEventArgs e);

}
