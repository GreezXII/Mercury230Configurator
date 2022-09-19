using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
    class OpenConnectionRequest : Request
    {
        public byte AccessLevel { get; private set; }
        public byte[] Password { get; private set; }
        public OpenConnectionRequest(byte addr, MeterAccessLevels accLvl, string pwd) : base(addr)
        {
            RequestType = RequestTypes.OpenConnection;
            AccessLevel = (byte)accLvl;
            Password = StringToBCD(pwd);
        }

        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, AccessLevel };
            requestBody.AddRange(Password);
            return CreateByteArray(requestBody);

        }
    }
}