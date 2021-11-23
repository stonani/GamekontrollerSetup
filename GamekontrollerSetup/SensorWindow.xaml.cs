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
using GamecontrollerLibaray;
using GamecontrollerLibaray.Controllers;

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
            //_SerialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

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
            //ReadDataB.IsEnabled = false;
            //StopDataB.IsEnabled = true;

            //_SerialPort.Open();
            //_SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            BlockingCollection<GyroDataContainer> dataQueue = new BlockingCollection<GyroDataContainer>();
            GyroDataContainer gyroDataContainer = new GyroDataContainer();

            var mpuSensor = new DummyMPUSensor();
            var sensorController = new SensorController(mpuSensor, dataQueue, gyroDataContainer);
            var keyboard = new PressKeyboardWindows();
            var writeData = new FormsWrite(this);

            var movementController = new MovementController(keyboard);
            var gyroControl = new GyroController(dataQueue, movementController);
            var writeDataController = new WriteDataController(writeData, dataQueue);


            var gyroControlThread = new Thread(gyroControl.Run);
            gyroControlThread.IsBackground = true;

            var writeDataControlThread = new Thread(writeDataController.Run);
            writeDataControlThread.IsBackground = true;

            gyroControlThread.Start();
            writeDataControlThread.Start();
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
