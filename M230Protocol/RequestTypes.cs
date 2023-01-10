namespace M230Protocol
{
    public enum RequestTypes
    {
        /// <summary>Communication channel testing.</summary>
        TestConnection = 0x00,
        /// <summary>Request to open communication channel.</summary> 
        OpenConnection = 0x01,
        /// <summary>Request to close communication channel.</summary>
        CloseConnection = 0x02,
        /// <summary>Request to read settings.</summary>
        ReadSettings = 0x08,
        /// <summary>Request to read time array.</summary>
        ReadJournal = 0x04,
        /// <summary>Request to read energy arrays within 12 months.</summary>
        ReadArray = 0x05,
        /// <summary>Write settings request.</summary>
        WriteSettings = 0x03
    }
}
