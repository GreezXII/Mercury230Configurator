using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    public class ReadJournalResponse : Response
    {
        public const int Length = 9;
        public List<DateTime> Records { get; private set; } = new List<DateTime>();
        public ReadJournalResponse(byte[] response) : base(response)
        {
            byte[] buffer = new byte[6];
            Array.Copy(response, 1, buffer, 0, buffer.Length);
            Records.Add(ParseDateTime(buffer));
            Array.Copy(response, 7, buffer, 0, buffer.Length);
            Records.Add(ParseDateTime(buffer));
        }
        private DateTime ParseDateTime(byte[] buffer)
        {
            byte year = ByteToHexByte(buffer[5]);
            if (year == 0)
                return default(DateTime);
            byte month = ByteToHexByte(buffer[4]);
            byte day = ByteToHexByte(buffer[3]);
            byte hour = ByteToHexByte(buffer[2]);
            byte minute = ByteToHexByte(buffer[1]);
            byte second = ByteToHexByte(buffer[0]);
            return new DateTime(2000 + year, month, day, hour, minute, second);
        }
    }
}
