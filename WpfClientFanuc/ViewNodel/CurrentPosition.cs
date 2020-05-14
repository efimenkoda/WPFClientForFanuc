using FRRobot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClientFanuc
{
    public class CurrentPosition: INotifyPropertyChanged
    {
        private FRCRobot robot;
        private double x;
        public double X { 
            get
            {
                return x;
            }
            set
            {
                x = value;
                PropertyChanged(this, new PropertyChangedEventArgs("X"));
            }
        }
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Y"));
            }
        }
        private double z;
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Z"));
            }
        }

        private double w;
        public double W
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
                PropertyChanged(this, new PropertyChangedEventArgs("W"));
            }
        }

        private double p;
        public double P
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
                PropertyChanged(this, new PropertyChangedEventArgs("P"));
            }
        }

        private double r;
        public double R
        {
            get
            {
                return r;
            }
            set
            {
                r = value;
                PropertyChanged(this, new PropertyChangedEventArgs("R"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public CurrentPosition(FRCRobot robot)
        {
            this.robot = robot;
        }

        public void GetCurPosition()
        {
            if (robot.IsConnected)
            {
                //FRCSysPositions sysPositions = robot.RegPositions;
                //FRCSysPosition sysPosition = sysPositions[1];
                //FRCSysGroupPosition sysGroupPosition = sysPosition.Group[1];
                //FRCXyzWpr xyzWprSys = sysGroupPosition.Formats[FRETypeCodeConstants.frXyzWpr];                

                //X = xyzWprSys.X.ToString();
                //Y = xyzWprSys.Y.ToString();
                //Z = xyzWprSys.Z.ToString();
                //P = xyzWprSys.P.ToString();
                //R = xyzWprSys.R.ToString();
                //W = xyzWprSys.W.ToString();

                FRCCurPosition curPositions = robot.CurPosition;
                FRCCurGroupPosition groupCurPosition = curPositions.Group[1,FRECurPositionConstants.frJointDisplayType];
                FRCXyzWpr xyzWprCur = groupCurPosition.Formats[FRETypeCodeConstants.frExtXyzWpr];

                X = Math.Round(xyzWprCur.X,3);
                Y = Math.Round(xyzWprCur.Y,3);
                Z = Math.Round(xyzWprCur.Z, 3);
                P = Math.Round(xyzWprCur.P, 3);
                R = Math.Round(xyzWprCur.R, 3);
                W = Math.Round(xyzWprCur.W, 3);
                
            }
        }
    }
}
