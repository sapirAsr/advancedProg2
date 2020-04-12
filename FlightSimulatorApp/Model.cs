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
        private double throttle;
        private double aileron ;
        private double elevator;
        private double rudder;
        //dashboard
        private double latitude;
        private double longitude;
        private double airSpeed;
        private double altitude;
        private double roll;
        private double pitch;
        private double altimeter;
        private double heading;
        private double groundSpeed;
        private double verticalSpeed;

        public Model(ITelnetClient telnetClient) {
            this.telnetClient = telnetClient;
            stop = false; 
        }

        //controlers properties
        public double Throttle { get { return throttle; } set { throttle = value; NotifyPropertyChanged("throttle"); } }
        public double Aileron { get { return aileron; } set { aileron = value; NotifyPropertyChanged("aileron"); } }
     
        public double Elevator { get { return elevator; } set { elevator = value; NotifyPropertyChanged("elevator"); } }
        public double Rudder { get { return rudder; } set { rudder = value; NotifyPropertyChanged("rudder"); } }

        //dashboard properties
        public double Latitude { get { return latitude; } set { latitude = value; NotifyPropertyChanged("latitude"); } }
        public double Longitude { get { return longitude; } set { longitude = value; NotifyPropertyChanged("longitude"); } }
        public double AirSpeed { get { return airSpeed; } set { airSpeed = value; NotifyPropertyChanged("airSpeed"); } }
        public double Altitude { get { return altitude; } set { altitude = value; NotifyPropertyChanged("altitude"); } }
        public double Roll { get { return roll; } set { roll = value; NotifyPropertyChanged("roll"); } }
        public double Pitch { get { return pitch; } set { pitch = value; NotifyPropertyChanged("pitch"); } }
        public double Altimeter { get { return altimeter; } set { altimeter = value; NotifyPropertyChanged("altimeter"); } }
        public double Heading { get { return heading; } set { heading = value; NotifyPropertyChanged("heading"); } }
        public double GroundSpeed { get { return groundSpeed; } set { groundSpeed = value; NotifyPropertyChanged("groundSpeed"); } }
        public double VerticalSpeed {  get { return verticalSpeed; } set { verticalSpeed = value; NotifyPropertyChanged("verticalSpeed"); } }
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
                    try 
                    {
                        latitude = double.Parse(telnetClient.read());
                    } catch (Exception)
                    {
                        latitude = double.NaN;
                    }
                    telnetClient.write("get /position/longitude-deg\n");
                    try
                    {
                        longitude = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        longitude = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    try
                    {
                        telnetClient.write(AirSpeed.ToString());
                        airSpeed = Convert.ToDouble(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        airSpeed = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    try
                    {
                        altitude = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        altitude = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    try
                    {
                        roll = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        roll = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    try
                    {
                        pitch = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        pitch = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    try
                    {
                        altimeter = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        altimeter = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                    try
                    {
                        heading = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        heading = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    try
                    {
                        groundSpeed = double.Parse(telnetClient.read());
                    }
                    catch
                    {
                        groundSpeed = double.NaN;
                    }
                    telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    try
                    {
                        verticalSpeed = double.Parse(telnetClient.read());
                    }
                    catch (Exception)
                    {
                        verticalSpeed = double.NaN;
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
