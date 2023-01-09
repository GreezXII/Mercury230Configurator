using M230Protocol.Frames.Base;

namespace M230Protocol.Frames.Requests
{
	/// <summary>
	/// Command to open connection with the meter.
	/// </summary>
	public class OpenConnectionRequest : Request
    {
        /// <summary>
        /// Defines what commands are allowed for user.
        /// </summary>
        public byte AccessLevel { get; private set; }
		/// <summary>
		/// Confirms eligibility to access level.
		/// </summary>
		public byte[] Password { get; private set; }

        public OpenConnectionRequest(byte addr, MeterAccessLevels accLvl, string pwd) : base(addr)
        {
            RequestType = RequestTypes.OpenConnection;
            AccessLevel = (byte)accLvl;
            Password = StringToBCD(pwd);
        }
		/// <summary>
		/// Create command for transmitting to the meter.
		/// </summary>
		/// <returns>Array with command for the meter.</returns>
		public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, AccessLevel };
            requestBody.AddRange(Password);
            return CreateByteArray(requestBody);
        }
    }
}