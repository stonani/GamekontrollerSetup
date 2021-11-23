using System;
using System.IO.Ports;


namespace GamecontrollerLibaray.Interface
{
    public interface ISensor
    {
        event EventHandler<ReceivedEventArgs> OnDataReceived;
        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e);
        string GetData();
    }
}
