using System.Runtime.Serialization;

namespace M230Protocol.Exceptions
{
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
