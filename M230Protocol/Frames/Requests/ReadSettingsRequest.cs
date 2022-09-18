using M230Protocol.Frames.Base;
 
namespace M230Protocol.Frames.Requests
{
    class ReadSettingsRequest : Request
    {
        public enum Settings : byte
        {
            SerialNumberAndReleaseDate = 0x00,  // Серийный номер и дата выпуска
            SoftwareVersion = 0x03,             // Версия ПО
            Location = 0x0B                     // Местоположение
        }
        public byte SettingType { get; private set; }
        public byte[] Parameters { get; private set; }
        public ReadSettingsRequest(byte addr, Settings settings, byte[] parameters) : base(addr)
        {
            RequestType = RequestTypes.ReadSettings;
            SettingType = (byte)settings;
            Parameters = parameters;
        }

        public override byte[] Create()
        {
            List<byte> body = new() { Address, (byte)RequestType, SettingType };
            body.AddRange(Parameters);
            return AddCRC(body.ToArray());
        }
    }
}
