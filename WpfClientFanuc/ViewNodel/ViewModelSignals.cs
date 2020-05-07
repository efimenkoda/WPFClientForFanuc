using FRRobot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using WpfClientFanuc.View.ViewModel;
using WpfClientFanuc.Properties;
using System.Windows.Controls;
using System.ComponentModel;

namespace WpfClientFanuc
{
    public class ViewModelSignals :INotifyPropertyChanged
    {
        FRCRobot robot;
        private RelayCommand connectCommand;
        private RelayCommand selectItemTypeIO;

        public string BtnConnectContent { get; set; }
        public ConnectionToFanuc Connect { get; private set; }
        public ObservableCollection<IOSignals> Signals { get; set; }
        public Dictionary<string, FREIOTypeConstants> listTypeIO;

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, FREIOTypeConstants> ListTypeIO
        {
            get
            {
                listTypeIO = new Dictionary<string, FREIOTypeConstants>();
                listTypeIO.Add("DIN", FREIOTypeConstants.frDInType);
                listTypeIO.Add("DOUT", FREIOTypeConstants.frDOutType);
                listTypeIO.Add("UI", FREIOTypeConstants.frUOPInType);
                listTypeIO.Add("UO", FREIOTypeConstants.frUOPOutType);
                listTypeIO.Add("SI", FREIOTypeConstants.frSOPInType);
                listTypeIO.Add("SO", FREIOTypeConstants.frSOPOutType);
                listTypeIO.Add("RI", FREIOTypeConstants.frRDInType);
                listTypeIO.Add("RO", FREIOTypeConstants.frRDOutType);
                listTypeIO.Add("FLG", FREIOTypeConstants.frFlagType);
                return listTypeIO;
            }
            set
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ListTypeIO"));
            }
        }
        public ViewModelSignals()
        {
            BtnConnectContent = "Подключить";           

            Connect = new ConnectionToFanuc();
            Signals = new ObservableCollection<IOSignals>();
        }


        private void DigitalOutIO(FRCRobot robot, FREIOTypeConstants ioTypeConstant)
        {

            Signals.Clear();
            switch (ioTypeConstant)
            {
                case FREIOTypeConstants.frDOutType:
                    FRCDigitalIOType dioType = (FRCDigitalIOType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCDigitalIOSignal item in dioType.Signals)
                    {                        
                        Signals.Add(new IOSignals { NumberIO = "DOUT " + item.LogicalNum, Status = item.Value });
                    }
                    break;
                case FREIOTypeConstants.frDInType:
                    FRCDigitalIOType dioTypeIn = (FRCDigitalIOType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCDigitalIOSignal item in dioTypeIn.Signals)
                    {
                        Signals.Add(new IOSignals { NumberIO = "DIN " + item.LogicalNum, Status = item.Value });
                    }
                    break;
                case FREIOTypeConstants.frUOPInType:
                    FRCUOPIOType TypeUI = (FRCUOPIOType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCUOPIOSignal item in TypeUI.Signals)
                    {
                        Signals.Add(new IOSignals { NumberIO = "UI " + item.LogicalNum, Status = item.Value });
                    }
                    break;
                case FREIOTypeConstants.frUOPOutType:
                    FRCUOPIOType TypeUO = (FRCUOPIOType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCUOPIOSignal item in TypeUO.Signals)
                    {
                        Signals.Add(new IOSignals { NumberIO = "UO " + item.LogicalNum, Status = item.Value });
                    }
                    break;
                case FREIOTypeConstants.frSOPInType:
                    FRCSOPIOType TypeSI = (FRCSOPIOType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCSOPIOSignal item in TypeSI.Signals)
                    {
                        Signals.Add(new IOSignals { NumberIO = "SI " + item.LogicalNum, Status = item.Value });
                    }
                    break;
                case FREIOTypeConstants.frSOPOutType:
                    FRCSOPIOType TypeSO = (FRCSOPIOType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCSOPIOSignal item in TypeSO.Signals)
                    {
                        Signals.Add(new IOSignals { NumberIO = "SO " + item.LogicalNum, Status = item.Value });
                    }
                    break;
                case FREIOTypeConstants.frFlagType:
                    FRCFlagType TypeFlag = (FRCFlagType)robot.IOTypes[ioTypeConstant];
                    foreach (FRCFlagSignal item in TypeFlag.Signals)
                    {
                        Signals.Add(new IOSignals { NumberIO = "FLAG " + item.LogicalNum, Status = item.Value });
                    }
                    break;
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
                            BtnConnectContent = "Подключено";
                            DigitalOutIO(robot, FREIOTypeConstants.frDOutType);
                        }

                    }));
            }
        }


        public RelayCommand SelectItemTypeIO
        {
            get
            {
                return selectItemTypeIO ??
                    (selectItemTypeIO = new RelayCommand(obj =>
                    {
                        MessageBox.Show(obj.ToString());
                    }));
            }
        }

        //private void SelectItemTypeIO(object sender, RoutedEventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
        //    MessageBox.Show(selectedItem.Content.ToString());
        //}

    }


}
