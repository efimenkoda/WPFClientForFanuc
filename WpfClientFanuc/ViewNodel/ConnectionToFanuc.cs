﻿using FRRobot;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClientFanuc.View.ViewModel;

namespace WpfClientFanuc
{



    public class ConnectionToFanuc
    {
        public FRCRobot robot;

        public string IPaddress { get; set; }
        public ConnectionToFanuc()
        {
            robot = new FRCRobot();
            IPaddress = ConfigurationManager.AppSettings["IPaddressFanuc"];
        }

        

        public void ConnectFanuc()
        {
            try
            {
                robot.Connect(IPaddress);
                
                //MessageBox.Show("Fanuc to Connect!");

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                
            }

        }



    }
}
