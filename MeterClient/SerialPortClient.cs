using System.IO.Ports;
using M230Protocol.Frames.Base;

namespace MeterClient
{
    public class SerialPortClient
    {
        // TODO: figure out settings
        // TODO: refactor constructor
        // TODO: creational patterns
        public SerialPort SerialPort { get; private set; }
        public SerialPortClient(string portName, 
                                int baudrate = 9600, 
                                Parity parity = Parity.None, 
                                int dataBits = 8, 
                                StopBits portStopBits = StopBits.One, 
                                int writeTimeout = 5000, 
                                int readTimeout = 5000)
        {
            SerialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = baudrate,
                Parity = parity,
                DataBits = dataBits,
                StopBits = portStopBits,
                WriteTimeout = writeTimeout,
                ReadTimeout = readTimeout
            };
        }

        public async Task<byte[]> PerformDataExchange(byte[] buffer, int count, CancellationToken token = default)
        {
            // TODO: Timeout
            byte[] readBuffer = new byte[count];
            int bytesRead;
            int offset = 0;
            using (SerialPort)
            {
                SerialPort.Open();
                await SerialPort.BaseStream.WriteAsync(buffer, token);

                while (count > 0)
                {
                    bytesRead = await SerialPort.BaseStream.ReadAsync(readBuffer, offset, count, token);
                    offset += bytesRead;
                    count -= bytesRead;
                }
            }
            return readBuffer;
        }
    }
}