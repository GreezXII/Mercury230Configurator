namespace M230Protocol
{
    enum MeterJournals : byte
    {
        OnOff = 0x01,                     // Время включения и выключения прибора
        Phase1OnOff = 0x03,               // Время включения и выключения фазы 1
        Phase2OnOff = 0x04,               // Время включения и выключения фазы 2
        Phase3OnOff = 0x05,               // Время включения и выключения фазы 3
        OpeningClosing = 0x12,            // Время вскрытия и закрытия прибора
        Phase1CurrentOnOff = 0x17,        // Время включения и отключения тока фазы 1
        Phase2CurrentOnOff = 0x18,        // Время включения и отключения тока фазы 2
        Phase3CurrentOnOff = 0x19         // Время включения и отключения тока фазы 3
    }
}
