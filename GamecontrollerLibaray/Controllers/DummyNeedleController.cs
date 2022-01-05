using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamecontrollerLibaray.DataObjects;
using System.Collections.Concurrent;

namespace GamecontrollerLibaray.Controllers
{
    public class DummyNeedleController
    {
        private readonly BlockingCollection<JoystickDataContainer> _joystickDataQueue;


        private MouseController _mouseController;

        private double joystickX { get; set; }
        private double joystickY { get; set; }
        private bool joystickButton { get; set; }
        public bool _handleData { get; set; }


        public DummyNeedleController(BlockingCollection<JoystickDataContainer> joystickDataQueue, MouseController mouseController)
        {
            _joystickDataQueue = joystickDataQueue;
            _mouseController = mouseController;
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
            joystickX = HandleJoystickSamples().Item1;
            joystickY = HandleJoystickSamples().Item2;
            joystickButton = HandleJoystickSamples().Item3;

            _mouseController.MoveMouse(Convert.ToInt32(joystickX), Convert.ToInt32(joystickY));
            _mouseController.LeftClickMouse(joystickButton);
        }

        public Tuple<double, double, bool> HandleJoystickSamples()
        {
            while (!_joystickDataQueue.IsCompleted)
            {
                try
                {
                    var _DataContainer = _joystickDataQueue.Take();
                    var x = _DataContainer.XValue;
                    var y = _DataContainer.YValue;
                    var button = _DataContainer.button;
                    return new Tuple<double, double, bool>(x, y, button);
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
