using System;
using System.IO.Ports;

namespace GamecontrollerLibaray.Interface
{
    public interface ISerialSensor
    {
        event EventHandler<ReceivedEventArgs> OnDataReceived;
        void WriteSerialPort(string command);
        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e);
    }
}
