using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Responses
{
    public class SerialNumberAndReleaseDateResponse : Response
    {
        public const int Length = 10;
        /// <summary>
        /// Serial number of the meter.
        /// </summary>
        public int SerialNumber { get; private set; }
        /// <summary>
        /// Release date of the meter.
        /// </summary>
        public DateTime ReleaseDate { get; private set; }

        public SerialNumberAndReleaseDateResponse(byte[] response) : base(response)
        {
            byte[] serialNumberBuffer = new byte[4];
            Array.Copy(response, 1, serialNumberBuffer, 0, 4);
            SerialNumber = BitwiseBytesToInt(serialNumberBuffer);

            byte[] releaseDateBuffer = new byte[3];
            Array.Copy(response, 5, releaseDateBuffer, 0, 3);
            ReleaseDate = new DateTime(2000 + releaseDateBuffer[2], releaseDateBuffer[1], releaseDateBuffer[0]);
        }
    }
}
