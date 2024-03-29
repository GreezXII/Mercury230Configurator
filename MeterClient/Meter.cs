﻿using M230Protocol;
using M230Protocol.Exceptions;
using M230Protocol.Frames.Requests;
using M230Protocol.Frames.Responses;
using System.Globalization;
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
            var request = new CloseConnectionRequest(Address);
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
		/// <returns>A task that returns tuple with start time and end time.</returns>
		/// <exception cref="InvalidRecordNumberException"></exception>
		public async Task<(DateTime, DateTime)> ReadJournalRecordAsync(MeterJournals journal, byte recordNumber, CancellationToken token = default)
        {
            if (recordNumber < 0 || recordNumber > 9)
                throw new InvalidRecordNumberException();
            var readJournalRecordRequest = new ReadJournalRecordRequest(Address, journal, recordNumber);
            byte[] outputBuffer = readJournalRecordRequest.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadJournalResponse.Length, token);
            var result = new ReadJournalResponse(inputBuffer);
            return (result.StartTime, result.EndTime);
        }

		/// <summary>
		/// Read all records from specified enet journal.
		/// </summary>
		/// <param name="journal">Type of journal.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns a list of tuples consists of start time and end time.</returns>
		public async Task<List<(DateTime, DateTime)>> ReadAllJournalRecordsAsync(MeterJournals journal, CancellationToken token = default)
        {
            var result = new List<(DateTime, DateTime)>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(await ReadJournalRecordAsync(journal, (byte)i, token));
            }
            return result.OrderBy(r => r.Item1).ToList();
        }

		/// <summary>
		/// Read stored energy from reset for specified meter rate.
		/// </summary>
		/// <param name="meterRates">Defines specific rate or their sum.</param>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns tuple with four doubles that represent energy values.</returns>
		public async Task<(double, double, double, double)> ReadStoredEnergyFromResetAsync(MeterRates meterRates, CancellationToken token = default)
        {
            var request = new ReadStoredEnergyRequest(Address, EnergyArrays.FromReset, 0x0, meterRates);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
            var response = new ReadStoredEnergyResponse(inputBuffer);
			return (response.ActivePositive, response.ActiveNegative, response.ReactivePositive, response.ReactiveNegative);
		}

        /// <summary>
        /// Read stored energy in current year for specified meter rate.
        /// </summary>
        /// <param name="meterRates">Defines specific rate or their sum.</param>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns tuple with four doubles that represent energy values.</returns>
        public async Task<(double, double, double, double)> ReadStoredEnergyCurrentYearAsync(MeterRates meterRates, CancellationToken token = default)
		{
			var request = new ReadStoredEnergyRequest(Address, EnergyArrays.CurrentYear, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
			var response = new ReadStoredEnergyResponse(inputBuffer);
            return (response.ActivePositive, response.ActiveNegative, response.ReactivePositive, response.ReactiveNegative);
        }

        /// <summary>
        /// Read stored energy in past year for specified meter rate.
        /// </summary>
        /// <param name="meterRates">Defines specific rate or their sum.</param>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns tuple with four doubles that represent energy values.</returns>
        public async Task<(double, double, double, double)> ReadStoredEnergyPastYearAsync(MeterRates meterRates, CancellationToken token = default)
		{
			var request = new ReadStoredEnergyRequest(Address, EnergyArrays.PastYear, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
            var response = new ReadStoredEnergyResponse(inputBuffer);
            return (response.ActivePositive, response.ActiveNegative, response.ReactivePositive, response.ReactiveNegative);
        }

        /// <summary>
        /// Read stored energy in current day for specified meter rate.
        /// </summary>
        /// <param name="meterRates">Defines specific rate or their sum.</param>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns tuple with four doubles that represent energy values.</returns>
        public async Task<(double, double, double, double)> ReadStoredEnergyCurrentDayAsync(MeterRates meterRates, CancellationToken token = default)
		{
			var request = new ReadStoredEnergyRequest(Address, EnergyArrays.CurrentDay, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
            var response = new ReadStoredEnergyResponse(inputBuffer);
            return (response.ActivePositive, response.ActiveNegative, response.ReactivePositive, response.ReactiveNegative);
        }

        /// <summary>
        /// Read stored energy in past day for specified meter rate.
        /// </summary>
        /// <param name="meterRates">Defines specific rate or their sum.</param>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns tuple with four doubles that represent energy values.</returns>
        public async Task<(double, double, double, double)> ReadStoredEnergyPastDayAsync(MeterRates meterRates, CancellationToken token = default)
		{
			var request = new ReadStoredEnergyRequest(Address, EnergyArrays.PastDay, null, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
            var response = new ReadStoredEnergyResponse(inputBuffer);
            return (response.ActivePositive, response.ActiveNegative, response.ReactivePositive, response.ReactiveNegative);
        }

        /// <summary>
        /// Read stored energy by month for specified meter rate.
        /// </summary>
        /// <param name="meterRates">Defines specific rate or their sum.</param>
        /// <param name="month">The month in which you need to get the accumulated energy.</param>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns tuple with four doubles that represent energy values.</returns>
        public async Task<(double, double, double, double)> ReadStoredEnergyByMonthAsync(MeterRates meterRates, Months month, CancellationToken token = default)
		{
			var request = new ReadStoredEnergyRequest(Address, EnergyArrays.Month, month, meterRates);
			byte[] outputBuffer = request.Create();
			byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyResponse.Length, token);
            var response = new ReadStoredEnergyResponse(inputBuffer);
            return (response.ActivePositive, response.ActiveNegative, response.ReactivePositive, response.ReactiveNegative);
        }

        /// <summary>
        /// Read stored energy per phases for specified meter rate.
        /// </summary>
        /// <param name="meterRates">Defines specific rate or their sum.</param>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns tuple with three doubles that represent energy values per phase.</returns>
        public async Task<(double, double, double)> ReadStoredEnergyPerPhasesAsync(MeterRates meterRates, CancellationToken token = default)
        {
            var request = new ReadStoredEnergyRequest(Address, EnergyArrays.PerPhases, null, meterRates);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, ReadStoredEnergyPerPhaseResponse.Length, token);
            var response = new ReadStoredEnergyPerPhaseResponse(inputBuffer);
            return (response.Phase1, response.Phase2, response.Phase3);
		}

        /// <summary>
        /// Read serial number and release date of meter.
        /// </summary>
        /// <param name="token">Propagates notification that operation should be canceled.</param>
        /// <returns>A task that returns <see cref="(int serialNumber, DateTime releaseDate)"/>.</returns>
        public async Task<(int, DateTime)> ReadSerialNumberAndReleaseDateAsync(CancellationToken token = default)
        {
			var request = new ReadSettingsRequest(Address, MeterSettings.SerialNumberAndReleaseDate, Array.Empty<byte>());
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, SerialNumberAndReleaseDateResponse.Length, token);
            var response = new SerialNumberAndReleaseDateResponse(inputBuffer);
			return (response.SerialNumber, response.ReleaseDate);
        }

		/// <summary>
		/// Read location of meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns location string.</returns>
		public async Task<string> ReadLocationAsync(CancellationToken token = default)
        {
            var request = new ReadSettingsRequest(Address, MeterSettings.Location, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, LocationResponse.Length, token);
            var response = new LocationResponse(inputBuffer);
			return response.Location;
        }

		/// <summary>
		/// Read software version of meter.
		/// </summary>
		/// <param name="token">Propagates notification that operation should be canceled.</param>
		/// <returns>A task that returns <see cref="string"/> with software version.</returns>
		public async Task<String> ReadSoftwareVersionAsync(CancellationToken token = default)
		{
            var request = new ReadSettingsRequest(Address, MeterSettings.SoftwareVersion, new byte[0]);
            byte[] outputBuffer = request.Create();
            byte[] inputBuffer = await SerialPort.GetResponseAsync(outputBuffer, SoftwareVersionResponse.Length, token);
            var response = new SoftwareVersionResponse(inputBuffer);
			return response.SoftwareVersion;
		}
	}
}
