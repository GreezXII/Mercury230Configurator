using System.Runtime.Serialization;

namespace M230Protocol.Exceptions
{
	internal class InvalidLocationFormat : Exception
	{
		string _message = "Location string length should be from 0 to 4.";
		public override string Message => _message;
		public InvalidLocationFormat()
		{
		}
		public InvalidLocationFormat(string? message) : base(message)
		{
		}
		public InvalidLocationFormat(string? message, Exception? innerException) : base(message, innerException)
		{
		}
		protected InvalidLocationFormat(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
