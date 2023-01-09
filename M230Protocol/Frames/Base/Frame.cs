using System.Diagnostics;
using System.Reflection;

namespace M230Protocol.Frames.Base
{
	/// <summary>
	/// Base class for frames. Frame in this context is a class with specific fields that parsed from a byte array.
	/// Byte array specifies a command for the meter or response from the meter.
	/// </summary>
	public class Frame
    {
		/// <summary>
		/// Address of the meter.
		/// </summary>
		public byte Address { get; protected set; }
		/// <summary>
		/// Cyclic redundancy check, checksum for data integrity. CRC16 with MODBUS polynomial.
		/// </summary>
		public byte[]? CRC { get; protected set; }

        public Frame() { }
        public Frame(byte address) => Address = address;
		/// <summary>
		/// Calculates CRC16 with MODBUS polynomial.
		/// </summary>
		/// <param name="buffer">Array for which CRC is required.</param>
		/// <returns>Return array with two CRC bytes.</returns>
		public static byte[] CalculateCRC16Modbus(byte[] buffer)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < buffer.Length; pos++)
            {
                crc ^= buffer[pos];
                for (int i = 8; i != 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                        crc >>= 1;
                }
            }
            return BitConverter.GetBytes(crc);
        }
    }
}
