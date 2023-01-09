using System.Runtime.Serialization;

namespace M230Protocol.Exceptions
{
	/// <summary>
	/// Throws when the CRC of the received byte array does not match the calculated CRC value.
	/// </summary>
	internal class CrcDoesNotMatchException : Exception
	{
		private string _message = "The CRC of the received packet does not match the calculated CRC value.";
		public override string Message => _message;
		public CrcDoesNotMatchException() { }
		public CrcDoesNotMatchException(string? message) : base(message) { }
		public CrcDoesNotMatchException(string? message, Exception? innerException) : base(message, innerException) { }
		protected CrcDoesNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
