﻿namespace M230Protocol
{
    enum EnergyArrays : byte    // Массивы энергии в пределах 12 месяцев
    {
        FromReset,      // От сброса
        CurrentYear,    // Текущий год
        PastYear,       // Прошедший год
        Month,          // За месяц
        CurrentDay,     // За текущие сутки
        PastDay,        // За прошедшие сутки
        PerPhase        // По фазам
    }
}