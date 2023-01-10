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
		/// Create binary coded decimal as byte array from string.
		/// </summary>
		/// <param name="s">String that should be converter to binary coded decimal.</param>
		/// <returns>Byte array where each byte is character from input string.</returns>
		/// <exception cref="CouldNotCreateBCD">.</exception>
		internal static byte[] StringToBCD(string s)
        {
            byte[] bytePassword = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                byte b;
                bool result = byte.TryParse(s[i].ToString(), out b);
                if (!result)
                    throw new CouldNotCreateBCD();
                bytePassword[i] = b;
            }
            return bytePassword;
        }
        /// <summary>
        /// Create command in form of byte array for transmitting to the meter.
        /// </summary>
        /// <returns>Byte array that describes command for the meter.</returns>
        public abstract byte[] Create();
    }
}
