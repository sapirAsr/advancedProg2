﻿using FlightSimulatorApp.ViewModels;
using FlightSimulatorApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
            /**...
            Model model = new Model(new TelnetClient());
            vmDash = new VMDashboard(model);
            vmC = new VMControlers(model);
            mapVm = new MapVM(model);
            model.connect("localhost", 5402);
            model.start();
            DataContext = vmDash;    
             */
        }
    }
}
