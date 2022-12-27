using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    public class ReadStoredEnergyPerPhaseResponse : Response
    {
        public const int Length = 15;
        public double Phase1 { get; private set; }
        public double Phase2 { get; private set; }
        public double Phase3 { get; private set; }
        public ReadStoredEnergyPerPhaseResponse(byte[] response) : base(response)
        {
            byte[] buffer = new byte[4];
            Array.Copy(response, 1, buffer, 0, 4);
            Phase1 = GetEnergyValue(buffer);
            Array.Copy(response, 5, buffer, 0, 4);
            Phase2 = GetEnergyValue(buffer);
            Array.Copy(response, 9, buffer, 0, 4);
            Phase3 = GetEnergyValue(buffer);
        }
        
    }
}
