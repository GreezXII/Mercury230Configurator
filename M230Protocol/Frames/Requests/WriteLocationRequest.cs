using M230Protocol.Frames.Base;
using System.Text;

namespace M230Protocol.Frames.Requests
{
    class WriteLocationRequest : Request
    {
        public byte ParameterNumber { get; private set; }
        public byte[] Location { get; private set; }
        public WriteLocationRequest(byte addr, string location) : base(addr)
        {
            if (location.Length < 0 || location.Length > 4)
                throw new Exception("Местоположение может содержать от 0 до 4 символов.");
            RequestType = RequestTypes.WriteSettings;
            ParameterNumber = 0x22;
            location = location.PadRight(4, ' ');
            Location = Encoding.ASCII.GetBytes(location);
        }
        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, ParameterNumber };
            requestBody.AddRange(Location);
            return CreateByteArray(requestBody);
        }
    }
}
