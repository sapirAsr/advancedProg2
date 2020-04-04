using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class Model : IModel
    {
        ITelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;
        volatile Boolean stop;
        //controlers
        private double Throttle;
        private double Aileron ;
        private double Elevator;
        private double Rudder;
        //dashboard
        private double Latitude;
        private double Longitude;
        private double AirSpeed;
        private double Altitude;
        private double Roll;
        private double Pitch;
        private double Altimeter;
        private double Heading;
        private double GroundSpeed;
        private double VerticalSpeed;
        public Model(ITelnetClient telnetClient) {
            this.telnetClient = telnetClient;
            stop = false; 
        }

        //controlers properties
        public double throttle { get { return Throttle; } set { Throttle = value; NotifyPropertyChanged("throttle"); } }
        public double aileron { get { return Aileron; } set { Aileron = value; NotifyPropertyChanged("aileron"); } }
     
        public double elevator { get { return Elevator; } set { Elevator = value; NotifyPropertyChanged("elevator"); } }
        public double rudder { get { return Rudder; } set { Rudder = value; NotifyPropertyChanged("rudder"); } }

        //dashboard properties
        public double latitude { get { return Latitude; } set { Latitude = value; NotifyPropertyChanged("latitude"); } }
        public double longitude { get { return Longitude; } set { Longitude = value; NotifyPropertyChanged("longitude"); } }
        public double airSpeed { get { return AirSpeed; } set { AirSpeed = value; NotifyPropertyChanged("airSpeed"); } }
        public double altitude { get { return Altitude; } set { Altitude = value; NotifyPropertyChanged("altitude"); } }
        public double roll { get { return Roll; } set { Roll = value; NotifyPropertyChanged("roll"); } }
        public double pitch { get { return Pitch; } set { Pitch = value; NotifyPropertyChanged("pitch"); } }
        public double altimeter { get { return Altimeter; } set { Altimeter = value; NotifyPropertyChanged("altimeter"); } }
        public double heading { get { return Heading; } set { Heading = value; NotifyPropertyChanged("heading"); } }
        public double groundSpeed { get { return GroundSpeed; } set { GroundSpeed = value; NotifyPropertyChanged("groundSpeed"); } }
        public double verticalSpeed {  get { return VerticalSpeed; } set { VerticalSpeed = value; NotifyPropertyChanged("verticalSpeed"); } }

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
                    telnetClient.write("get /controls/engines/current-engine/throttle\n");
                    Throttle = double.Parse(telnetClient.read());
                    telnetClient.write("get /controls/flight/aileron\n");
                    Aileron = double.Parse(telnetClient.read());
                    telnetClient.write("get /controls/flight/elevator\n");
                    Elevator = double.Parse(telnetClient.read());
                    telnetClient.write("get /controls/flight/rudder\n");
                    Rudder = double.Parse(telnetClient.read());
                    //dashboard
                    telnetClient.write("get /position/latitude-deg\n");
                    try 
                    {
                        Latitude = double.Parse(telnetClient.read());
                    } catch (Exception)
                    {
                        Latitude = double.NaN;
                    }
                    telnetClient.write("get /position/longitude-deg\n");
                    try
                    {
                        Longitude = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        Longitude = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    try
                    {
                        AirSpeed = double.Parse(telnetClient.read());

                    }
                    catch (Exception)
                    {
                        AirSpeed = double.NaN;

                    }
                    telnetClient.write("get '/instrumentation/gps/indicated-altitude-ft'\n");
                    try
                    {
                        Altitude = double.Parse(telnetClient.read());

                    }
                    catch (Exception)
                    {
                        Altitude = double.NaN;

                    }
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    try
                    {
                        Roll = double.Parse(telnetClient.read());

                    }
                    catch (Exception)
                    {
                        Roll = double.NaN;

                    }
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    try
                    {
                        Pitch = double.Parse(telnetClient.read());

                    }
                    catch (Exception)
                    {
                        Pitch = double.NaN;

                    }
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    try
                    {
                        Altimeter = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        Altimeter = double.NaN;

                    }
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    try
                    {
                        Heading = double.Parse(telnetClient.read());

                    }
                    catch (Exception)
                    {
                        Heading = double.NaN;

                    }
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    try
                    {
                        GroundSpeed = double.Parse(telnetClient.read());

                    }
                    catch
                    {
                        GroundSpeed = double.NaN;

                    }
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    try
                    {
                        VerticalSpeed = double.Parse(telnetClient.read());

                    }
                    catch (Exception)
                    {
                        VerticalSpeed = double.NaN;
                    }
                    Thread.Sleep(250);// read the data in 4Hz
                }
            }).Start();
        }

        public void NotifyPropertyChanged(string propName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void SetAileron(double number)
        {
            telnetClient.write("set /controls/flight/aileron " + number + "\n");
        }

        public void SetThrottle(double number)
        {
            telnetClient.write("set /controls/engines/current-engine/throttle " + number + "\n");
        }

        public void SetElevator(double number)
        {
            telnetClient.write("set /controls/flight/rudder " + number + "\n");
        }

        public void SetRudder(double number)
        {
            telnetClient.write("get /controls/flight/rudder " + number + "\n");
        }
    }
}
