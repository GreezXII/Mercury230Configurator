namespace M230Protocol
{
    enum RequestTypes : byte
    {
        TestConnection = 0x00,  // Тестирование канала связи
        OpenConnection = 0x01,  // Запрос на открытие канала связи
        CloseConnection = 0x02, // Запрос на закрытие канала связи
        ReadSettings = 0x08,    // Запрос на чтение параметров
        ReadJournal = 0x04,     // Запрос на чтение массивов времени (журналов)
        ReadArray = 0x05,       // Запрос на чтение массивов энергии в пределах 12 месяцев
        WriteSettings = 0x03,   // Запрос на запись параметров
    }
}
