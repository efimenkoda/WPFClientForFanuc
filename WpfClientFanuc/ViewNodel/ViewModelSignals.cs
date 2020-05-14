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
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfClientFanuc;

namespace WpfClientFanuc
{
    public class ViewModelSignals : INotifyPropertyChanged
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        FRCRobot robot;
        private RelayCommand connectCommand;
        private RelayCommand selectItemTypeIO;
        private string btnTextConnect;
        public string BtnTextConnect
        {
            get
            {
                if(btnTextConnect==null)
                    btnTextConnect = "Подключить";
                return btnTextConnect;
            }
            set
            {
                btnTextConnect = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BtnTextConnect"));
            }
        }
        
        public CurrentPosition curPosition { get; set; }
        public ConnectionToFanuc Connect { get; private set; }
        public ObservableCollection<IOSignals> Signals { get; set; }

        //public ObservableCollection<IOSignals> Signals {
        //    get
        //    {
        //        return signals;
        //    }
        //        set
        //    {
        //        signals = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs("Signals"));
        //    }
        //}
        public Dictionary<string, FREIOTypeConstants> listTypeIO;
        private RelayCommand setItemIO;

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
                listTypeIO = value;
            }
        }
        public ViewModelSignals()
        {
            Connect = new ConnectionToFanuc();            
            Signals = new ObservableCollection<IOSignals>();
            curPosition = new CurrentPosition(Connect.robot);
        }

        //protected void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        private void SetSignalsIO(FRCRobot robot, FREIOTypeConstants ioTypeConstant, long index, bool value)
        {
            try
            {
                if (robot.IsConnected)
                {
                    switch (ioTypeConstant)
                    {
                        case FREIOTypeConstants.frDInType:
                        case FREIOTypeConstants.frDOutType:
                            FRCDigitalIOSignal dioSignal = (FRCDigitalIOSignal)((FRCDigitalIOType)robot.IOTypes[ioTypeConstant]).Signals[index];
                            dioSignal.Value = value;
                            dioSignal.Update();
                            break;
                        case FREIOTypeConstants.frUOPInType:
                        case FREIOTypeConstants.frUOPOutType:
                            FRCUOPIOSignal uop = (FRCUOPIOSignal)((FRCUOPIOType)robot.IOTypes[ioTypeConstant]).Signals[index];
                            uop.Value = value;
                            uop.Update();
                            break;
                        case FREIOTypeConstants.frSOPInType:
                        case FREIOTypeConstants.frSOPOutType:
                            FRCSOPIOSignal sop = (FRCSOPIOSignal)((FRCSOPIOType)robot.IOTypes[ioTypeConstant]).Signals[index];
                            sop.Value = value;
                            sop.Update();
                            break;                        
                        case FREIOTypeConstants.frFlagType:
                            FRCFlagSignal flagSignal = (FRCFlagSignal)((FRCFlagType)robot.IOTypes[ioTypeConstant]).Signals[index];
                            flagSignal.Value = value;
                            flagSignal.Update();
                            break;
                        case FREIOTypeConstants.frRDInType:
                        case FREIOTypeConstants.frRDOutType:
                            FRCRobotIOSignal rd = (FRCRobotIOSignal)((FRCRobotIOType)robot.IOTypes[ioTypeConstant]).Signals[index];
                            rd.Value = value;
                            rd.Update();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void GetSignalsIO(FRCRobot robot, FREIOTypeConstants ioTypeConstant)
        {
            try
            {
                Signals.Clear();
                if (robot.IsConnected)
                {
                    switch (ioTypeConstant)
                    {
                        //DIN DOUT
                        case FREIOTypeConstants.frDOutType:                        
                        case FREIOTypeConstants.frDInType:
                            FRCDigitalIOType dioTypeIn = (FRCDigitalIOType)robot.IOTypes[ioTypeConstant];
                            foreach (FRCDigitalIOSignal item in dioTypeIn.Signals)
                            {
                                item.StartMonitor(1);
                                Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, Comment = item.Comment, TypeConstants=ioTypeConstant });
                            }
                            break;
                        //UI UO
                        case FREIOTypeConstants.frUOPInType:                       
                        case FREIOTypeConstants.frUOPOutType:
                            FRCUOPIOType TypeUO = (FRCUOPIOType)robot.IOTypes[ioTypeConstant];
                            foreach (FRCUOPIOSignal item in TypeUO.Signals)
                            {
                                item.StartMonitor(1);
                                Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, Comment = item.Comment, TypeConstants = ioTypeConstant });
                            }
                            break;
                        //SI SO
                        case FREIOTypeConstants.frSOPInType:                        
                        case FREIOTypeConstants.frSOPOutType:
                            FRCSOPIOType TypeSO = (FRCSOPIOType)robot.IOTypes[ioTypeConstant];
                            foreach (FRCSOPIOSignal item in TypeSO.Signals)
                            {
                                Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, Comment = item.Comment, TypeConstants = ioTypeConstant });
                            }
                            break;
                        //FLAG
                        case FREIOTypeConstants.frFlagType:
                            FRCFlagType TypeFlag = (FRCFlagType)robot.IOTypes[ioTypeConstant];
                            foreach (FRCFlagSignal item in TypeFlag.Signals)
                            {
                                Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, Comment = item.Comment, TypeConstants = ioTypeConstant });
                            }
                            break;
                        //GI GO
                        //case FREIOTypeConstants.frGPInType:
                        //FRCGroupIOType TypeGI = (FRCGroupIOType)robot.IOTypes[ioTypeConstant];
                        //foreach (FRCGroupIOSignal item in TypeGI.Signals)
                        //{
                        //    Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Value = item.Value, Comment = item.Comment });
                        //}
                        //break;
                        //case FREIOTypeConstants.frGPOutType:
                        //    FRCGroupIOType TypeGO = (FRCGroupIOType)robot.IOTypes[ioTypeConstant];
                        //    foreach (FRCGroupIOSignal item in TypeGO.Signals)
                        //    {
                        //        Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Value = item.Value, Comment = item.Comment });
                        //    }
                        //    break;
                        //RO RI
                        case FREIOTypeConstants.frRDOutType:                        
                        case FREIOTypeConstants.frRDInType:
                            FRCRobotIOType TypeRI = (FRCRobotIOType)robot.IOTypes[ioTypeConstant];
                            foreach (FRCRobotIOSignal item in TypeRI.Signals)
                            {
                                Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, Comment = item.Comment, TypeConstants = ioTypeConstant });
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public RelayCommand ConnectCommand
        {
            get
            {
                return connectCommand ??
                    (connectCommand = new RelayCommand(obj =>
                    {
                        string strIP = obj as string;
                        string sRegexIPAddressPattern = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
                        Regex rIPAddress = new Regex(sRegexIPAddressPattern);
                        if (rIPAddress.Match(strIP).Success)
                        {
                            Connect.ConnectFanuc();
                            robot = Connect.robot;

                            if (robot.IsConnected)
                            {
                                BtnTextConnect = "Подключено";
                                //Form.BtnConnect.Content = "Подключено";
                                GetSignalsIO(robot, FREIOTypeConstants.frDOutType);                                
                                curPosition.GetCurPosition();
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный формат IP адреса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        try
                        {
                            if (robot.IsConnected)
                            {
                                KeyValuePair<string, FREIOTypeConstants> selected = (KeyValuePair<string, FREIOTypeConstants>)obj;
                                GetSignalsIO(robot, selected.Value);
                            }

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Отсутствует подключение к роботу: ");

                        }

                    }));
            }
        }

        public RelayCommand SetItemIO
        {
            get
            {
                return setItemIO ??
                    (setItemIO = new RelayCommand(obj =>
                    {
                        try
                        {
                            if (robot.IsConnected)
                            {
                                //IOSignals selectedIO = Form.ListItemIO.SelectedItem as IOSignals;
                                int numberIO = (int)obj;
                                //KeyValuePair<string, FREIOTypeConstants> selectedTypeIO = (KeyValuePair<string, FREIOTypeConstants>)obj;
                                foreach (var item in Signals)
                                {
                                    if(item.NumberIO==numberIO)
                                    {
                                        SetSignalsIO(robot, item.TypeConstants, item.NumberIO, item.Status);
                                        GetSignalsIO(robot, item.TypeConstants);
                                        break;
                                    }
                                }
                                //SetSignalsIO(robot, selectedTypeIO.Value, selectedIO.NumberIO, !selectedIO.Status);
                                //GetSignalsIO(robot, selectedTypeIO.Value);
                            }

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Отсутствует подключение к роботу: ");
                        }

                    }));
            }
        }
    }


}
