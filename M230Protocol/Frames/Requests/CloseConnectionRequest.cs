using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    public class CloseConnectionRequest : Request
    {
        public CloseConnectionRequest(byte addr) : base(addr)
        {
            RequestType = RequestTypes.CloseConnection;
        }
        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType };
            return CreateByteArray(requestBody);
        }
    }
}