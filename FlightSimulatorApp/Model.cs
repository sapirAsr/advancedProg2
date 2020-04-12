using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class Model : IModel
    {
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;
        volatile Boolean stop;
        bool b = true;
        //controlers
        private string throttle;
        private string aileron;
        private string elevator;
        private string rudder;
        //dashboard
        private string latitude;
        private string longitude;
        private string airSpeed;
        private string altitude;
        private string roll;
        private string pitch;
        private string altimeter;
        private string heading;
        private string groundSpeed;
        private string verticalSpeed;

        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
        }

        //controlers properties
        public string Throttle { get { return throttle; } set { throttle = value; NotifyPropertyChanged("Throttle"); } }
        public string Aileron { get { return aileron; } set { aileron = value; NotifyPropertyChanged("Aileron"); } }
        public string Elevator { get { return elevator; } set { elevator = value; NotifyPropertyChanged("Elevator"); } }
        public string Rudder { get { return rudder; } set { rudder = value; NotifyPropertyChanged("Rudder"); } }

        //dashboard properties
        public string Latitude { get { return latitude; } set { latitude = value; NotifyPropertyChanged("Latitude"); } }
        public string Longitude { get { return longitude; } set { longitude = value; NotifyPropertyChanged("Longitude"); } }
        public string AirSpeed { get { return airSpeed; } set { airSpeed = value; NotifyPropertyChanged("AirSpeed"); } }
        public string Altitude { get { return altitude; } set { altitude = value; NotifyPropertyChanged("Altitude"); } }
        public string Roll { get { return roll; } set { roll = value; NotifyPropertyChanged("Roll"); } }
        public string Pitch { get { return pitch; } set { pitch = value; NotifyPropertyChanged("Pitch"); } }
        public string Altimeter { get { return altimeter; } set { altimeter = value; NotifyPropertyChanged("Altimeter"); } }
        public string Heading { get { return heading; } set { heading = value; NotifyPropertyChanged("Heading"); } }
        public string GroundSpeed { get { return groundSpeed; } set { groundSpeed = value; NotifyPropertyChanged("GroundSpeed"); } }
        public string VerticalSpeed { get { return verticalSpeed; } set { verticalSpeed = value; NotifyPropertyChanged("VerticalSpeed"); } }
        public void connect(string ip, int port)
        {
            telnetClient.connect(ip, port);
        }

        public void disconnect()
        {
            stop = true;
            telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    //controlers joystick
                    /**telnetClient.write("get /controls/engines/current-engine/throttle\n");
                    Throttle = double.Parse(telnetClient.read());
                    telnetClient.write("get /controls/flight/aileron\n");
                    Aileron = double.Parse(telnetClient.read());
                    telnetClient.write("get /controls/flight/elevator\n");
                    Elevator = double.Parse(telnetClient.read());
                    telnetClient.write("get /controls/flight/rudder\n");
                    Rudder = double.Parse(telnetClient.read());**/
                    //dashboard
                    telnetClient.write("get /position/latitude-deg\n");
                    Latitude = telnetClient.read();
                    Console.WriteLine(Latitude);
                    telnetClient.write("get /position/longitude-deg\n");
                    Longitude = telnetClient.read();
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    AirSpeed = telnetClient.read();
                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    Altitude = telnetClient.read();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    Roll = telnetClient.read();
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    Pitch = telnetClient.read();
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    Altimeter = telnetClient.read();
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    Heading = telnetClient.read();
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    GroundSpeed = telnetClient.read();
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    VerticalSpeed = telnetClient.read();
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void SetAileron(string number)
        {
            telnetClient.write("set /controls/flight/aileron " + number + "\n");
        }

        public void SetThrottle(string number)
        {
            telnetClient.write("set /controls/engines/current-engine/throttle " + number + "\n");
        }

        public void SetElevator(string number)
        {
            telnetClient.write("set /controls/flight/rudder " + number + "\n");
        }

        public void SetRudder(string number)
        {
            telnetClient.write("get /controls/flight/rudder " + number + "\n");
        }
    }
}