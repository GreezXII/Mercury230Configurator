using System;
using System.IO.Ports;
using M230Protocol;
using MeterClient;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Threading.Tasks;
using M230Protocol.Frames.Responses;

namespace DesktopClient.ViewModel
{
    partial class ConnectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _address;

        [ObservableProperty]
        private string? _password;

        public string[] AccessLevelNames { get; }
        string? _selectedAccessLevelName;
        public string? SelectedAccessLevelName
        {
            get => _selectedAccessLevelName;
            set
            {
                SetProperty(ref _selectedAccessLevelName, value);
                SetMeterAccessLevel(ref _selectedAccessLevel, SelectedAccessLevelName);
            }
        }
        MeterAccessLevels _selectedAccessLevel;

        public string[] serialPortsNames { get; }
        public string? selectedSerialPort { get; set; }

        public Meter Meter { get; }

        public ConnectionViewModel()
        {
            // Init access levels
            AccessLevelNames = new string[] { "Пользователь", "Администратор" };
            SelectedAccessLevelName = AccessLevelNames[0];
            _selectedAccessLevel = MeterAccessLevels.User;

            // Init serial ports
            serialPortsNames = SerialPort.GetPortNames();
            if (serialPortsNames.Length > 0)
                selectedSerialPort = serialPortsNames[0];

            // Init Meter
            Meter = App.Current.Services.GetService<Meter>() ?? throw new ArgumentNullException("Не удалось создать экземпляр класса Meter.");

            // Init command
            OpenConnectionCommand = new AsyncRelayCommand<PasswordBox>(OpenConnection);
        }

        public IRelayCommand OpenConnectionCommand { get; }

        private static void SetMeterAccessLevel(ref MeterAccessLevels field, string? level)
        {
            switch (level)
            {
                case "Администратор":
                    field = MeterAccessLevels.Admin;
                    break;
                case "Пользователь":
                    field = MeterAccessLevels.User;
                    break;
                default:
                    throw new ArgumentException("Передан неизвестный уровень доступа.");
            }
        }

        private Task<CommunicationStateResponse> OpenConnection(PasswordBox? passwordBox)
        {
            ArgumentNullException.ThrowIfNull(passwordBox?.SecurePassword);
            ArgumentNullException.ThrowIfNull(Address);
            ArgumentNullException.ThrowIfNull(selectedSerialPort);

            return Meter.OpenConnectionAsync(_selectedAccessLevel, passwordBox.SecurePassword);
        }
    }
}
