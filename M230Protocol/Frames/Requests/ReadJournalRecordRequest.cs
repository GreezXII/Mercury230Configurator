using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class ReadJournalRecordRequest : Request
    {
        public byte JournalNumber { get; private set; }
        public byte RecordNumber { get; private set; }
        public ReadJournalRecordRequest(byte addr, MeterJournals journal, byte recordNumber) : base(addr)
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
