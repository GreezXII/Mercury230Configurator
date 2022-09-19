using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class ReadJournalRecordRequest : Request
    {
        public enum Journals : byte
        {
            OnOff = 0x01,                     // Время включения и выключения прибора
            Phase1OnOff = 0x03,               // Время включения и выключения фазы 1
            Phase2OnOff = 0x04,               // Время включения и выключения фазы 2
            Phase3OnOff = 0x05,               // Время включения и выключения фазы 3
            OpeningClosing = 0x12,            // Время вскрытия и закрытия прибора
            Phase1CurrentOnOff = 0x17,        // Время включения и отключения тока фазы 1
            Phase2CurrentOnOff = 0x18,        // Время включения и отключения тока фазы 2
            Phase3CurrentOnOff = 0x19         // Время включения и отключения тока фазы 3
        }
        public byte JournalNumber { get; private set; }
        public byte RecordNumber { get; private set; }
        public ReadJournalRecordRequest(byte addr, Journals journal, byte recordNumber) : base(addr)
        {
            RequestType = RequestTypes.ReadJournal;
            JournalNumber = (byte)journal;
            RecordNumber = recordNumber;
        }

        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, JournalNumber, RecordNumber };
            return CreateByteArray(requestBody);
        }
    }
}
