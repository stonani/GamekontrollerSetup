using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamecontrollerLibaray.Interface;

namespace GamecontrollerLibaray.Calculators
{
    public class GyroCatagorizer:IGyroCatagorize
    {
        private List<string> _gyroDirection = new List<string>();
        public List<string> CalculateMovementDirection(double x, double y, double z) //Var tidligere n liste
        {
            _gyroDirection.Clear();

            if (z<90)
            {
                _gyroDirection.Add("down");
            }
            else if(z>90)
            {
                _gyroDirection.Add("up");
            }
            if(y<-75)
            {
                _gyroDirection.Add("left");
            }
            else if(y>-45)
            {
                _gyroDirection.Add("right"); ;
            }

            return _gyroDirection;
        }
    }
}
