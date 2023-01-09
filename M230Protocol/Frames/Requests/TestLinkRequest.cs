using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    /// <summary>
    /// Command to test physical availability of the meter.
    /// </summary>
    public class TestLinkRequest : Request
    {
        public TestLinkRequest(byte addr) : base(addr)
        {
            RequestType = RequestTypes.TestConnection;
        }
        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType };
            return CreateByteArray(requestBody);
        }
    }
}