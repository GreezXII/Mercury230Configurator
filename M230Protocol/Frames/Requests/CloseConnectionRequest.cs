using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    /// <summary>
    /// Command to close connection with the meter.
    /// </summary>
    public class CloseConnectionRequest : Request
    {
        public CloseConnectionRequest(byte addr) : base(addr)
        {
            RequestType = RequestTypes.CloseConnection;
        }
        /// <summary>
        /// Create command for transmitting to the meter.
        /// </summary>
        /// <returns>Array with command for the meter.</returns>
        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType };
            return CreateByteArray(requestBody);
        }
    }
}