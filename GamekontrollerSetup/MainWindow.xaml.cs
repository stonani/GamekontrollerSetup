using GamecontrollerLibaray;
using GamecontrollerLibaray.Controllers;
using GamecontrollerLibaray.DataObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace GamekontrollerSetup
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sensorB_Click(object sender, RoutedEventArgs e)
        {
            SensorWindow sensorW = new SensorWindow();
            sensorW.Show();
        }

        private void exitB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void activateB_Click(object sender, RoutedEventArgs e)
        {
            BlockingCollection<GyroDataContainer> dataQueue = new BlockingCollection<GyroDataContainer>();
            GyroDataContainer gyroDataContainer = new GyroDataContainer();

            var mpuSensor = new DummyMPUSensor();
            var sensorController = new SensorController(mpuSensor,dataQueue, gyroDataContainer);
            var keyboard = new PressKeyboardWindows();
            
            var movementController = new MovementController(keyboard);
            var gyroControl = new GyroController(dataQueue, movementController);


            var gyroControlThread = new Thread(gyroControl.Run);

            //mpuSensorThread.Start();
            gyroControlThread.Start();
        }
    }
}
