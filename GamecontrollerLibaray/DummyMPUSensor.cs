using GamecontrollerLibaray.Interface;
using System;
using System.IO.Ports;

namespace GamecontrollerLibaray
{
    public class DummyMPUSensor : ISensor
    {
        public event EventHandler<ReceivedEventArgs> OnDataReceived;

        public DummyMPUSensor()
        {
            SerialPort port = new SerialPort();
            port.PortName = "COM7";
            port.BaudRate = 9600;
            port.ReadTimeout = 5000;
            port.DtrEnable = true;    // Data-terminal-ready
            port.RtsEnable = true;    // Request-to-send
            port.Open();

            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }
        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string gyroDataIn = sp.ReadLine();

            ReceivedEventArgs rea = new ReceivedEventArgs();
            rea.Data = gyroDataIn;
            OnDataReceived(this, rea);
        }

        public string GetData()
        {
            throw new System.NotImplementedException();
        }
    }
}
