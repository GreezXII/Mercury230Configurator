using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DesktopClient.ViewModel;
using MeterClient;
using DesktopClient.Service;
using System.IO;

namespace DesktopClient
{
    public partial class App : Application
    {
        public new static App Current => (App) Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        private static SettingsService ConfigureSettingsService()
        {
            if (File.Exists(SettingsService.Filename))
                return SettingsService.Load();
            else
                return new SettingsService();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            SettingsService settings = ConfigureSettingsService();
            services.AddSingleton(settings);
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ConnectionViewModel>();
            services.AddSingleton<AboutMeterViewModel>();
            services.AddSingleton<JournalsViewModel>();
            services.AddSingleton<EnergyViewModel>();
            services.AddSingleton<Meter>();
            services.AddSingleton<MeterCommandService>();

            return services.BuildServiceProvider();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var connectionViewModel = Current.Services.GetService<ConnectionViewModel>() ?? throw new NullReferenceException("Не удалось создать ConnectionViewModel.");
            var settings = Current.Services.GetService<SettingsService>() ?? throw new NullReferenceException("Не удалось создать SettingsService.");
            settings.Address = connectionViewModel.Address;
            settings.AccessLevel = connectionViewModel.SelectedAccessLevelName;
            settings.ComPortName = connectionViewModel.SelectedSerialPort;
            settings.Timeout = connectionViewModel.ConnectionTimeout;
            SettingsService.Save(settings);
            base.OnExit(e);
        }
    }
}
