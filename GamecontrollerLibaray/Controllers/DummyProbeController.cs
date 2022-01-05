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
    public class DummyProbeController
    {
        private readonly BlockingCollection<GyroDataContainer> _gyroDataQueue;
        private readonly BlockingCollection<CameraDataContainer> _cameraDataQueue;

        private GyroCatagorizer _gyroCatagoriser;
        private CameraCatagorizer _cameraCatagoriser;

        private MovementController _movementController;

        private bool _alligned;
        private int _led1x { get; set; }
        private int _led1y { get; set; }
        private int _led2x { get; set; }
        private int _led2y { get; set; }


        private double gyrosampleX { get; set; }
        private double gyrosampleY { get; set; }
        private double gyrosampleZ { get; set; }

        public bool _handleData { get; set; }
        public DummyProbeController(BlockingCollection<GyroDataContainer> gyroDataQueue, BlockingCollection<CameraDataContainer> cameraDataQueue, MovementController movementController)
        {
            _gyroDataQueue = gyroDataQueue;
            _cameraDataQueue = cameraDataQueue;
            _gyroCatagoriser = new GyroCatagorizer();
            _cameraCatagoriser = new CameraCatagorizer();
            _movementController = movementController;
            _alligned = true;
        }

        public void Run()
        {
            while (true)
            {
                if(_handleData)
                {
                    Start();
                }                
            }
        }

        public void Start()
        {
            gyrosampleX = HandleGyroSamples().Item1;
            gyrosampleY = HandleGyroSamples().Item2;
            gyrosampleZ = HandleGyroSamples().Item3;

            _led1x = HandleCameraSamples().Item1;
            _led1y = HandleCameraSamples().Item2;
            _led2x = HandleCameraSamples().Item3;
            _led2y = HandleCameraSamples().Item4;

            _alligned=_cameraCatagoriser.isAlligmnt(_led1x, _led1y, _led2x, _led2y);
            if(_alligned)
            {
                var gyroDirection = _gyroCatagoriser.CalculateMovementDirection(gyrosampleX, gyrosampleY, gyrosampleZ);
                _movementController.HandleMovement(gyroDirection);
            }            
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
                    var x1 = _DataContainer.x1;
                    var y1 = _DataContainer.y1;
                    var x2 = _DataContainer.x2;
                    var y2 = _DataContainer.y2;
                    return new Tuple<int, int, int, int>(x1, y1, x2, y2);
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
