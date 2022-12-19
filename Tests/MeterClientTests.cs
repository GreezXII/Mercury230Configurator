using MeterClient;
using M230Protocol;
using M230Protocol.Frames.Responses;
using M230Protocol.Frames.Requests;

namespace Tests
{
    [TestClass]
    public class MeterClientTests
    {
        Meter Meter { get; set; }
        SerialPortClient SerialPortClient { get; set; }
        CancellationToken Token { get; set; }
        readonly string userPassword = "111111";
        readonly string adminPassword = "222222";

        public MeterClientTests()
        {
            Meter = new Meter(89, "COM5");
        }

        [TestMethod]
        public async Task TestLink()
        {
            CommunicationState result = await Meter.TestLinkAsync();
            Assert.AreEqual(result, CommunicationState.OK);
        }

        [TestMethod]
        public async Task OpenUserConnection()
        {
            CommunicationState result = await Meter.OpenConnectionAsync(MeterAccessLevels.User, userPassword);
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

        [TestMethod]
        public async Task CloseConnection()
        {
            List<ReadJournalResponse> result = await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase1OnOff);
        }
    }
}