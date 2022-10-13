using M230Protocol;
using M230Protocol.Frames.Requests;
using M230Protocol.Frames.Responses;

namespace MeterClient
{
    public class Meter
    {
        public byte Address { get; set; }
        private SerialPortClient SerialPort { get; set; }
        public Meter(byte address, string portName)
        {
            Address = address;
            SerialPort = new SerialPortClient(portName);
        }
        public async Task<CommunicationState> TestLinkAsync(CancellationToken token = default)
        {
            TestLinkRequest request = new TestLinkRequest(Address);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, token);
            CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
            return response.State;
        }
        public async Task<CommunicationState> OpenConnectionAsync(MeterAccessLevels meterAccessLevel, string password, CancellationToken token = default)
        {
            OpenConnectionRequest request = new(Address, meterAccessLevel, password);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, token);
            CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
            return response.State;
        }
        public async Task<CommunicationState> CloseConnectionAsync(CancellationToken token = default)
        {
            CloseConnectionRequest request = new CloseConnectionRequest(Address);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, CommunicationStateResponse.Length, token);
            CommunicationStateResponse response = new CommunicationStateResponse(inputBuffer);
            return response.State;
        }
        public async Task<ReadJournalResponse> ReadJournalRecordAsync(MeterJournals journal, byte recordNumber, CancellationToken token = default)
        {
            ReadJournalRecordRequest readJournalRecordRequest = new ReadJournalRecordRequest(Address, journal, recordNumber);
            byte[] outputBuffer = readJournalRecordRequest.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadJournalResponse.Length, token);
            return new ReadJournalResponse(inputBuffer);
        }
        public async Task<List<ReadJournalResponse>> ReadAllJournalRecordsAsync(MeterJournals journal, CancellationToken token = default)
        {
            List<ReadJournalResponse> result = new List<ReadJournalResponse>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(await ReadJournalRecordAsync(MeterJournals.OnOff, (byte)i));
            }
            return result;
        }
    }
}
