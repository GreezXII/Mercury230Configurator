using M230Protocol.Frames.Base;
using static M230Protocol.Frames.Requests.ReadStoredEnergyRequest;

namespace M230Protocol.Frames.Responses
{
    class ReadStoredEnergyResponsePerPhase : Response
    {
        public const int Length = 15;
        public double Phase1 { get; private set; }
        public double Phase2 { get; private set; }
        public double Phase3 { get; private set; }
        public Rates Rate { get; private set; }
        public ReadStoredEnergyResponsePerPhase(byte[] response, Rates r) : base(response)
        {
            Rate = r;
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
