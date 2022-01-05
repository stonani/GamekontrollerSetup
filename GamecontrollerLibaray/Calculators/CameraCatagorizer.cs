using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamecontrollerLibaray.Interface;

namespace GamecontrollerLibaray.Calculators
{
    public class CameraCatagorizer: ICameraCatagorize
    {
        public bool isAlligmnt(int x1, int y1, int x2, int y2)
        {
            if( x1 > 155 && x1<165 && x2 > 155 && x2 < 165)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
