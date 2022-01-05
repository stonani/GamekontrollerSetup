using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GamecontrollerLibaray.Interface;

namespace GamekontrollerSetup
{
    public class FormsWrite : IWriteData
    {
        private SensorWindow _sensorwindow;

        public FormsWrite(SensorWindow sensorWindow)
        {
            _sensorwindow = sensorWindow;
        }

        public void ReportCameraData(int x1, int y1, int x2, int y2)
        {
            _sensorwindow.Dispatcher.Invoke(() =>
            {
                _sensorwindow.CamX1.Text = Convert.ToString(x1);
                _sensorwindow.Camy1.Text = Convert.ToString(y1);
                _sensorwindow.Camx2.Text = Convert.ToString(x2);
                _sensorwindow.Camy2.Text = Convert.ToString(y2);
            });
        }

        public void ReportGyroData(double x, double y, double z)
        {
            _sensorwindow.Dispatcher.Invoke(() =>
            {
                _sensorwindow.GyroXT.Text = Convert.ToString(x);
                _sensorwindow.GyroYT.Text = Convert.ToString(y);
                _sensorwindow.GyroZT.Text = Convert.ToString(z);

            });
        }

        public void ReportJoystickData(double x, double y, bool b)
        {
            _sensorwindow.Dispatcher.Invoke(() =>
            {
                _sensorwindow.JoyStickXT.Text = Convert.ToString(x);
                _sensorwindow.JoyStickYT.Text = Convert.ToString(y);
                _sensorwindow.JoyStickBT.Text = Convert.ToString(b);

            });
        }

    }
}
