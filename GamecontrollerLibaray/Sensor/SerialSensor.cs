using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamecontrollerLibaray.Interface;
using System.IO.Ports;

namespace GamecontrollerLibaray.Sensor
{
    public class SerialSensor : ISerialSensor
    {
        public event EventHandler<ReceivedEventArgs> OnDataReceived;
        private SerialPort _port;

        public SerialSensor(string portname, int baudrate, int readtimeout)
        {
            _port = new SerialPort();
            _port.PortName = portname;
            _port.BaudRate = baudrate;
            _port.ReadTimeout = readtimeout;
            _port.DtrEnable = true;    // Data-terminal-ready
            _port.RtsEnable = true;    // Request-to-send

            _port.Open();
            _port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        //public void CloseSerialPort()
        //{
        //    _port.Close();
        //}

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string SerialDataIn = sp.ReadLine();

            ReceivedEventArgs rea = new ReceivedEventArgs();
            rea.Data = SerialDataIn;
            OnDataReceived(this, rea);
        }

        public void WriteSerialPort(string command)
        {
            _port.Write(command);
        }
    }
}
