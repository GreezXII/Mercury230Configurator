using MeterClient;
using M230Protocol;
using M230Protocol.Frames.Responses;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MeterClientTests
    {
        static Meter Meter = new Meter(89, "COM5");
        static SerialPortClient SerialPortClient { get; set; }
        CancellationToken Token { get; set; }
        static readonly string userPassword = "111111";
        static readonly string adminPassword = "222222";

        [TestMethod]
        [ClassInitialize]
        public static async Task InitializeMeter(TestContext TestContext)
        {
            Meter.OpenConnectionAsync(MeterAccessLevels.User, userPassword).Wait();
        }

        [TestMethod]
        public async Task TestLink()
        {
            CommunicationState result = await Meter.TestLinkAsync();
            Assert.AreEqual(result, CommunicationState.OK);
        }

        [TestMethod]
        public async Task ReadJournalRecords_OnOff()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.OnOff);
        }
        
        [TestMethod]
        public async Task ReadJournalRecord_Phase1OnOff_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase1OnOff);
        }

        [TestMethod]
        public async Task ReadJournalRecord_Phase2OnOff_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase2OnOff);
        }

        [TestMethod]
        public async Task ReadJournalRecord_Phase3OnOff_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase3OnOff);
        }

        [TestMethod]
        public async Task ReadJournalRecord_Phase1CurrentOnOff_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase1CurrentOnOff);
        }

        [TestMethod]
        public async Task ReadJournalRecord_Phase2CurrentOnOff_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase2CurrentOnOff);
        }

        [TestMethod]
        public async Task ReadJournalRecord_Phase3CurrentOnOff_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase3CurrentOnOff);
        }

        [TestMethod]
        public async Task ReadJournalRecord_OpeningClosing_Success()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.OpeningClosing);
        }

        // TODO: add to test separate rates?
		[TestMethod]
		public async Task ReadStoredEnergyFromReset_Success()
		{
            var result = await Meter.ReadStoredEnergyFromResetAsync(MeterRates.Sum);
		}

		[TestMethod]
		public async Task ReadStoredEnergyCurrentYear_Success()
		{
			var result = await Meter.ReadStoredEnergyCurrentYearAsync(MeterRates.Sum);
		}

		[TestMethod]
		public async Task ReadStoredEnergyPastYear_Success()
		{
			var result = await Meter.ReadStoredEnergyPastYearAsync(MeterRates.Sum);
		}

		[TestMethod]
		public async Task ReadStoredEnergyCurrentDay_Success()
		{
			var result = await Meter.ReadStoredEnergyCurrentDayAsync(MeterRates.Sum);
		}

		[TestMethod]
		public async Task ReadStoredEnergyPastDay_Success()
		{
			var result = await Meter.ReadStoredEnergyPastDayAsync(MeterRates.Sum);
		}

		[TestMethod]
		public async Task ReadStoredEnergyByMonth_Success()
		{
            foreach (Months month in Enum.GetValues(typeof(Months)))
            {
                // TODO: Delete Months.None
                if (month == Months.None)
                    continue;
                await Meter.ReadStoredEnergyByMonthAsync(MeterRates.Sum, month);
			}
		}

		[TestMethod]
		public async Task ReadStoredEnergyPerPhases_Success()
		{
            var result = await Meter.ReadStoredEnergyPerPhasesAsync(MeterRates.Sum);
		}

        [TestMethod]
        public async Task ReadSerialNumberAndReleaseDate_Success()
        {
            var result = await Meter.ReadSerialNumberAndReleaseDateAsync();
        }

        [TestMethod]
        public async Task ReadLocation_Success()
        {
            var result = await Meter.ReadLocationAsync();
        }

        [TestMethod]
        public async Task ReadSoftwareVersion()
        {
            var result = await Meter.ReadSoftwareVersionAsync();
        }

		[TestMethod]
        [ClassCleanup]
        public static async Task CloseConnection()
        {
            await Meter.CloseConnectionAsync();
        }
    }
}