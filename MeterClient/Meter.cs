using M230Protocol;
using M230Protocol.Exceptions;
using M230Protocol.Frames.Requests;
using M230Protocol.Frames.Responses;
using System.Security;

namespace MeterClient
{
	public class Meter
	{
		public byte Address { get; set; }
		public SerialPortClient SerialPort { get; set; }

        public Meter() => SerialPort = new SerialPortClient();
        public Meter(byte address, string portName)
        {
            Address = address;
            SerialPort = new SerialPortClient(portName);
        }

		/// <summary>
		/// Test physical connection with a meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="CommunicationState"/></returns>
		public async Task<CommunicationState> TestLinkAsync(CancellationToken token = default)
        {
            var request = new TestLinkRequest(Address);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, CommunicationStateResponse.Length, token);
            var response = new CommunicationStateResponse(inputBuffer);
			return response.State;
        }

		/// <summary>
		/// Open connection with meter.
		/// </summary>
		/// <param name="meterAccessLevel">Defines allowed requests.</param>
		/// <param name="password">Confirms the right to access level.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="CommunicationState"/>.</returns>
		public async Task<CommunicationState> OpenConnectionAsync(MeterAccessLevels meterAccessLevel, SecureString password, CancellationToken token = default)
        {
            var request = new OpenConnectionRequest(Address, meterAccessLevel, password);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, CommunicationStateResponse.Length, token);
            var response = new CommunicationStateResponse(inputBuffer);
            return response.State;
        }

		/// <summary>
		/// Close connection with meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="CommunicationStateResponse"/>.</returns>
		public async Task<CommunicationState> CloseConnectionAsync(CancellationToken token = default)
        {
            CloseConnectionRequest request = new CloseConnectionRequest(Address);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, CommunicationStateResponse.Length, token);
            var response = new CommunicationStateResponse(inputBuffer);
            return response.State;
        }

		/// <summary>
		/// Read one record from specified event journal.
		/// </summary>
		/// <param name="journal">Type of journal.</param>
		/// <param name="recordNumber">Serial number of record in journal. Should be in 0...9 range.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadJournalResponse"/>.</returns>
		/// <exception cref="InvalidRecordNumberException"></exception>
		public async Task<ReadJournalResponse> ReadJournalRecordAsync(MeterJournals journal, byte recordNumber, CancellationToken token = default)
        {
            if (recordNumber < 0 || recordNumber > 9)
                throw new InvalidRecordNumberException();
            ReadJournalRecordRequest readJournalRecordRequest = new ReadJournalRecordRequest(Address, journal, recordNumber);
            byte[] outputBuffer = readJournalRecordRequest.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadJournalResponse.Length, token);
            return new ReadJournalResponse(inputBuffer);
        }

		/// <summary>
		/// Read all records from specified enet journal.
		/// </summary>
		/// <param name="journal">Type of journal.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns List of <see cref="ReadJournalResponse"/>.</returns>
		public async Task<List<ReadJournalResponse>> ReadAllJournalRecordsAsync(MeterJournals journal, CancellationToken token = default)
        {
            List<ReadJournalResponse> result = new List<ReadJournalResponse>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(await ReadJournalRecordAsync(journal, (byte)i, token));
            }
            return result;
        }

		/// <summary>
		/// Read stored energy from reset for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyResponse"/>.</returns>
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyFromResetAsync(MeterRates meterRates, CancellationToken token = default)
        {
            ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.FromReset, 0x0, meterRates);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
            return new ReadStoredEnergyResponse(inputBuffer);
		}

		/// <summary>
		/// Read stored energy in current year for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyResponse"/>.</returns>
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyCurrentYearAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.CurrentYear, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}

		/// <summary>
		/// Read stored energy in past year for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyResponse"/>.</returns>
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyPastYearAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.PastYear, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}

		/// <summary>
		/// Read stored energy in current day for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyResponse"/>.</returns>
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyCurrentDayAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.CurrentDay, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}

		/// <summary>
		/// Read stored energy in past day for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyResponse"/>.</returns>
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyPastDayAsync(MeterRates meterRates, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.PastDay, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}

		/// <summary>
		/// Read stored energy by month for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="month">The month in which you need to get the accumulated energy.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyPerPhaseResponse"/>.</returns>
		public async Task<ReadStoredEnergyResponse> ReadStoredEnergyByMonthAsync(MeterRates meterRates, Months month, CancellationToken token = default)
		{
			ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.Month, month, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
			return new ReadStoredEnergyResponse(inputBuffer);
		}

		/// <summary>
		/// Read stored energy per phases for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="ReadStoredEnergyPerPhaseResponse"/>.</returns>
		public async Task<ReadStoredEnergyPerPhaseResponse> ReadStoredEnergyPerPhasesAsync(MeterRates meterRates, CancellationToken token = default)
        {
            ReadStoredEnergyRequest request = new ReadStoredEnergyRequest(Address, EnergyArrays.PerPhases, null, meterRates);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyPerPhaseResponse.Length, token);
            return new ReadStoredEnergyPerPhaseResponse(inputBuffer);
		}

		/// <summary>
		/// Read serial number and release date of meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="SerialNumberAndReleaseDateResponse"/>.</returns>
		public async Task<SerialNumberAndReleaseDateResponse> ReadSerialNumberAndReleaseDateAsync(CancellationToken token = default)
        {
			ReadSettingsRequest request = new ReadSettingsRequest(Address, MeterSettings.SerialNumberAndReleaseDate, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, SerialNumberAndReleaseDateResponse.Length, token);
            return new SerialNumberAndReleaseDateResponse(inputBuffer);
        }

		/// <summary>
		/// Read location of meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="LocationResponse"/>.</returns>
		public async Task<LocationResponse> ReadLocationAsync(CancellationToken token = default)
        {
            ReadSettingsRequest request = new ReadSettingsRequest(Address, MeterSettings.Location, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, LocationResponse.Length, token);
            return new LocationResponse(inputBuffer);
        }

		/// <summary>
		/// Read software version of meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="SoftwareVersionResponse"/>.</returns>
		public async Task<SoftwareVersionResponse> ReadSoftwareVersionAsync(CancellationToken token = default)
		{
            ReadSettingsRequest request = new ReadSettingsRequest(Address, MeterSettings.SoftwareVersion, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, SoftwareVersionResponse.Length, token);
            return new SoftwareVersionResponse(inputBuffer);
		}
	}
}
