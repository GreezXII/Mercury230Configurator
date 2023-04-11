using DesktopClient.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace DesktopClient.View
{
    /// <summary>
    /// Interaction logic for AboutMeterView.xaml
    /// </summary>
    public partial class AboutMeterView : UserControl
    {
        public AboutMeterView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<AboutMeterViewModel>();
        }
    }
}
