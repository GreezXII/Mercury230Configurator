using System.Runtime.Serialization;

namespace M230Protocol.Exceptions
{
	/// <summary>
	/// Throws when record number in journal reading request is not in 0...9 range.
	/// </summary>
	public class InvalidRecordNumberException : Exception
	{
		string _message = "Record number should be in 0...9 range.";
		public InvalidRecordNumberException()
		{
		}
		public InvalidRecordNumberException(string? message) : base(message)
		{
		}
		public InvalidRecordNumberException(string? message, Exception? innerException) : base(message, innerException)
		{
		}
		protected InvalidRecordNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
