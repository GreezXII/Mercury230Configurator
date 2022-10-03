using M230Protocol.Frames.Base;
 
namespace M230Protocol.Frames.Requests
{
    public class ReadSettingsRequest : Request
    {
        public byte SettingType { get; private set; }
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
