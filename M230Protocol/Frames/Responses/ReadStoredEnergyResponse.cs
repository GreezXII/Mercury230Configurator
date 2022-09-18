using M230Protocol.Frames.Base;
using static M230Protocol.Frames.Requests.ReadStoredEnergyRequest;

namespace M230Protocol.Frames.Responses
{
    class ReadStoredEnergyResponse : Response
    {
        public double ActivePositive { get; private set; }
        public double ActiveNegative { get; private set; }
        public double ReactivePositive { get; private set; }
        public double ReactiveNegative { get; private set; }
        public double Phase1 { get; private set; }
        public double Phase2 { get; private set; }
        public double Phase3 { get; private set; }
        public bool IsPerPhase { get; private set; }
        public Rates Rate { get; private set; }
        public ReadStoredEnergyResponse(byte[] response, Rates r) : base(response)
        {
            Rate = r;
            if (response.Length == 19)  // TODO: Create two separate classes for energy per Phase and for energy not per Phase
            {
                IsPerPhase = false;
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
            if (response.Length == 15)
            {
                IsPerPhase = true;
                byte[] buffer = new byte[4];
                Array.Copy(response, 1, buffer, 0, 4);
                Phase1 = GetEnergyValue(buffer);
                Array.Copy(response, 5, buffer, 0, 4);
                Phase2 = GetEnergyValue(buffer);
                Array.Copy(response, 9, buffer, 0, 4);
                Phase3 = GetEnergyValue(buffer);
            }
        }
        private double GetEnergyValue(byte[] array)
        {
            // Изменить порядок байт согласно документации
            byte[] buffer = new byte[array.Length];
            buffer[0] = array[1];
            buffer[1] = array[0];
            buffer[2] = array[3];
            buffer[3] = array[2];

            return FullHexToInt(buffer) / 1000.0d;
        }
    }
}
