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
        
        /// <inheritdoc cref="Request.Create"/>
        public override byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType };
            return CreateByteArray(requestBody);
        }
    }
}