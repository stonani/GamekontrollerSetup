using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GamekontrollerSetup
{
    public class FormsWrite : IWriteData
    {
        private SensorWindow _sensorwindow;

        public FormsWrite(SensorWindow sensorWindow)
        {
            _sensorwindow = sensorWindow;
        }

        public void ReportGyroData(string x, string y, string z)
        {
            _sensorwindow.Dispatcher.Invoke(() =>
            {
                _sensorwindow.GyroXT.Text = x;
                _sensorwindow.GyroYT.Text = y;

            });
        }
    }
}
