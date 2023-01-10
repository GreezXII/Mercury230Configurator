namespace M230Protocol
{
    public enum MeterJournals
    {
        /// <summary>Meter enable and disable time.</summary>
        OnOff = 0x01,                    
        /// <summary>Phase 1 on and off time.</summary>
        Phase1OnOff = 0x03,
        /// <summary>Phase 2 on and off time.</summary>
        Phase2OnOff = 0x04,
        /// <summary>Phase 3 on and off time.</summary>
        Phase3OnOff = 0x05,
        /// <summary>Opening and closing time of the meter.</summary>
        OpeningClosing = 0x12,
        /// <summary>Phase 1 current on and off time.</summary>
        Phase1CurrentOnOff = 0x17,        
        /// <summary>Phase 2 current on and off time.</summary>
        Phase2CurrentOnOff = 0x18,        
        /// <summary>Phase 3 current on and off time.</summary>
        Phase3CurrentOnOff = 0x19         
    }
}
