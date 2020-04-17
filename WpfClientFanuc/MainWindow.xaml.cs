using FRRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClientFanuc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MoveTestFanuc testFanuc = new MoveTestFanuc();
        FRCRobot robot;
        DigitalOUT digitalOUT;
        public MainWindow()
        {
            InitializeComponent();
            Counter counter = new Counter();
            List<int> list = new List<int>();
            list.AddRange(counter.GetValues(81, 84));
            list.AddRange(counter.GetValues(101, 108));
            ValuesDOut.ItemsSource = list;
            
            robot=testFanuc.ConnectFanuc();
            digitalOUT = new DigitalOUT(robot);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            

            bool result=false;
            if (digitalOUT[Convert.ToInt32(ValuesDOut.SelectedItem)] == false)
            {
                Progress.Visibility = Visibility.Visible;
                circle.Fill = Brushes.Red;
                digitalOUT[Convert.ToInt32(ValuesDOut.SelectedItem)] = true;
                int index =Convert.ToInt32(ValuesDOut.SelectedItem);
                result=await digitalOUT.Wait(index);
                ValueLabelDOUT.Content = result.ToString();
                circle.Fill = Brushes.Green; 
            }
            else
            {
                circle.Fill = Brushes.Red;
            }

            Progress.Visibility = Visibility.Hidden;
        }


        private void ValuesDOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValueLabelDOUT.Content = digitalOUT[Convert.ToInt32(ValuesDOut.SelectedItem)];
        }

        private void SetDOUT_Click(object sender, RoutedEventArgs e)
        {
            if(digitalOUT[Convert.ToInt32(ValuesDOut.SelectedItem)]==true)
            {
                digitalOUT[Convert.ToInt32(ValuesDOut.SelectedItem)] = true;
            }
            else
            {
                digitalOUT[Convert.ToInt32(ValuesDOut.SelectedItem)] = false;
            }
            
        }
    }
}
