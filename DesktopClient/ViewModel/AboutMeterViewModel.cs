﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopClient.Service;
using MeterClient;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

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

        [ObservableProperty]
        private string? _location;

        public Meter Meter { get; }
        public MeterCommandService CommandService { get; }

        public AboutMeterViewModel()
        {
            Meter = App.Current.Services.GetService<Meter>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса Meter.");
            CommandService = App.Current.Services.GetService<MeterCommandService>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса ProgressService.");
        }

        [RelayCommand]
        private async Task ReadInfoAsync(CancellationToken token)
        {
            await CommandService.RunCommandAsync(async () =>
            {
                (SerialNumber, ReleaseDate) = await Meter.ReadSerialNumberAndReleaseDateAsync(token);
                SoftwareVersion = await Meter.ReadSoftwareVersionAsync(token);
            });
        }

        [RelayCommand]
        private void CancelReadInfo() => ReadInfoCommand.Cancel();

        [RelayCommand]
        private async Task ReadLocationAsync(CancellationToken token) => await CommandService.RunCommandAsync(async () => Location = await Meter.ReadLocationAsync(token));

        [RelayCommand]
        private void CancelReadLocation() => ReadLocationCommand.Cancel();
    }
}
