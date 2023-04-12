using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Navigation;

namespace DesktopClient.Service
{
    public partial class MeterCommandService : ObservableObject
    {
        const string taskNotRunningMessage = "Приложение готово к работе";
        const string taskRunningMessage = "Выполнение...";
        const string connectedMessage = "Подключено";
        const string notConnectedMessage = "Не подключено";
        const int interval = 240000;
        const int counterInterval = 1000;

        [ObservableProperty]
        string? _commandStatusMessage;

        [ObservableProperty]
        string? _connectionStatusMessage;

        bool _isTaskRunning;
        public bool IsTaskRunning
        {
            get => _isTaskRunning;
            set
            {
                SetProperty(ref _isTaskRunning, value);
                CommandStatusMessage = value ? taskRunningMessage : taskNotRunningMessage;
                CanStartCommand = !IsTaskRunning && IsConnected;
            }
        }

        bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                SetProperty(ref _isConnected, value);
                if (value == true)
                {
                    ConnectionTimer.Start();
                    IndicationTimer.Start();
                    ConnectionStatusMessage = connectedMessage;
                }
                else
                {
                    ConnectionTimer.Stop();
                    IndicationTimer.Stop();
                    ConnectionStatusMessage = notConnectedMessage;
                }
                CanStartCommand = !IsTaskRunning && IsConnected;
            }
        }

        private bool _canStartCommand;
        public bool CanStartCommand 
        {
            get => _canStartCommand; 
            set => SetProperty(ref _canStartCommand, value);
        }

        [ObservableProperty]
        private TimeSpan _timeLeft;

        readonly Timer ConnectionTimer;
        readonly Timer IndicationTimer;

        public MeterCommandService()
        {
            CommandStatusMessage = taskNotRunningMessage;

            ConnectionStatusMessage = notConnectedMessage;

            ConnectionTimer = new Timer(interval);
            ConnectionTimer.Elapsed += (_, _) => IsConnected = false;

            TimeLeft = TimeSpan.FromMilliseconds(interval);
            IndicationTimer = new Timer(counterInterval);
            IndicationTimer.Elapsed += (_, _) => TimeLeft = TimeLeft.Add(TimeSpan.FromMilliseconds(-counterInterval));
            CanStartCommand = false;
        }

        public async Task RunCommand(Func<Task> function)
        {
            IsTaskRunning = true;
            try
            {
                await function();
            }
            catch (OperationCanceledException) { }
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
                IsTaskRunning = false;
            }
        }
    }
}
