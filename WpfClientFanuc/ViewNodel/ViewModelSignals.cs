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
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.Collections.Specialized;


namespace WpfClientFanuc
{

    public class ViewModelSignals : INotifyPropertyChanged
    {
        private FRCRobot robot;
        //выбранный текущий тип сигнала
        private FREIOTypeConstants ioTypeConstant;
        private RelayCommand connectCommand;
        private RelayCommand selectItemTypeIO;
        private RelayCommand inputIPAddress;
        private string btnTextConnect;
        /// <summary>
        /// кнопка подключения к роботу
        /// </summary>
        public string BtnTextConnect
        {
            get
            {
                if (btnTextConnect == null)
                    btnTextConnect = "Подключить";
                return btnTextConnect;
            }
            set
            {
                btnTextConnect = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BtnTextConnect"));
            }
        }

        public CurrentPosition CurPosition { get; set; }
        public ConnectionToFanuc Connect { get; private set; }

        public ObservableCollection<IOSignals> Signals { get; set; }

        public Dictionary<string, FREIOTypeConstants> listTypeIO;
        private RelayCommand setItemIO;

        public event PropertyChangedEventHandler PropertyChanged;
        private DispatcherTimer timer;
        private RelayCommand curPos;

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

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick1);
            timer.Interval = new TimeSpan(0, 0, 1);            

            Connect = new ConnectionToFanuc();
            Signals = new ObservableCollection<IOSignals>();
            CurPosition = new CurrentPosition(Connect.robot);

        }
        /// <summary>
        /// Асинхронный вызов метода GetSignalsIO каждую секунду.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void timer_Tick1(object sender, EventArgs e)
        {
            await GetSignalsIO(robot, ioTypeConstant);
        }

  
        /// <summary>
        /// Метод изменения значения сигнала
        /// </summary>
        /// <param name="robot">экземпляр подключения к роботу</param>
        /// <param name="ioTypeConstant">константа типа сигнала</param>
        /// <param name="index">индекс сигнала, значение которое нужно изменить</param>
        /// <param name="value">новое значение сигнала</param>
        private async void SetSignalsIO(FRCRobot robot, FREIOTypeConstants ioTypeConstant, long index, bool value)
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
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Signals.Clear();
                await GetSignalsIO(robot, ioTypeConstant);
            }
        }

        /// <summary>
        /// Получение списка сигналов и передача в список Signals
        /// </summary>
        /// <param name="robot">экземпляр подключения к роботу</param>
        /// <param name="ioTypeConstant">константа типа сигналов</param>
        /// <returns>возвращаемое значение-текущая задача</returns>
        private async Task GetSignalsIO(FRCRobot robot, FREIOTypeConstants ioTypeConstant)
        {
            
            Task task = new Task(() =>
              {
                  Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                  {
                      try
                      {
                          
                          if (robot.IsConnected)
                          {
                              Signals.Clear();
                              switch (ioTypeConstant)
                              {
                                  //DIN DOUT
                                  case FREIOTypeConstants.frDOutType:
                                  case FREIOTypeConstants.frDInType:
                                      FRCDigitalIOType dioTypeIn = (FRCDigitalIOType)robot.IOTypes[ioTypeConstant];
                                          foreach (FRCDigitalIOSignal item in dioTypeIn.Signals)
                                          {
                                          
                                              Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, 
                                                         Comment = item.Comment, TypeConstants = ioTypeConstant });

                                          }
                              
                              break;
                                  //UI UO
                                  case FREIOTypeConstants.frUOPInType:
                                  case FREIOTypeConstants.frUOPOutType:
                                      FRCUOPIOType TypeUO = (FRCUOPIOType)robot.IOTypes[ioTypeConstant];
                                      foreach (FRCUOPIOSignal item in TypeUO.Signals)
                                      {
                                          Signals.Add(new IOSignals { NumberIO = item.LogicalNum, Status = item.Value, 
                                                        Comment = item.Comment, TypeConstants = ioTypeConstant });
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
                          MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                      }
                  }));

              },TaskCreationOptions.LongRunning);

            task.Start();
            await task;

        }



        public Task Wait()
        {
            Task task = Task.Run(() =>
            {
                //this.Dispatcher.Invoke(() =>
                //{
                while (true)
                {

                }

                //});
            });
            return task;
        }

        /// <summary>
        /// Команда, вызываемая по нажатии на кнопку "подключение "
        /// </summary>
        public RelayCommand ConnectCommand
        {
            get
            {
                return connectCommand ??
                    (connectCommand = new RelayCommand(async obj =>
                  {

                      Connect.ConnectFanuc();
                      robot = Connect.robot;

                      if (robot.IsConnected)
                      {
                          BtnTextConnect = "Подключено";
                          KeyValuePair<string, FREIOTypeConstants> selected = (KeyValuePair<string, FREIOTypeConstants>)obj;
                          ioTypeConstant = selected.Value;
                          //запуск асинхронного вызова метода GetSignalsIO каждую секунду
                          //timer.Start();
                          await GetSignalsIO(robot, selected.Value);
                          CurPosition.GetCurPosition();

                      }

                  }));
            }
        }

        /// <summary>
        /// Команда получения текущей позиции и отображание координат в соответствующих TextBox'ах
        /// </summary>
        public RelayCommand CurPos
        {
            get
            {
                return curPos ??
                    (curPos = new RelayCommand(obj =>
                    {

                        if (robot.IsConnected)
                        {

                            CurPosition.GetCurPosition();

                        }

                    }));
            }
        }

        /// <summary>
        /// Поле ввода IP адреса проверяемое регулярным выражением
        /// </summary>
        public RelayCommand InputIPAddress
        {
            get
            {
                return inputIPAddress ??
                    (inputIPAddress = new RelayCommand(obj =>
                    {
                        try
                        {
                            string strIP = obj as string;
                            string sRegexIPAddressPattern = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
                            Regex rIPAddress = new Regex(sRegexIPAddressPattern);
                            if (!rIPAddress.Match(strIP).Success)
                            {
                                MessageBox.Show("Неверный формат IP адреса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Отсутствует подключение к роботу: ");

                        }

                    }));
            }
        }


        /// <summary>
        /// Команда запускаемая при выборе в ComboBox типа сигнала
        /// </summary>
        public RelayCommand SelectItemTypeIO
        {
            get
            {
                return selectItemTypeIO ??
                    (selectItemTypeIO = new RelayCommand(async obj =>
                   {
                       try
                       {
                           if (robot.IsConnected)
                           {
                               Signals.Clear();
                               KeyValuePair<string, FREIOTypeConstants> selected = (KeyValuePair<string, FREIOTypeConstants>)obj;
                               //await App.Current.Dispatcher.Invoke(async () =>
                               //{
                               
                               ioTypeConstant = selected.Value;
                               //запуск асинхронного вызова метода GetSignalsIO каждую секунду
                               //timer.Start();
                               await GetSignalsIO(robot, selected.Value);
                               //});
                           }

                       }
                       catch (Exception)
                       {
                           MessageBox.Show("Отсутствует подключение к роботу: ");

                       }

                   }));
            }
        }
        /// <summary>
        /// Команда выполняющая изменение сигнала, когда в ListView происходит событие IsChecked
        /// </summary>
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
                                int numberIO = (int)obj;                                
                                foreach (var item in Signals)
                                {
                                    if (item.NumberIO == numberIO)
                                    {
                                        SetSignalsIO(robot, item.TypeConstants, item.NumberIO, item.Status);                                        
                                        break;
                                    }
                                }
                            }
                        }


                        catch (Exception e)
                        {

                            MessageBox.Show("Отсутствует подключение к роботу " + e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            
                        }

                    }));
            }
        }
    }


}
