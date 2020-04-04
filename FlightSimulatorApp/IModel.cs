using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface IModel 
    {
        void connect(string ip, int port);
        void disconnect();
        void start();

        //controlers properties
        double throttle { set; get; }
        double aileron { set; get; }
        double elevator { set; get; }
        double rudder { set; get; }


    }
}
