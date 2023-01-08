using System.IO.Ports;
using M230Protocol.Frames.Base;
using System;

namespace MeterClient
{
    public class SerialPortClient
    {
        public SerialPort SerialPort { get; private set; }
        public SerialPortClient(string portName, 
                                int baudrate = 9600, 
                                Parity parity = Parity.None, 
                                int dataBits = 8, 
                                StopBits portStopBits = StopBits.One, 
                                int readTimeout = 5000)
        {
            SerialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = baudrate,
                Parity = parity,
                DataBits = dataBits,
                StopBits = portStopBits,
                ReadTimeout = readTimeout
            };
        }

        private async Task<byte[]> ReadFromSerialPortAsync(byte[] buffer, int count, CancellationToken token = default)
        {
            // TODO: Timeout
            byte[] inputBuffer = new byte[count];
            int offset = 0;
            using (SerialPort)
            {
                SerialPort.Open();
                await SerialPort.BaseStream.WriteAsync(buffer, token);

                while (count > 0)
                {
                    if (token.IsCancellationRequested)
                        throw new OperationCanceledException();
                    bytesRead = await SerialPort.BaseStream.ReadAsync(readBuffer.AsMemory(offset, count), token);
                    offset += bytesRead;
                    count -= bytesRead;
                }
            }
            return readBuffer;
        }
		public async Task<byte[]> GetResponseAsync(byte[] buffer, int count, CancellationToken token = default)
        {
            Task<byte[]> readFromSerialPortTask = ReadFromSerialPortAsync(buffer, count, token);
            var completedTask = await Task.WhenAny(readFromSerialPortTask, Task.Delay(SerialPort.ReadTimeout, token));
            if (completedTask == readFromSerialPortTask)
                return await readFromSerialPortTask;
            else
                throw new TimeoutException();
		}
	}
}