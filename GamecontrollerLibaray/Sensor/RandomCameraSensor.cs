using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamecontrollerLibaray.Interface;

namespace GamecontrollerLibaray.Sensor
{
    public class RandomCameraSensor : ICameraSensor
    {
        private List<int> _randomCoordinates;

        public RandomCameraSensor()
        {
            _randomCoordinates = new List<int>();
        }

        public List<int> getCoordinates()
        {
            _randomCoordinates.Clear();

            Random r = new Random();
            int rInt = r.Next(155, 165); //for ints

            for (int i = 0; i < 4; i++)
            {
                _randomCoordinates.Add(rInt);
            }

            return _randomCoordinates;
        }
    }
}
