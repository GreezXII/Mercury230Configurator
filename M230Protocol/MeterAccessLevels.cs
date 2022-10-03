namespace M230Protocol
{
    public enum MeterAccessLevels : byte  // Уровень доступа к счётчику
    {
        User = 0x01,  // Пользователь
        Admin = 0x02  // Администратор
    }
}
