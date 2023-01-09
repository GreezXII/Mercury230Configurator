using M230Protocol.Exceptions;
using M230Protocol.Frames.Base;
using System.Text;

namespace M230Protocol.Frames.Requests
{
    /// <summary>
    /// Command to change location setting of the meter.
    /// </summary>
    public class WriteLocationRequest : Request
    {
        /// <summary>
        /// Parameter that defines that this type of command is for location writing.
        /// </summary>
        public byte ParameterNumber { get; private set; }
        /// <summary>
        /// New location value as byte array.
        /// </summary>
        public byte[] Location { get; private set; }

        public WriteLocationRequest(byte addr, string location) : base(addr)
        {
            if (location.Length < 0 || location.Length > 4)
                throw new InvalidLocationFormat();
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
