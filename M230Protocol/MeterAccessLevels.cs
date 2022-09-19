namespace M230Protocol
{
    enum MeterAccessLevels : byte  // Уровень доступа к счётчику
    {
        User = 0x01,  // Пользователь
        Admin = 0x02  // Администратор
    }
}
