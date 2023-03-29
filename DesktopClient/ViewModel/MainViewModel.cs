using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopClient.ViewModel
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject? _connectionViewModel;
        [ObservableProperty]
        private ObservableObject? _aboutMeterViewModel;
        [ObservableProperty]
        private ObservableObject? _energyViewModel;
        [ObservableProperty]
        private ObservableObject? _journalsViewModel;
        
        [ObservableProperty]
        public ObservableObject? _selectedViewModel;

        public MainViewModel()
        {
            _connectionViewModel = App.Current.Services.GetService<ConnectionViewModel>();
            _aboutMeterViewModel = App.Current.Services.GetService<AboutMeterViewModel>();
            _journalsViewModel = App.Current.Services.GetService<JournalsViewModel>();
            _energyViewModel = App.Current.Services.GetService<EnergyViewModel>();
            SelectedViewModel = _connectionViewModel;
        }

        [RelayCommand]
        private void ChangeSelectedViewModel(ObservableObject? viewModel) 
            => SelectedViewModel = viewModel;

        [RelayCommand]
        private static void CloseApp() => App.Current.Shutdown();
    }
}
