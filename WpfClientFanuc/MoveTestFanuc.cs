using FRRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClientFanuc
{

   

public class MoveTestFanuc
{
    FRCRobot robot;
    public MoveTestFanuc()
    {
        robot = new FRCRobot();
    }

    public FRCRobot ConnectFanuc()
    {
        try
        {
            robot.Connect("127.0.0.1");
            return robot;
        }
        catch (Exception e)
        {
            MessageBox.Show("Error: " + e.Message);
                return robot;
        }

    }

}
}
