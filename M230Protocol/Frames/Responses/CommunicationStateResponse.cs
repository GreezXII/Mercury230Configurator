using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    public class CommunicationStateResponse : Response
    {
        public const int Length = 4;
        public CommunicationState State { get; private set; }
        public CommunicationStateResponse(byte[] response) : base(response)
        {
            State = (CommunicationState)response[1];
        }
    }
}
