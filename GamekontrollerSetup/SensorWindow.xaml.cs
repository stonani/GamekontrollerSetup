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
using GamecontrollerLibaray.Calculators;

namespace GamekontrollerSetup
{
    /// <summary>
    /// Interaction logic for SensorWindow.xaml
    /// </summary>
    public partial class SensorWindow : Window
    {
        private SensorController _sensorController;
        private Thread writeDataControlThread;
        private WriteDataController writeDataController;

        public SensorWindow(BlockingCollection<JoystickDataContainer> joyDataQueue, BlockingCollection<GyroDataContainer> gyroDataQueue, BlockingCollection<CameraDataContainer> cameraDataQueue, SensorController sensorController )
        {
            InitializeComponent();
            StopDataB.IsEnabled = false;
            _sensorController = sensorController;
            var writeData = new FormsWrite(this);
            writeDataController = new WriteDataController(writeData, joyDataQueue, gyroDataQueue, cameraDataQueue);


            writeDataController.handleData = false;
            writeDataControlThread = new Thread(writeDataController.Run);
            writeDataControlThread.IsBackground = true;

            writeDataControlThread.Start();
        }

        private void backB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ReadDataB_Click(object sender, RoutedEventArgs e)
        {
            StopDataB.IsEnabled = true;
            ReadDataB.IsEnabled = false;

            _sensorController.sendCommand("a");
            writeDataController.handleData = true;
        }

        private void StopDataB_Click(object sender, RoutedEventArgs e)
        {
            //Set windows elements
            StopDataB.IsEnabled = false;
            ReadDataB.IsEnabled = true;

            _sensorController.sendCommand("i");
            writeDataController.handleData = false;
        }
    }
}
