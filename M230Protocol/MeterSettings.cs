namespace M230Protocol
{
    enum MeterSettings : byte
    {
        SerialNumberAndReleaseDate = 0x00,  // Серийный номер и дата выпуска
        SoftwareVersion = 0x03,             // Версия ПО
        Location = 0x0B                     // Местоположение
    }
}
