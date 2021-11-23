using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamecontrollerLibaray.Calculators
{
    public class GyroCalculator
    {
        private List<string> gyroDirection = new List<string>(); 
        public List<string> CalculateMovementDirection(double x, double y, double z)
        {
            gyroDirection.Clear();

            if(x<-10)
            {
                gyroDirection.Add("up");
            }
            else if(x>10)
            {
                gyroDirection.Add("down");
            }
            if(y<-10)
            {
                gyroDirection.Add("left");
            }
            else if(y>10)
            {
                gyroDirection.Add("right");
            }

            return gyroDirection;
        }
    }
}
