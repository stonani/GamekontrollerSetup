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
        private readonly BlockingCollection<GyroDataContainer> _dataQueue;

        public string gyrosampleX { get; set; }
        public string gyrosampleY { get; set; }
        private string gyrosampleZ { get; set; }

        public WriteDataController(IWriteData writeData, BlockingCollection<GyroDataContainer> dataQueue)
        {
            _writeData = writeData;
            _dataQueue = dataQueue;
        }

        public void Run()
        {
            while (true)
            {
                Start();
            }
        }

        public void Start()
        {
            gyrosampleX = Convert.ToString(HandleGyroSamples().Item1);
            gyrosampleY = Convert.ToString(HandleGyroSamples().Item2);
            gyrosampleZ = Convert.ToString(HandleGyroSamples().Item3);

            _writeData.ReportGyroData(gyrosampleX, gyrosampleY, gyrosampleZ);
        }

        public Tuple<double, double, double> HandleGyroSamples()
        {
            while (!_dataQueue.IsCompleted)
            {
                try
                {
                    var _DataContainer = _dataQueue.Take();
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



    }
}
