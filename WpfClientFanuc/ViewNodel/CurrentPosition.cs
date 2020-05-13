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
        private string x;
        public string X { 
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
        private string y;
        public string Y
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
        private string z;
        public string Z
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

        private string w;
        public string W
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

        private string p;
        public string P
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

        private string r;
        public string R
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

                X = xyzWprCur.X.ToString();
                Y = xyzWprCur.Y.ToString();
                Z = xyzWprCur.Z.ToString();
                P = xyzWprCur.P.ToString();
                R = xyzWprCur.R.ToString();
                W = xyzWprCur.W.ToString();
                //MessageBox.Show(xyzWprCur.X.ToString() + " " + xyzWprCur.Y.ToString() + " " + xyzWprCur.Z.ToString() +
                //                " " + xyzWprCur.P.ToString() + " " + xyzWprCur.R.ToString() + " " + xyzWprCur.W.ToString());
            }
        }
    }
}
