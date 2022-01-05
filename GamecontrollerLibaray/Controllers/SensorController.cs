using GamecontrollerLibaray.Interface;
using GamecontrollerLibaray.DataObjects;
using System.Collections.Concurrent;
using System;
using System.Globalization;
using System.Threading;

namespace GamecontrollerLibaray.Controllers
{
    public class SensorController
    {
        private ISerialSensor _serialSensor;
        private ICameraSensor _cameraSensor;

        private readonly BlockingCollection<GyroDataContainer> _gyroDataQueue;
        private readonly BlockingCollection<JoystickDataContainer> _joystickdataQueue;
        private readonly BlockingCollection<CameraDataContainer> _cameraDataQueue;

        private GyroDataContainer _gyroDataContainer;
        private JoystickDataContainer _joystickDataContainer;
        private CameraDataContainer _cameraDataContainer;


        //sbyte indexOfX, indexOfY;
        //double gyroDataX, gyroDataY;
        public delegate void Notify();  // delegate
        string SerialDataIn; 

        public SensorController(ISerialSensor serialSensor, ICameraSensor cameraSensor, BlockingCollection<GyroDataContainer> gyrodataQueue, GyroDataContainer gyroDataContainer, BlockingCollection<JoystickDataContainer> joystickdataQueue, JoystickDataContainer joystickDataContainer, BlockingCollection<CameraDataContainer> cameraDataQueue, CameraDataContainer cameraDataContainer)
        {
            _serialSensor = serialSensor;
            _cameraSensor = cameraSensor;

            _gyroDataQueue = gyrodataQueue;
            _joystickdataQueue = joystickdataQueue;
            _cameraDataQueue = cameraDataQueue;

            _gyroDataContainer = gyroDataContainer;
            _joystickDataContainer = joystickDataContainer;
            _cameraDataContainer = cameraDataContainer;

            _serialSensor.OnDataReceived += HandleSensorData;
        }
        public event Notify CalibrateCompleted; // event

        public void sendCommand(string command)
        {
            _serialSensor.WriteSerialPort(command);
        }

        private void HandleSensorData(object sender, ReceivedEventArgs e)
        {
            SerialDataIn = e.Data;
            if(SerialDataIn.Contains("OK"))
            {
                CalibrateCompleted?.Invoke();
            }
            else
            {
                DummySplitSerialData();
            }

            _cameraDataContainer.x1 = _cameraSensor.getCoordinates()[0];
            _cameraDataContainer.y1 = _cameraSensor.getCoordinates()[1];
            _cameraDataContainer.x2 = _cameraSensor.getCoordinates()[2];
            _cameraDataContainer.y2 = _cameraSensor.getCoordinates()[3];
            _cameraDataQueue.Add(_cameraDataContainer);
        }

        private void DummySplitSerialData()
        {
            //indexOfX = Convert.ToSByte(gyroDataIn.IndexOf("x"));
            //indexOfY = Convert.ToSByte(gyroDataIn.IndexOf("y"));
             string[] data = SerialDataIn.Split(',');

            try
            {            
                _joystickDataContainer.XValue = double.Parse(data[0], CultureInfo.InvariantCulture);
                _joystickDataContainer.YValue = double.Parse(data[1], CultureInfo.InvariantCulture);
                if(double.Parse(data[2], CultureInfo.InvariantCulture) == 1)
                {
                    _joystickDataContainer.button = false;
                }
                else
                {
                    _joystickDataContainer.button = true;
                }
                _joystickdataQueue.Add(_joystickDataContainer);

                _gyroDataContainer.XValue = double.Parse(data[3], CultureInfo.InvariantCulture);
                _gyroDataContainer.YValue = double.Parse(data[4], CultureInfo.InvariantCulture);
                _gyroDataContainer.ZValue = double.Parse(data[5], CultureInfo.InvariantCulture);

                _gyroDataQueue.Add(_gyroDataContainer);

                //gyroDataX = double.Parse(gyroDataIn.Substring(0, indexOfX), CultureInfo.InvariantCulture);
                //gyroDataY = double.Parse(gyroDataIn.Substring(indexOfX + 1, (indexOfY - indexOfX) - 1), CultureInfo.InvariantCulture);

                //_gyroDataContainer.XValue = gyroDataX;
                //_gyroDataContainer.YValue = gyroDataY;
            }
            catch (Exception)
            {
                //Exception
            }

        }
    }
}
