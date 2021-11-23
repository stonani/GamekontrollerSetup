using GamecontrollerLibaray.Calculators;
using GamecontrollerLibaray.DataObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamecontrollerLibaray.Controllers
{
    public class GyroController
    {
        private readonly BlockingCollection<GyroDataContainer> _dataQueue;
        private GyroCalculator _gyroCalculator;
        private MovementController _movementController;
        private bool alligned = true;
        public double gyrosampleX { get; set; }
        public double gyrosampleY { get; set; }
        private double gyrosampleZ { get; set; }
        public GyroController(BlockingCollection<GyroDataContainer> dataQueue, MovementController movementController)
        {
            _dataQueue = dataQueue;
            _gyroCalculator = new GyroCalculator();
            _movementController = movementController;
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
            gyrosampleX = HandleGyroSamples().Item1;
            gyrosampleY = HandleGyroSamples().Item2;
            gyrosampleZ = HandleGyroSamples().Item3;

            var gyroDirection =  _gyroCalculator.CalculateMovementDirection(gyrosampleX, gyrosampleY, gyrosampleZ);
            _movementController.HandleMovement(gyroDirection);
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
