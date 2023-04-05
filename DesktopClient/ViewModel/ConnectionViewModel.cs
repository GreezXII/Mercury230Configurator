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
using DesktopClient.Service;
using System.Windows;
using System.ComponentModel;

namespace DesktopClient.ViewModel
{
    partial class ConnectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _address;

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
        ProgressService? ProgressService;

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
            Meter = App.Current.Services.GetService<Meter>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса Meter.");
            ProgressService = App.Current.Services.GetService<ProgressService>();
        }

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

        [RelayCommand]
        private async Task OpenConnection(PasswordBox? passwordBox)
        {
            if (Address == null)
            {
                MessageBox.Show("Адрес счетчика не может быть пустым.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (passwordBox?.SecurePassword == null)
            {
                MessageBox.Show("Пароль не может быть null.", "Ошибка", MessageBoxButton.OK);
                return;
            }
            if (selectedSerialPort == null)
            {
                MessageBox.Show("Не выбран Com-порт.", "Ошибка", MessageBoxButton.OK);
                return;
            }

            ProgressService.IsTaskRunning = true;
            CommunicationStateResponse response = await Meter.OpenConnectionAsync(_selectedAccessLevel, passwordBox.SecurePassword);
            if (response.State != CommunicationState.OK)
                MessageBox.Show("Not ok");
            ProgressService.IsTaskRunning = false;
        }
    }
}
