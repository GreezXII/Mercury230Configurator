using System.Diagnostics;

namespace M230Protocol.Frames.Base
{
    class Response : Frame
    {
        public static int Length;
        public byte[] Body { get; private set; }
        public Response(byte[] response)
        {
            Address = response[0];
            Body = new byte[response.Length - 3];
            Array.Copy(response, 1, Body, 0, response.Length - 3);
            // TODO: Move CRC check in another class
            //CRC = new byte[] { response[^2], response[^1] };
            //if (!CheckCRC(response))
            //    throw new Exception("CRC принятого пакета не совпадает с полученным значением CRC при проверке.");
            foreach (byte b in response)
                Trace.Write($"{Convert.ToString(b, 16)} ");  // TODO: Logging
            Trace.WriteLine("");
        }
        //private bool CheckCRC(byte[] response) // TODO: Move CRC check in another class
        //{
        //    byte[] buffer = new byte[response.Length - 2];
        //    Array.Copy(response, 0, buffer, 0, response.Length - 2);
        //    byte[] CRCval = CalculateCRC16Modbus(buffer);
        //    return CRCMatch(CRC, CRCval);
        //}
        internal int FullHexToInt(byte[] buffer)
        {
            string hex = "";
            for (int i = 0; i < buffer.Length; i++)
                hex += ByteToHexString(buffer[i]);
            return Convert.ToInt32(hex, 16);
        }
        internal int BiwiseBytesToInt(byte[] buffer)
        {
            int result = buffer[0];
            for (int i = 1; i < buffer.Length; i++)
            {
                result *= 100;
                result += buffer[i];
            }
            return result;
        }
        internal string ByteToHexString(byte b)
        {
            string hex = Convert.ToString(b, 16);
            if (hex.Length == 1)
                hex = "0" + hex;
            return hex;
        }
        internal byte ByteToHexByte(byte b)
        {
            string hex = ByteToHexString(b);
            return byte.Parse(hex);
        }
    }
}
