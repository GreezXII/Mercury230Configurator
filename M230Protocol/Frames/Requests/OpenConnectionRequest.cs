using M230Protocol.Frames.Base;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

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
        public SecureString SecurePassword { get; set; }

        public OpenConnectionRequest(byte addr, MeterAccessLevels accLvl, SecureString pwd) : base(addr)
        {
            RequestType = RequestTypes.OpenConnection;
            AccessLevel = (byte)accLvl;
            SecurePassword = pwd;
        }
        /// <inheritdoc cref="Request.Create"/>
        public override byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, AccessLevel };
            requestBody.AddRange(GetBytesFromSecureString(SecurePassword));
            return CreateByteArray(requestBody);
        }
        private byte[] GetBytesFromSecureString(SecureString secureString)
        {
            IntPtr bstr = IntPtr.Zero;
            byte[]? workArray = null;
            GCHandle? handle = null;
            try
            {
                bstr = Marshal.SecureStringToGlobalAllocAnsi(secureString);
                unsafe
                {
                    byte* bstrBytes = (byte*)bstr;
                    workArray = new byte[secureString.Length];
                    handle = GCHandle.Alloc(workArray, GCHandleType.Pinned);
                    for (int i = 0; i < workArray.Length; i++)
                    {
                        byte b = (byte)(*bstrBytes++ - 48);
                        if (b < 0 || b > 9)
                            throw new InvalidOperationException("The secure string has an invalid character. M230 meter uses only digits for passwords.");
                        workArray[i] = b;
                    }
                }
                return workArray;
            }
            finally
            {
                handle?.Free();
                if (bstr != IntPtr.Zero)
                    Marshal.ZeroFreeGlobalAllocAnsi(bstr);
            }
        }
    }
}