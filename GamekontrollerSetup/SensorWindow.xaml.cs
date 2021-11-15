using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using GamecontrollerLibaray.DataObjects;

namespace GamekontrollerSetup
{
    /// <summary>
    /// Interaction logic for SensorWindow.xaml
    /// </summary>
    public partial class SensorWindow : Window
    {
        double _joyXdata = 90;
        double _joyYdata = 100;
        SerialPort _SerialPort;
        BlockingCollection<JoystickDataContainer> _joyDataQueue = new BlockingCollection<JoystickDataContainer>();

        public SensorWindow()
        {
            InitializeComponent();

            //initilize serial port
            _SerialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

            //initilize windows elements
            StopDataB.IsEnabled = false;
        }

        private void backB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReadDataB_Click(object sender, RoutedEventArgs e)
        {
            //Set windows elements
            ReadDataB.IsEnabled = false;
            StopDataB.IsEnabled = true;

            _SerialPort.Open();
            _SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);


        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            string[] DataList = indata.Split('\n');
            string[] DataPartList = DataList[1].Split(',');

           
        }

        public void setStatus()
        {
            JoyStickXT.Text = Convert.ToString(_joyXdata);
            JoyStickYT.Text = Convert.ToString(_joyYdata);
        }

        private void StopDataB_Click(object sender, RoutedEventArgs e)
        {
            //Set windows elements
            StopDataB.IsEnabled = false;
            ReadDataB.IsEnabled = true;

            _SerialPort.Close();
        }
    }
}
