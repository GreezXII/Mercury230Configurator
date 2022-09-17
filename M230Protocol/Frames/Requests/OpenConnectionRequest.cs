using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class OpenConnectionRequest : Request
    {
        public enum MeterAccessLevels : byte  // Уровень доступа к счётчику
        {
            User = 0x01,  // Пользователь
            Admin = 0x02  // Администратор
        }

        public byte AccessLevel { get; private set; }
        public byte[] Password { get; private set; }
        public OpenConnectionRequest(byte addr, MeterAccessLevels accLvl, string pwd) : base(addr)
        {
            RequestType = RequestTypes.OpenConnection;
            AccessLevel = (byte)accLvl;
            Password = StringToBCD(pwd);
            Length += 1 + Password.Length;
        }

        public override byte[] Create()
        {
            List<byte> body = new() { Address, (byte)RequestType, AccessLevel };
            body.AddRange(Password);
            byte[] buffer = body.ToArray();
            Length = buffer.Length;
            return AddCRC(buffer);
        }
    }
}