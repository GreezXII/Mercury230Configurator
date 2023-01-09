using M230Protocol.Frames.Base;
 
namespace M230Protocol.Frames.Requests
{
    /// <summary>
    /// Command for reading of meter settings.
    /// </summary>
    public class ReadSettingsRequest : Request
    {
        /// <summary>
        /// Specifies setting.
        /// </summary>
        public byte SettingType { get; private set; }
        /// <summary>
        /// Additional feature for setting.
        /// </summary>
        public byte[] Parameters { get; private set; }

        public ReadSettingsRequest(byte addr, MeterSettings settings, byte[] parameters) : base(addr)
        {
            RequestType = RequestTypes.ReadSettings;
            SettingType = (byte)settings;
            Parameters = parameters;
        }
        public byte[] Create()
        {
            List<byte> requestBody = new() { (byte)RequestType, SettingType };
            requestBody.AddRange(Parameters);
            return CreateByteArray(requestBody);
        }
    }
}
