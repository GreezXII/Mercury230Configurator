using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    /// <summary>
    /// Command to read stored energy.
    /// </summary>
    public class ReadStoredEnergyRequest : Request
    {
		/// <summary>
		/// Specifies to return energy value for specific time span or per phases.
		/// </summary>
		public EnergyArrays EnergyDataType { get; private set; }
        /// <summary>
        /// Specifies month for time span of energy value.  
        /// </summary>
        /// <value>Number of month if month time span required or 0 if not.</value>
        public Months Month { get; private set; }
		/// <summary>
		/// Specifies whether to return the accumulated energy for the amount of tariffs or for a specific tariff.
		/// </summary>
		public MeterRates Rate { get; private set; }

        public ReadStoredEnergyRequest(byte addr, EnergyArrays energyDataType, Months? month, MeterRates rate) : base(addr)
        {
            RequestType = RequestTypes.ReadArray;
            EnergyDataType = energyDataType;
            Month = month == null ? 0x0 : month.Value;
            Rate = rate;
        }
        /// <summary>
        /// Combine month value and energy type value in one byte.
        /// </summary>
        /// <returns>Byte that holds values for month and energy type.</returns>
        private byte CombineMonthAndEnergyDataArray()
        {

            byte result = (byte)((byte)EnergyDataType << 4);
            return (byte)(result | (byte)Month);
        }
        /// <inheritdoc cref="Request.Create"/>
        public override byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, CombineMonthAndEnergyDataArray(), (byte)Rate };
            return CreateByteArray(requestBody);
        }
    }
}
