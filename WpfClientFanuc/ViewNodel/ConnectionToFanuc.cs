using FRRobot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WpfClientFanuc.View.ViewModel;

namespace WpfClientFanuc
{
    /// <summary>
    /// Подключение к роботу
    /// </summary>
    public class ConnectionToFanuc
    {
        public FRCRobot robot;
        public string IPaddress { get; set; }
        
        public ConnectionToFanuc()
        {
            robot = new FRCRobot();
            IPaddress = ConfigurationManager.AppSettings["IPaddressFanuc"];
        }
       /// <summary>
       /// метод подключения
       /// </summary>
        public void ConnectFanuc()
        {
            try
            {
                robot.Connect(IPaddress);                

            }
            catch (Exception e)
            {
                MessageBox.Show("Error connection: " + e.Message);
                
            }
            
        }



    }
}
