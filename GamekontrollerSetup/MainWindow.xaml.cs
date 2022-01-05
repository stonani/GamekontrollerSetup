using GamecontrollerLibaray;
using GamecontrollerLibaray.Controllers;
using GamecontrollerLibaray.DataObjects;
using GamecontrollerLibaray.Sensor;
using GamecontrollerLibaray.WindowsControl;
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
        private enum ArduinoState
        {
            Active,
            Deactive,
            Calibrating
        }
        private ArduinoState _currentArduinoState { get; set; }

        private SensorController _sensorControl;
        private DummyProbeController _dummyProbeControl;
        private DummyNeedleController _dummyNeedleController;

        private SerialSensor _mpuSensor;
        private RandomCameraSensor _cameraSensor;

        private NulpunkjusteringMessage _f;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private int tickCounter = 0;

        private Thread _probeThread;
        private Thread _needleThread;

        private BlockingCollection<GyroDataContainer> _gyroDataQueue;
        private BlockingCollection<CameraDataContainer> _cameraDataQueue;
        private BlockingCollection<JoystickDataContainer> _joystickDataQueue;

        public Main()
        {
            InitializeComponent();

            GyroDataContainer gyroDataContainer = new GyroDataContainer();
            _gyroDataQueue = new BlockingCollection<GyroDataContainer>();

            CameraDataContainer cameraDataContainer = new CameraDataContainer();
            _cameraDataQueue = new BlockingCollection<CameraDataContainer>();

            JoystickDataContainer joystickDataContainer = new JoystickDataContainer();
            _joystickDataQueue = new BlockingCollection<JoystickDataContainer>();

            _mpuSensor = new SerialSensor("COM3",115200, 5000);
            _cameraSensor = new RandomCameraSensor();

            _sensorControl = new SensorController(_mpuSensor, _cameraSensor, _gyroDataQueue, gyroDataContainer, _joystickDataQueue, joystickDataContainer, _cameraDataQueue, cameraDataContainer);

            var keyboard = new PressKeyboardWindows();
            var mouse = new MouseWindows();

            var movementController = new MovementController(keyboard);
            var mouseController = new MouseController(mouse);
            _dummyProbeControl = new DummyProbeController(_gyroDataQueue, _cameraDataQueue, movementController);
            _dummyNeedleController = new DummyNeedleController(_joystickDataQueue, mouseController);

            _f = new NulpunkjusteringMessage();

            _currentArduinoState = ArduinoState.Deactive;

            _sensorControl.CalibrateCompleted += CalibrateCompleted; // register with an event

            _dummyProbeControl._handleData = false;
            _probeThread = new Thread(_dummyProbeControl.Run);
           // _probeThread.IsBackground = true;

            _dummyNeedleController._handleData = false;
            _needleThread = new Thread(_dummyNeedleController.Run);
           // _needleThread.IsBackground = true;


            _probeThread.Start();
            _needleThread.Start();

        }
        public void CalibrateCompleted()
        {
            _currentArduinoState = ArduinoState.Deactive;
        }


        private void sensorB_Click(object sender, RoutedEventArgs e)
        {
            _sensorControl.sendCommand("i");
            SensorWindow sensorW = new SensorWindow(_joystickDataQueue,_gyroDataQueue,_cameraDataQueue, _sensorControl);
            sensorW.Show();
        }

        private void activateB_Click(object sender, RoutedEventArgs e)
        {
            if (_currentArduinoState == ArduinoState.Deactive)
            {
                _currentArduinoState = ArduinoState.Active;

                _sensorControl.sendCommand("a");
                activateB.Content = "Deactivate";

                _dummyNeedleController._handleData = true;
                _dummyProbeControl._handleData = true;

            }
            else if (_currentArduinoState == ArduinoState.Active)
            {
                _currentArduinoState = ArduinoState.Deactive;
                _sensorControl.sendCommand("i");
                activateB.Content = "Activate";

                _dummyNeedleController._handleData = false;
                _dummyProbeControl._handleData = false;
            }


                //Lock sensorbutton og activebutton
            }

        private void exitB_Click_1(object sender, RoutedEventArgs e)
        {
            _sensorControl.sendCommand("i");
            Application.Current.Shutdown();
        }

        private void nullpointB_Click(object sender, RoutedEventArgs e)
        {
            string message = "Læg gamingcontrollerne på en flad overflade i nulstilling position";
            string title = "Nulsillings vindu";
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK && _currentArduinoState== ArduinoState.Deactive)
            {
                _currentArduinoState = ArduinoState.Calibrating;

                _sensorControl.sendCommand("c");

                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
            else
            {
                // Do something  
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_currentArduinoState == ArduinoState.Calibrating || _currentArduinoState == ArduinoState.Active)
            {               
                _f.Show();
                tickCounter++;
            }
            else
            {
                _f.Close();
                dispatcherTimer.Stop();
            }

            if (tickCounter == 120)//to minutter
            {
                _f.Close();
                dispatcherTimer.Stop();
                tickCounter = 0;
            }
        }
    }
}
