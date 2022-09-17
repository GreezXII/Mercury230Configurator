using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class TestLinkRequest : Request
    {
        public TestLinkRequest(byte addr) : base(addr)
        {
            RequestType = RequestTypes.TestConnection;
        }
    }
}