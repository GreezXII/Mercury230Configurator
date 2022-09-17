using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class CloseConnectionRequest : Request
    { 
        public CloseConnectionRequest(byte addr) : base(addr)
        {
            RequestType = RequestTypes.CloseConnection;
        }
    }
}