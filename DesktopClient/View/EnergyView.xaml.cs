using System.Windows.Controls;
using DesktopClient.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopClient.View
{
    /// <summary>
    /// Interaction logic for EnergyView.xaml
    /// </summary>
    public partial class EnergyView : UserControl
    {
        public EnergyView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<EnergyViewModel>();
        }
    }
}
