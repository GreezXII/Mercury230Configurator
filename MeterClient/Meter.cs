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
        public async Task<ReadStoredEnergyResponse> ReadStoredEnergyFromResetAsync(MeterRates meterRates, CancellationToken token = default)
        {
            ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.FromReset, 0x0, meterRates);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyResponse.Length, token);
            return new ReadStoredEnergyResponse(inputBuffer);
		}
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyCurrentYearAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.CurrentYear, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyPastYearAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.PastYear, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyCurrentDayAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.CurrentDay, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyPastDayAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.PastDay, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyByMonthAsync(MeterRates meterRates, Months month, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.Month, month, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}
        public async Task<ReadStoredEnergyPerPhaseResponse> ReadStoredEnergyPerPhasesAsync(MeterRates meterRates, CancellationToken token = default)
        {
            ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.PerPhases, null, meterRates);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, ReadStoredEnergyPerPhaseResponse.Length, token);
            return new ReadStoredEnergyPerPhaseResponse(inputBuffer);
		}
        public async Task<SerialNumberAndReleaseDateResponse> ReadSerialNumberAndReleaseDateAsync(CancellationToken token = default)
        {
			ReadSettingsRequest request = new ReadSettingsRequest(Address, MeterSettings.SerialNumberAndReleaseDate, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, SerialNumberAndReleaseDateResponse.Length, token);
            return new SerialNumberAndReleaseDateResponse(inputBuffer);
        }
        public async Task<LocationResponse> ReadLocationAsync(CancellationToken token = default)
        {
            ReadSettingsRequest request = new ReadSettingsRequest(Address, MeterSettings.Location, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, LocationResponse.Length, token);
            return new LocationResponse(inputBuffer);
        }
		public async Task<SoftwareVersionResponse> ReadSoftwareVersionAsync(CancellationToken token = default)
		{
            ReadSettingsRequest request = new ReadSettingsRequest(Address, MeterSettings.SoftwareVersion, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.PerformDataExchange(outputBuffer, SoftwareVersionResponse.Length, token);
            return new SoftwareVersionResponse(inputBuffer);
		}
	}
}
