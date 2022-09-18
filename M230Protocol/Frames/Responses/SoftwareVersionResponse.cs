using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    class SoftwareVersionResponse : Response
    {
        public string SoftwareVersion { get; private set; }
        public SoftwareVersionResponse(byte[] response) : base(response)
        {
            byte[] buffer = new byte[3];
            Array.Copy(response, 1, buffer, 0, 3);

            SoftwareVersion = buffer[0].ToString();
            for (int i = 1; i < buffer.Length; i++)
            {
                SoftwareVersion += "." + buffer[i].ToString();
            }
        }
    }
}
