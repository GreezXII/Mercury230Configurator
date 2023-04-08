using CommunityToolkit.Mvvm.ComponentModel;
using System.Timers;

namespace DesktopClient.Service
{
    public partial class MeterConnectionService : ObservableObject
    {
        const string _connectedMessage = "Подключено";
        const string _notConnectedMessage = "Не подключено";

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set 
            { 
                SetProperty(ref _isConnected, value);
                if (value == true)
                {
                    Timer.Start();
                    Message = _connectedMessage;
                }
                else
                {
                    Timer.Stop();
                    Message = _notConnectedMessage;
                }
            }
        }

        [ObservableProperty]
        private string? _message;
        
        readonly Timer Timer;

        public MeterConnectionService()
        {
            Message = _notConnectedMessage;
            Timer = new Timer(5000);
            Timer.Elapsed += (_, _) => IsConnected = false;
        }
    }
}
