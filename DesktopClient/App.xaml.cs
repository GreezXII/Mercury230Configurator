using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using DesktopClient.ViewModel;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App) Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ConnectionViewModel>();
            services.AddSingleton<AboutMeterViewModel>();
            services.AddSingleton<JournalsViewModel>();
            services.AddSingleton<EnergyViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
