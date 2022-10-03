using M230Protocol.Frames.Base;
using static M230Protocol.Frames.Requests.ReadStoredEnergyRequest;

namespace M230Protocol.Frames.Responses
{
    class ReadStoredEnergyResponse : Response
    {
        public const int Length = 19;
        public double ActivePositive { get; private set; }
        public double ActiveNegative { get; private set; }
        public double ReactivePositive { get; private set; }
        public double ReactiveNegative { get; private set; }
        public MeterRates Rate { get; private set; }
        public ReadStoredEnergyResponse(byte[] response, MeterRates r) : base(response)
        {
            Rate = r;
            byte[] buffer = new byte[4];
            Array.Copy(response, 1, buffer, 0, 4);
            ActivePositive = GetEnergyValue(buffer);
            Array.Copy(response, 5, buffer, 0, 4);
            ActiveNegative = GetEnergyValue(buffer);
            Array.Copy(response, 9, buffer, 0, 4);
            ReactivePositive = GetEnergyValue(buffer);
            Array.Copy(response, 13, buffer, 0, 4);
            ReactiveNegative = GetEnergyValue(buffer);
        }
    }
}
