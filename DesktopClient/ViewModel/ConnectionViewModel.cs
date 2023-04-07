using System;
using System.IO.Ports;
using M230Protocol;
using MeterClient;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Threading.Tasks;
using DesktopClient.Service;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using DesktopClient.Helpers.Validation;
using System.Threading;

namespace DesktopClient.ViewModel
{
    partial class ConnectionViewModel : ObservableValidator
    {
        [ObservableProperty]
        private bool _IsError;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(1)]
        [Digits]
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

        public string[] SerialPortsNames { get; }
        public string? SelectedSerialPort { get; set; }

        public Meter Meter { get; set; }
        public ProgressService ProgressService { get; }
        public MeterConnectionService MeterConnectionService { get; }

        public ConnectionViewModel()
        {
            // Init access levels
            AccessLevelNames = new string[] { "Пользователь", "Администратор" };
            SelectedAccessLevelName = AccessLevelNames[0];
            _selectedAccessLevel = MeterAccessLevels.User;

            // Init serial ports
            SerialPortsNames = SerialPort.GetPortNames();
            if (SerialPortsNames.Length > 0)
                SelectedSerialPort = SerialPortsNames[0];

            // Init Meter
            Meter = App.Current.Services.GetService<Meter>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса Meter.");
            ProgressService = App.Current.Services.GetService<ProgressService>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса ProgressService.");
            MeterConnectionService = App.Current.Services.GetService<MeterConnectionService>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса MeterConnectionService.");
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
        private async Task OpenConnectionAsync(PasswordBox? passwordBox, CancellationToken token)
        {
            ProgressService.IsTaskRunning = true;
            try
            {
                if (Address == null || !byte.TryParse(Address, out byte address))
                {
                    MessageBox.Show("Неверно задан адрес счетчика.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                if (passwordBox == null)
                {
                    MessageBox.Show("Пароль не может быть null.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                if (SelectedSerialPort == null)
                {
                    MessageBox.Show("Не выбран Com-порт.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                Meter = new Meter(address, SelectedSerialPort);
                CommunicationState linkTest = await Meter.TestLinkAsync(token);
                if (linkTest != CommunicationState.OK)
                {
                    MessageBox.Show("Не удалось установить физическое соединение со счётчиком. Проверьте подключение.", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                CommunicationState authorizationState = await Meter.OpenConnectionAsync(_selectedAccessLevel, passwordBox.SecurePassword, token);
                if (authorizationState != CommunicationState.OK)
                {
                    MessageBox.Show("Не удалось выполнить авторизацию. Проверьте правильность ввода пароля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                MeterConnectionService.IsConnected = true;
            }
            catch (OperationCanceledException) {  }
            catch (TimeoutException)
            {
                MessageBox.Show("Превышено время ожидания запроса.", "Ошибка", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK);
            }
            finally
            {
                ProgressService.IsTaskRunning = false;
            }
        }

        [RelayCommand]
        private void CancelConnection()
        {
            OpenConnectionCommand.Cancel();
        }
    }
}
