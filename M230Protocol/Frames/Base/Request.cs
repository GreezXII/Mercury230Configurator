using M230Protocol.Exceptions;

namespace M230Protocol.Frames.Base
{
	/// <summary>
	/// Base class for all requests. Request in this context is a command for the meter.
	/// </summary>
	public abstract class Request : Frame
    {
        /// <summary>
        /// Specifies command for the meter.
        /// </summary>
        public RequestTypes RequestType { get; protected set; }
        public Request(byte addr) : base(addr) { }
        /// <summary>
        /// Adds address and CRC to specific command.
        /// </summary>
        /// <param name="request">Specific command for the meter.</param>
        /// <returns>Array of bytes with a command that ready to be transfered to the meter.</returns>
        protected byte[] CreateByteArray(List<byte> request)
        {
            request.Insert(0, Address);
			byte[] CRC = CalculateCRC16Modbus(request.ToArray());
			request.AddRange(CRC);
			return request.ToArray();
		}
        /// <summary>
        /// Create command in form of byte array for transmitting to the meter.
        /// </summary>
        /// <returns>Byte array that describes command for the meter.</returns>
        public abstract byte[] Create();
    }
}
