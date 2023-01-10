using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    public class SoftwareVersionResponse : Response
    {
        public const int Length = 6;
        /// <summary>
        /// Version of internal meter software.
        /// </summary>
        public string SoftwareVersion { get; private set; }

        public SoftwareVersionResponse(byte[] response) : base(response)
        {
            byte[] buffer = new byte[3];
            Array.Copy(response, 1, buffer, 0, 3);
            SoftwareVersion = String.Join(".", buffer);
        }
    }
}
