using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamekontrollerSetup
{
    public interface IWriteData
    {
        void ReportGyroData(string x, string y, string z);
    }
}
