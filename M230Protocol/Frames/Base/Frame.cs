using System.Diagnostics;
using System.Reflection;

namespace M230Protocol.Frames.Base
{
    /* Base class for all frames.
     * +---------+---------+
     * | Address |   CRC   |
     * | 1 byte  | 2 bytes |
     * +---------+---------+
     * 
     * Address - address of meter.
     * CRC - Cyclic redundancy check, checksum for data integrity. 
     *       CRC16 with MODBUS polynomial
     * 
     * This is base class for other frames.
     */
    class Frame
    {
        public byte Address { get; protected set; }
        public byte[] CRC { get; protected set; }
        public int Length { get; protected set; }

        public Frame() { }

        public Frame(byte addr)
        {
            Address = addr;
            Length = 3;
        }

        protected byte[] AddCRC(byte[] body)
        {
            byte[] CRC = CalculateCRC16Modbus(body);
            byte[] result = new byte[body.Length + CRC.Length];
            body.CopyTo(result, 0);
            CRC.CopyTo(result, body.Length);
            return result;
        }

        public static bool CRCMatch(byte[] a, byte[] b)
        {
            if (a == null || b == null)
                return false;
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;

            return true;
        }

        private byte[] CalculateCRC16Modbus(byte[] buffer)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < buffer.Length; pos++)
            {
                crc ^= (ushort)buffer[pos];
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
            CRC = BitConverter.GetBytes(crc);
            return CRC;
        }
    }
}
