using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopClient.Service;
using MeterClient;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopClient.ViewModel
{
    partial class AboutMeterViewModel : ObservableObject
    {
        [ObservableProperty]
        private int? _serialNumber;

        [ObservableProperty]
        private DateTime? _releaseDate;

        [ObservableProperty]
        private string? _softwareVersion;

        public Meter Meter { get; }
        public MeterCommandService CommandService { get; }

        public AboutMeterViewModel()
        {
            Meter = App.Current.Services.GetService<Meter>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса Meter.");
            CommandService = App.Current.Services.GetService<MeterCommandService>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса ProgressService.");
        }

        [RelayCommand]
        private async Task ReadInfoAsync()
        {
            await CommandService.RunCommand(async () =>
            {
                (SerialNumber, ReleaseDate) = await Meter.ReadSerialNumberAndReleaseDateAsync();
                SoftwareVersion = await Meter.ReadSoftwareVersionAsync();
            });
        }
    }
}
