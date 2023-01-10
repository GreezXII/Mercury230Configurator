using M230Protocol.Frames.Base;
using static M230Protocol.Frames.Requests.ReadStoredEnergyRequest;

namespace M230Protocol.Frames.Responses
{
    public class ReadStoredEnergyResponse : Response
    {
        public const int Length = 19;
        /// <summary>
        /// Positive active energy.
        /// </summary>
        public double ActivePositive { get; private set; }
        /// <summary>
        /// Negative active energy.
        /// </summary>
        public double ActiveNegative { get; private set; }
        /// <summary>
        /// Positive reactive energy.
        /// </summary>
        public double ReactivePositive { get; private set; }
        /// <summary>
        /// Negative reactive energy.
        /// </summary>
        public double ReactiveNegative { get; private set; }

        public ReadStoredEnergyResponse(byte[] response) : base(response)
        {
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
