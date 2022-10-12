using MeterClient;
using M230Protocol;
using M230Protocol.Frames.Requests;
using M230Protocol.Frames.Responses;

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
        public async Task OpenUserConnection()
        {
            CommunicationState result = await Meter.OpenConnectionAsync(MeterAccessLevels.User, userPassword);
            Assert.AreEqual(result, CommunicationState.OK);
        }

        //[TestCleanup]
        //public async Task CloseConnection()
        //{
        //    CloseConnectionRequest openConnectionRequest = new(89);
        //    byte[] outputBuffer = openConnectionRequest.Create();
        //    await SerialPortClient.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, Token);
        //}

        //[TestMethod]
        //public async Task TestLink()
        //{
        //    SerialPortClient SerialPortClient = new("COM5");
        //    TestLinkRequest testLinkRequest = new(89);
        //    byte[] outputBuffer = testLinkRequest.Create();
        //    CancellationToken Token = new CancellationToken();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, Token);
        //    CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
        //    Assert.AreEqual(response.State, CommunicationState.OK);
        //}

        //[TestMethod]
        //public async Task OpenConnection_User_Success()
        //{
        //    OpenConnectionRequest openConnectionRequest = new(89, MeterAccessLevels.User, userPassword);
        //    byte[] outputBuffer = openConnectionRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, Token);
        //    CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
        //    Assert.AreEqual(response.State, CommunicationState.OK);
        //}

        //[TestMethod]
        //public async Task OpenConnection_Admin_Success()
        //{
        //    OpenConnectionRequest openConnectionRequest = new(89, MeterAccessLevels.Admin, adminPassword);
        //    byte[] outputBuffer = openConnectionRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, Token);
        //    CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
        //    Assert.AreEqual(response.State, CommunicationState.OK);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_Phase1OnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.Phase1OnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_Phase2OnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.Phase2OnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_Phase3OnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.Phase3OnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_Phase1CurrentOnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.Phase1CurrentOnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_Phase2CurrentOnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.Phase2CurrentOnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_Phase3CurrentOnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.Phase3CurrentOnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_OnOff_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.OnOff, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}

        //[TestMethod]
        //public async Task ReadJournalRecord_OpenClosing_Success()
        //{
        //    ReadJournalRecordRequest readJournalRecordRequest = new(89, MeterJournals.OpeningClosing, 0);
        //    byte[] outputBuffer = readJournalRecordRequest.Create();
        //    byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, Token);
        //    ReadJournalResponse response = new ReadJournalResponse(inputBuffer);
        //}
    }
}