using FRRobot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClientFanuc
{
    public class DigitalOUT
    {
        public FRCRobot robot;
        public DigitalOUT(FRCRobot robot)
        {
            this.robot = robot;
        }
        public bool this[int index]
        {
            get
            {

                try
                {
                    FRCDigitalIOType dioType = (FRCDigitalIOType)robot.IOTypes[FREIOTypeConstants.frDOutType];
                    FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)dioType.Signals[index];
                    return dioSignal.Value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

            }
            set
            {
                try
                {
                    FRCDigitalIOType dioType = (FRCDigitalIOType)robot.IOTypes[FREIOTypeConstants.frDOutType];
                    FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)dioType.Signals[index];
                    dioSignal.Value = value;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }


        public Task<bool> Wait(int index)
        {
            Task<bool> task = Task.Run<bool>(() =>
            {
                //this.Dispatcher.Invoke(() =>
                //{
                while (this[index] == true)
                {

                }
                return this[index];
                //});
            });
            return task;
        }

    }
}
