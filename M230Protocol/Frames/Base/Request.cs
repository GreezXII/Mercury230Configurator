namespace M230Protocol.Frames.Base
{
    /* Base class for all requests.
     * +---------+------------------+---------+
     * | Address |   Request type   |   CRC   |
     * | 1 byte  |      1 byte      | 2 bytes |
     * +---------+------------------+---------+
     * 
     * Address - address of meter.
     * Request type - defines the functionality of the request.
     * CRC - Cyclic redundancy check, checksum for data integrity. 
     *       CRC16 with MODBUS polynomial.
     * 
     * This is base class for other requests.
     */
    abstract class Request : Frame
    {
        public enum RequestTypes : byte
        {
            TestConnection = 0x00,  // Test link
            OpenConnection = 0x01,  // Open connection
            CloseConnection = 0x02, // Close connection
            ReadSettings = 0x08,    // Read setings
            ReadJournal = 0x04,     // Read journal
            ReadArray = 0x05,       // Read energy arrays within 12 months
            WriteSettings = 0x03,   // Write settings
        }

        public RequestTypes RequestType { get; protected set; }
        public Request(byte addr) : base(addr) { }

        protected byte[] CreateByteArray(List<byte> specificRequest)
        {
            specificRequest.Insert(0, Address);
            return AddCRC(specificRequest);
        }

        private static byte[] AddCRC(List<byte> request)
        {
            byte[] CRC = CalculateCRC16Modbus(request.ToArray());
            request.AddRange(CRC);
            return request.ToArray();
        }

        internal static byte[] StringToBCD(string s)  // BCD - Binary-coded decimal TODO: Use .net built in function
        {
            byte[] bytePassword = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                byte b;
                bool result = byte.TryParse(s[i].ToString(), out b);
                if (!result)
                    throw new Exception($"Не удалось преобразовать {s} в двоично-десятичное представление.");
                bytePassword[i] = b;
            }
            return bytePassword;
        }
    }
}
