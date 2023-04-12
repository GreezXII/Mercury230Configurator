using System;

namespace DesktopClient.Helpers.Types
{
    class JournalRecord
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public JournalRecord()
        {
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
        }
        public JournalRecord(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
