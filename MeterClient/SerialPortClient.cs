using System.IO.Ports;

namespace MeterClient
{
	/// <summary>
	/// Implements transfer of byte arrays between client and meter.
    /// </summary>
	public class SerialPortClient
    {
        public string PortName { get; set; }
        public int Timeout { get; set; }
        public SerialPort? SerialPort { get; private set; }
        public SerialPortClient() : this("COM99") { }
        public SerialPortClient(string portName, int timeout = 10000)
        {
            PortName = portName;
            Timeout = timeout;
            SerialPort = new SerialPort
            {
                PortName = PortName,
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                ReadTimeout = Timeout,
                WriteTimeout = Timeout
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
            SerialPort!.Open();
            await SerialPort.BaseStream.WriteAsync(outputData, token);

		    byte[] inputData = new byte[inputDataLength];
		    int offset = 0;
            while (inputDataLength > 0)
            {
                int bytesRead = await SerialPort.BaseStream.ReadAsync(inputData.AsMemory(offset, inputDataLength), token);
                offset += bytesRead;
                inputDataLength -= bytesRead;
            }
            return inputData;
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
            using (SerialPort)
            {
                Task<byte[]> readFromSerialPortTask = MakeDataExchange(outputData, inputDataLength, token);
                Task timeoutTimer = Task.Delay(SerialPort!.ReadTimeout, token);
                var completedTask = await Task.WhenAny(readFromSerialPortTask, timeoutTimer);
                if (completedTask.IsCanceled)
                    throw new OperationCanceledException();
                if (completedTask == timeoutTimer)
                    throw new TimeoutException();
                return await readFromSerialPortTask;
            }
        }
    }
}