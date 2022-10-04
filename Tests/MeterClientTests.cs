using MeterClient;
using M230Protocol;
using M230Protocol.Frames.Requests;
using M230Protocol.Frames.Responses;

namespace Tests
{
    [TestClass]
    public class MeterClientTests
    {
        [TestMethod]
        public async Task TestLink()
        {
            SerialPortClient SerialPortClient = new("COM5");
            TestLinkRequest testLinkRequest = new(89);
            byte[] outputBuffer = testLinkRequest.Create();
            CancellationToken token = new CancellationToken();
            byte[] inputBuffer = await SerialPortClient.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, token);
            CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
            Assert.AreEqual(response.State, CommunicationState.OK);
        }
    }
}