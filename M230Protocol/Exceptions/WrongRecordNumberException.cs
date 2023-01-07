using System.Runtime.Serialization;

namespace M230Protocol.Exceptions
{
	public class WrongRecordNumberException : Exception
	{
		string _message = "Record number should be in 0...9 range.";
		public WrongRecordNumberException()
		{
		}
		public WrongRecordNumberException(string? message) : base(message)
		{
		}
		public WrongRecordNumberException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
		protected WrongRecordNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
