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
    }
}
