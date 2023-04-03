using System.IO.Ports;

namespace MeterClient
{
	/// <summary>
	/// Implements transfer of byte arrays between client and meter.
    /// </summary>
	public class SerialPortClient
    {
        public SerialPort SerialPort { get; private set; }
        public SerialPortClient() => SerialPort = new SerialPort();
        public SerialPortClient(string portName, int timeout = 200000)
        {
            SerialPort = new SerialPort
            {
                PortName = portName,
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                ReadTimeout = timeout,
                WriteTimeout = timeout
			};
        }
		/// <summary>
		/// Send a byte array to the meter and get a response byte array.
		/// </summary>
		/// <param name="outputData">Data that should be sent to the meter. To get the right byte array you should use Create() method for child type of <seealso cref="Request"/> type.</param>
		/// <param name="inputDataLength">The length of expected response byte array.</param>
		/// <param name="token">Propagates notification that operations should be canceled.</param>
		/// <returns>A Task that returns a response byte array.</returns>
		/// <exception cref="OperationCanceledException"></exception>
		private async Task<byte[]> MakeDataExchange(byte[] outputData, int inputDataLength, CancellationToken token = default)
        {
            using (SerialPort)
            {
                SerialPort.Open();
                await SerialPort.BaseStream.WriteAsync(outputData, token);

				byte[] inputData = new byte[inputDataLength];
				int offset = 0;
				while (inputDataLength > 0)
                {
                    if (token.IsCancellationRequested)
                        throw new OperationCanceledException();
                    int bytesRead = await SerialPort.BaseStream.ReadAsync(inputData.AsMemory(offset, inputDataLength), token);
                    offset += bytesRead;
                    inputDataLength -= bytesRead;
                }
                return inputData;
            }
        }
		/// <summary>
		/// Send a byte array to the meter and get a response byte array. This method extends <seealso cref="MakeDataExchange(byte[], int, CancellationToken)"/>
		/// with timeout Task as <seealso cref="SerialPort.ReadTimeout"/> and <seealso cref="SerialPort.WriteTimeout"/> could work incorrectly with virtual serial ports.
		/// </summary>
		/// <param name="outputData">Data that should be sent to the meter. To get the right byte array you should use Create() method for child type of <seealso cref="Request"/> type.</param>
		/// <param name="inputDataLength">The length of expected response byte array.</param>
		/// <param name="token">Propagates notification that operations should be canceled.</param>
		/// <returns>A Task that returns a response byte array.</returns>
		/// <exception cref="TimeoutException"></exception>
		public async Task<byte[]> GetResponseAsync(byte[] outputData, int inputDataLength, CancellationToken token = default)
        {
            Task<byte[]> readFromSerialPortTask = MakeDataExchange(outputData, inputDataLength, token);
            var completedTask = await Task.WhenAny(readFromSerialPortTask, Task.Delay(SerialPort.ReadTimeout, token));
            if (completedTask == readFromSerialPortTask)
                return await readFromSerialPortTask;
            else
                throw new TimeoutException();
		}
	}
}