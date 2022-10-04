namespace M230Protocol
{
    public enum CommunicationState : byte
    {
        OK,
        WrongCommandOrParameter,
        InternalMeterError,
        AccessDenied,
        LinkClosed
    }
}
