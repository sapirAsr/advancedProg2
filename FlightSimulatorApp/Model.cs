using Microsoft.Maps.MapControl.WPF;
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
        volatile Boolean connected;
        private static Mutex mut = new Mutex();
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
        private Location location;

        public Model(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;
            connected = false;
        }

        //controlers properties
        public string Throttle { get { return throttle; } set { throttle = value; NotifyPropertyChanged("throttle"); } }
        public string Aileron { get { return aileron; } set { aileron = value; NotifyPropertyChanged("aileron"); } }
        public string Elevator { get { return elevator; } set { elevator = value; NotifyPropertyChanged("elevator"); } }
        public string Rudder { get { return rudder; } set { rudder = value; NotifyPropertyChanged("rudder"); } }

        //dashboard properties
        public string Latitude { get { return latitude; } set { latitude = value; NotifyPropertyChanged("latitude"); } }
        public string Longitude { get { return longitude; } set { longitude = value; NotifyPropertyChanged("longitude");  } }
        public string AirSpeed { get { return airSpeed; } set { airSpeed = value; NotifyPropertyChanged("airSpeed"); } }
        public string Altitude { get { return altitude; } set { altitude = value; NotifyPropertyChanged("altitude"); } }
        public string Roll { get { return roll; } set { roll = value; NotifyPropertyChanged("roll"); } }
        public string Pitch { get { return pitch; } set { pitch = value; NotifyPropertyChanged("pitch"); } }
        public string Altimeter { get { return altimeter; } set { altimeter = value; NotifyPropertyChanged("altimeter"); } }
        public string Heading { get { return heading; } set { heading = value; NotifyPropertyChanged("heading"); } }
        public string GroundSpeed { get { return groundSpeed; } set { groundSpeed = value; NotifyPropertyChanged("groundSpeed"); } }
        public string VerticalSpeed { get { return verticalSpeed; } set { verticalSpeed = value; NotifyPropertyChanged("verticalSpeed"); } }
        
        public Location Location { get { return location; }  set { location = value; NotifyPropertyChanged("location"); } }
        public void connect(string ip, int port)
        {
            telnetClient.connect(ip, port);
            connected = true;
        }

        public void disconnect()
        {
            stop = true;
            connected = false;
            telnetClient.disconnect();
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    try
                    {
                        mut.WaitOne();
                        //dashboard
                        telnetClient.write("get /position/latitude-deg\n");
                        Latitude = telnetClient.read();
                        telnetClient.write("get /position/longitude-deg\n");
                        Longitude = telnetClient.read();
                        //for map
                        Location = new Location(Convert.ToDouble(Latitude), Convert.ToDouble(Longitude));
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
                    }
                    catch (Exception)
                    {

                    }
                    //Thread.Sleep(250);// read the data in 4Hz
                    mut.ReleaseMutex();

                }
            }).Start();
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));

            }
        }

        public void SetAileron(string number)
        {
            if (connected)
            {
                mut.WaitOne();
                telnetClient.write("set /controls/flight/aileron " + number + "\n");
                mut.ReleaseMutex();
            }
        }

        public void SetThrottle(string number)
        {
            if (connected)
            {
                mut.WaitOne();
                telnetClient.write("set /controls/engines/current-engine/throttle " + number + "\n");
                mut.ReleaseMutex();

            }
        }

        public void SetElevator(string number)
        {
            if (connected)
            {
                mut.WaitOne();
                telnetClient.write("set /controls/flight/elevator " + number + "\n");
                mut.ReleaseMutex();
            }
        }

        public void SetRudder(string number)
        {
            if (connected)
            {
                mut.WaitOne();
                telnetClient.write("set /controls/flight/rudder " + number + "\n");
                mut.ReleaseMutex();
            }
        }
    }
}