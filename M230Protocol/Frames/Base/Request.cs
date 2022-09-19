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
