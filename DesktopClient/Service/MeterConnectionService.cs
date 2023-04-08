using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Timers;

namespace DesktopClient.Service
{
    public partial class MeterConnectionService : ObservableObject
    {
        const string _connectedMessage = "Подключено";
        const string _notConnectedMessage = "Не подключено";
        const int interval = 240000;
        const int counterInterval = 1000;

        [ObservableProperty]
        private TimeSpan _timeLeft;

        private bool _isConnected;
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
                    Message = _connectedMessage;
                }
                else
                {
                    ConnectionTimer.Stop();
                    IndicationTimer.Stop();
                    Message = _notConnectedMessage;
                }
            }
        }

        [ObservableProperty]
        private string? _message;
        
        readonly Timer ConnectionTimer;
        readonly Timer IndicationTimer;

        public MeterConnectionService()
        {
            Message = _notConnectedMessage;

            ConnectionTimer = new Timer(interval);
            ConnectionTimer.Elapsed += (_, _) => IsConnected = false;

            TimeLeft = TimeSpan.FromMilliseconds(interval);
            IndicationTimer = new Timer(counterInterval);
            IndicationTimer.Elapsed += (_, _) => TimeLeft = TimeLeft.Add(TimeSpan.FromMilliseconds(-counterInterval));
        }
    }
}
