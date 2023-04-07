using CommunityToolkit.Mvvm.ComponentModel;

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
                Message = value ? _connectedMessage : _notConnectedMessage;
            }
        }

        [ObservableProperty]
        private string? _message;

        public MeterConnectionService() => Message = _notConnectedMessage;
    }
}
