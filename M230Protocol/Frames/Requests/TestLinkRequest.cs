using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class TestLinkRequest : Request
    {
        public TestLinkRequest(byte addr)
            : base(addr)
        {
            RequestType = RequestTypes.TestConnection;
        }

        public override byte[] Create()
        {
            byte[] body = new byte[] { Address, (byte)RequestType };
            Length = body.Length;
            return AddCRC(body);
        }
    }
}