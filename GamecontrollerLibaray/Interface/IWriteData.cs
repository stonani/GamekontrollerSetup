using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamekontrollerSetup
{
    public interface IWriteData
    {
        void ReportGyroData(double x, double y, double z);
        void ReportJoystickData(double x, double y, bool b);

        void ReportCameraData(int x1, int y1, int x2, int y2);
    }
}
