using M230Protocol.Frames.Base;
using System.Text;

namespace M230Protocol.Frames.Responses
{
    class LocationResponse : Response
    {
        public const int Length = 7;
        public string Location { get; private set; }
        public LocationResponse(byte[] response) : base(response)
        {
            byte[] buffer = new byte[4];
            Array.Copy(response, 1, buffer, 0, 4);
            Location = Encoding.ASCII.GetString(buffer);
        }
    }
}
