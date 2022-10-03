using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    public class ReadStoredEnergyRequest : Request
    {
        public EnergyArrays EnergyDataType { get; private set; }
        public Months Month { get; private set; }
        public MeterRates Rate { get; private set; }
        public ReadStoredEnergyRequest(byte addr, EnergyArrays energyDataType, Months month, MeterRates rate) : base(addr)
        {
            RequestType = RequestTypes.ReadArray;
            EnergyDataType = energyDataType;
            Month = month;
            Rate = rate;
        }
        private byte CombineMonthAndEnergyDataArray(EnergyArrays energyDataType, Months month)
        {

            byte result = (byte)((byte)energyDataType << 4);
            return (byte)(result | (byte)month);
        }

        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, CombineMonthAndEnergyDataArray(EnergyDataType, Month), (byte)Rate };
            return CreateByteArray(requestBody);
        }
    }
}
