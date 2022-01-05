using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamecontrollerLibaray.Interface
{
    public interface IGyroCatagorize
    {
        List<string> CalculateMovementDirection(double x, double y, double z);
    }
}
