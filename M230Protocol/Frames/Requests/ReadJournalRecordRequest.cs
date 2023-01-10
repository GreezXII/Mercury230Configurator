using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    /// <summary>
    /// Command for reading of event's journals.
    /// </summary>
    public class ReadJournalRecordRequest : Request
    {
        /// <summary>
        /// Event journal which you want to read.
        /// </summary>
        public byte Journal { get; private set; }
        /// <summary>
        /// Record number in journal. All journals consists of 10 records from 0 to 9.
        /// </summary>
        public byte RecordNumber { get; private set; }

        public ReadJournalRecordRequest(byte addr, MeterJournals journal, byte recordNumber) : base(addr)
        {
            RequestType = RequestTypes.ReadJournal;
            Journal = (byte)journal;
            RecordNumber = recordNumber;
        }
        /// <inheritdoc cref="Request.Create"/>
		public override byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, Journal, RecordNumber };
            return CreateByteArray(requestBody);
        }
    }
}
