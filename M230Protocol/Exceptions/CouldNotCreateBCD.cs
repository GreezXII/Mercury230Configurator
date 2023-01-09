using System.Runtime.Serialization;

namespace M230Protocol.Exceptions
{
	internal class CouldNotCreateBCD : Exception
	{
		string _message = "Could not create binary coded decimal.";
		public override string Message => _message;
		public CouldNotCreateBCD()
		{
		}
		public CouldNotCreateBCD(string? message) : base(message)
		{
		}
		public CouldNotCreateBCD(string? message, Exception? innerException) : base(message, innerException)
		{
		}
		protected CouldNotCreateBCD(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
