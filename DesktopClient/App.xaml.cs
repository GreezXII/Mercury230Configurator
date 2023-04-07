using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DesktopClient.ViewModel;
using MeterClient;
using DesktopClient.Service;

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

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ConnectionViewModel>();
            services.AddSingleton<AboutMeterViewModel>();
            services.AddSingleton<JournalsViewModel>();
            services.AddSingleton<EnergyViewModel>();
            services.AddSingleton<Meter>();
            services.AddSingleton<ProgressService>();
            services.AddSingleton<MeterConnectionService>();

            return services.BuildServiceProvider();
        }
    }
}
