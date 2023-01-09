using M230Protocol.Exceptions;

namespace M230Protocol.Frames.Base
{
    /// <summary>
    /// Base class for responses. Response in this context is answer from the meter that was parsed from a received byte array.
    /// </summary>
    public class Response : Frame
    {
        /// <summary>
        /// Response from the meter with excluded meter address and CRC.
        /// </summary>
        public byte[] Body { get; private set; }
        public Response(byte[] response)
        {
            CRC = new byte[] { response[^2], response[^1] };
            if (!CheckCRC(response))
                throw new CrcDoesNotMatchException();
            Address = response[0];
            Body = new byte[response.Length - 3];
            Array.Copy(response, 1, Body, 0, response.Length - 3);
        }
        /// <summary>
        /// Check that response was not changed during transmitting.
        /// </summary>
        /// <param name="response">Response from the meter.</param>
        /// <returns></returns>
        private bool CheckCRC(byte[] response)
        {
            byte[] buffer = new byte[response.Length - 2];
            Array.Copy(response, 0, buffer, 0, response.Length - 2);
            byte[] calculatedCRC = CalculateCRC16Modbus(buffer);
            if (CRC == null)
                return false;
            return Enumerable.SequenceEqual(CRC, calculatedCRC);
        }
		// TODO: Change functions that work with number representations
		protected int FullHexToInt(byte[] buffer)
        {
            string hex = "";
            for (int i = 0; i < buffer.Length; i++)
                hex += ByteToHexString(buffer[i]);
            return Convert.ToInt32(hex, 16);
        }
        protected int BitwiseBytesToInt(byte[] buffer)
        {
            int result = buffer[0];
            for (int i = 1; i < buffer.Length; i++)
            {
                result *= 100;
                result += buffer[i];
            }
            return result;
        }
        protected string ByteToHexString(byte b)
        {
            string hex = Convert.ToString(b, 16);
            if (hex.Length == 1)
                hex = "0" + hex;
            return hex;
        }
        protected byte ByteToHexByte(byte b)
        {
            string hex = ByteToHexString(b);
            return byte.Parse(hex);
        }
        protected double GetEnergyValue(byte[] array)
        {
            // Изменить порядок байт согласно документации
            byte[] buffer = new byte[array.Length];
            buffer[0] = array[1];
            buffer[1] = array[0];
            buffer[2] = array[3];
            buffer[3] = array[2];

            return FullHexToInt(buffer) / 1000.0d;
        }
    }
}
