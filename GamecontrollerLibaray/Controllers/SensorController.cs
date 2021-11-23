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
        private ISensor _sensor;
        private readonly BlockingCollection<GyroDataContainer> _dataQueue;
        private GyroDataContainer _gyroDataContainer;
        sbyte indexOfX, indexOfY;
        double gyroDataX, gyroDataY;
        string gyroDataIn;

        public SensorController(ISensor sensor, BlockingCollection<GyroDataContainer> dataQueue, GyroDataContainer gyroDataContainer)
        {
            _sensor = sensor;
            _dataQueue = dataQueue;
            _gyroDataContainer = gyroDataContainer;

            _sensor.OnDataReceived += HandleSensorData;
        }

        private void HandleSensorData(object sender, ReceivedEventArgs e)
        {
            if(_sensor.GetType() == typeof(DummyMPUSensor))
            {
                gyroDataIn = e.Data;
                DummySplitDataMPU();
            }
        }

        private void DummySplitDataMPU()
        {
            indexOfX = Convert.ToSByte(gyroDataIn.IndexOf("x"));
            indexOfY = Convert.ToSByte(gyroDataIn.IndexOf("y"));

            gyroDataX = double.Parse(gyroDataIn.Substring(0, indexOfX), CultureInfo.InvariantCulture);
            gyroDataY = double.Parse(gyroDataIn.Substring(indexOfX + 1, (indexOfY - indexOfX) - 1), CultureInfo.InvariantCulture);

            _gyroDataContainer.XValue = gyroDataX;
            _gyroDataContainer.YValue = gyroDataY;

            _dataQueue.Add(_gyroDataContainer);
        }
    }
}
