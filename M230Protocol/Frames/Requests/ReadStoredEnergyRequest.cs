using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class ReadStoredEnergyRequest : Request
    {
        public enum EnergyDataTypes : byte    // Массивы энергии в пределах 12 месяцев
        {
            FromReset,      // От сброса
            CurrentYear,    // Текущий год
            PastYear,       // Прошедший год
            Month,          // За месяц
            CurrentDay,     // За текущие сутки
            PastDay,        // За прошедшие сутки
            PerPhase        // По фазам
        }
        public enum Rates : byte  // Тарифы
        {
            Sum,    // Сумма тарифов
            Rate1,  // Тариф 1
            Rate2,  // Тариф 2
            Rate3,  // Тариф 3
        }
        public enum Months : byte  // Месяцы
        {
            None,
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        public EnergyDataTypes EnergyDataType { get; private set; }
        public Months Month { get; private set; }
        public Rates Rate { get; private set; }
        public ReadStoredEnergyRequest(byte addr, EnergyDataTypes energyDataType, Months month, Rates rate) : base(addr)
        {
            c = RequestTypes.ReadArray;
            EnergyDataType = energyDataType;
            Month = month;
            Rate = rate;
            //if (dataArray == EnergyDataArrays.PerPhase)  // TODO: Delete ResponseLength?
            //    ResponseLength = 15;
            //else
            //    ResponseLength = 19;
        }
        private byte CombineMonthAndEnergyDataArray(EnergyDataTypes energyDataType, Months month)  // TODO: Docs
        {

            byte result = (byte)((byte)energyDataType << 4);
            return (byte)(result | (byte)month);
        }

        public override byte[] Create()
        {
            byte[] body = new byte[] { Address, (byte)RequestType, CombineMonthAndEnergyDataArray(EnergyDataType, Month), (byte)Rate };
            return AddCRC(body);
        }
    }
}
