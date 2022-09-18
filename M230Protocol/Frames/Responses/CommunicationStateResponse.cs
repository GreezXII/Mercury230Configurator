using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    internal class CommunicationStateResponse : Response
    {
        public enum CommunicationState : byte
        {
            OK,
            WrongCommandOrParameter,
            InternalMeterError,
            AccessDenied,
            LinkClosed
        }

        public CommunicationState State { get; private set; }
        public CommunicationStateResponse(byte[] response) : base(response)
        {
            Length = 2;
            State = (CommunicationState)response[1];
        }
    }
}
