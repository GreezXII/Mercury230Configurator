using System;
using System.IO;
using System.Xml.Serialization;

namespace DesktopClient.Service
{
    [Serializable]
    public class SettingsService
    {
        public static string Filename = "Settings.xml";

        public string? Address { get; set; }
        public string AccessLevel { get; set; }
        public string? ComPortName { get; set; }
        public string Timeout { get; set; }

        public SettingsService()
        {
            AccessLevel = "Пользователь";
            Timeout = "5000";
        }

        public static void Save(SettingsService settings)
        {
            var serializer = new XmlSerializer(typeof(SettingsService));
            using var fs = new FileStream(Filename, FileMode.OpenOrCreate);
            serializer.Serialize(fs, settings);
        }

        public static SettingsService Load()
        {
            var serializer = new XmlSerializer(typeof(SettingsService));
            using var fs = new FileStream(Filename, FileMode.OpenOrCreate);
            var deserialized = serializer.Deserialize(fs);
            if (deserialized == null)
                throw new Exception("Не удалось создать класс для сохранения настроек.");
            else
                return (SettingsService)deserialized;
        }
    }
}
