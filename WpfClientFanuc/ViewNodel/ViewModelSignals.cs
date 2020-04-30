﻿using FRRobot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClientFanuc.View.ViewModel;

namespace WpfClientFanuc
{
    public class ViewModelSignals
    {
        FRCRobot robot;
        private RelayCommand connectCommand;
        public ConnectionToFanuc Connect { get; private set; }
        public ObservableCollection<IOSignals> Signals { get; set; }
        public ViewModelSignals()
        {
            Connect = new ConnectionToFanuc();
            Signals = new ObservableCollection<IOSignals>();
        }

        private void DigitalOutIO()
        {
            for (int i = 1; i <= 512; i++)
            {
                try
                {
                    FRCDigitalIOType dioType = (FRCDigitalIOType)robot.IOTypes[FREIOTypeConstants.frDInType];
                    FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)dioType.Signals[i];
                    Signals.Add(new IOSignals { NumberIO = "DIN " + i, Status = dioSignal.Value });
                }
                catch
                {

                }
            }
        }

        public RelayCommand ConnectCommand
        {
            get
            {
                return connectCommand ??
                    (connectCommand = new RelayCommand(obj =>
                    {
                        Connect.ConnectFanuc();
                        robot = Connect.robot;


                        if (robot.IsConnected)
                        {
                            DigitalOutIO();
                        }
                    }));

            }
        }
    }
}