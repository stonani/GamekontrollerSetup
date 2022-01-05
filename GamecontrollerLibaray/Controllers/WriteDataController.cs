using GamecontrollerLibaray.DataObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamekontrollerSetup
{
    public class WriteDataController
    {
        private IWriteData _writeData;
        private readonly BlockingCollection<JoystickDataContainer> _joyDataQueue;
        private readonly BlockingCollection<GyroDataContainer> _gyroDataQueue;
        private readonly BlockingCollection<CameraDataContainer> _cameraDataQueue;

        public string gyrosampleX { get; set; }
        public string gyrosampleY { get; set; }
        public string gyrosampleZ { get; set; }

        public int _led1x { get; set; }
        public int _led1y { get; set; }
        public int _led2x { get; set; }
        public int _led2y { get; set; }

        public double joystickX { get; set; }
        public double joystickY { get; set; }
        public bool joystickButton { get; set; }

        public bool handleData { get; set; }


        public WriteDataController(IWriteData writeData, BlockingCollection<JoystickDataContainer> joyDataQueue, BlockingCollection<GyroDataContainer> gyroDataQueue, BlockingCollection<CameraDataContainer> cameraDataQueue)
        {
            _writeData = writeData;
            _joyDataQueue = joyDataQueue;
            _cameraDataQueue = cameraDataQueue;
            _gyroDataQueue = gyroDataQueue;
        }

        public void Run()
        {
            while (true)
            {
                if(handleData)
                {
                    Start();
                }                
            }
        }

        public void Start()
        {
            _writeData.ReportGyroData(HandleGyroSamples().Item1, HandleGyroSamples().Item2, HandleGyroSamples().Item3);
            _writeData.ReportCameraData(HandleCameraSamples().Item1, HandleCameraSamples().Item2, HandleCameraSamples().Item3, HandleCameraSamples().Item4);
            _writeData.ReportJoystickData(HandleJoystickSamples().Item1, HandleJoystickSamples().Item2, HandleJoystickSamples().Item3);
        }

        public Tuple<double, double, double> HandleGyroSamples()
        {
            while (!_gyroDataQueue.IsCompleted)
            {
                try
                {
                    var _DataContainer = _gyroDataQueue.Take();
                    var gyroX = _DataContainer.XValue;
                    var gyroY = _DataContainer.YValue;
                    var gyroZ = _DataContainer.ZValue;
                    return new Tuple<double, double, double>(gyroX, gyroY, gyroZ);
                }
                catch (InvalidOperationException)
                {
                    //Take() was called on a completed collection
                }
            }
            return null;
        }

        public Tuple<int, int, int, int> HandleCameraSamples()
        {
            while (!_cameraDataQueue.IsCompleted)
            {
                try
                {
                    var _DataContainer = _cameraDataQueue.Take();
                    var camx1 = _DataContainer.x1;
                    var camy1 = _DataContainer.y1;
                    var camx2 = _DataContainer.x2;
                    var camy2 = _DataContainer.y2;
                    return new Tuple<int, int, int, int>(camx1, camy1, camx2, camy2);
                }
                catch (InvalidOperationException)
                {
                    //Take() was called on a completed collection
                }
            }
            return null;
        }

        public Tuple<double, double, bool> HandleJoystickSamples()
        {
            while (!_joyDataQueue.IsCompleted)
            {
                try
                {
                    var _DataContainer = _joyDataQueue.Take();
                    var joyx = _DataContainer.XValue;
                    var joyy = _DataContainer.YValue;
                    var joyb = _DataContainer.button;
                    return new Tuple<double, double, bool>(joyx, joyy, joyb);
                }
                catch (InvalidOperationException)
                {
                    //Take() was called on a completed collection
                }
            }
            return null;
        }
    }
}
