using System.Windows.Controls;
using DesktopClient.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopClient.View
{
    /// <summary>
    /// Interaction logic for ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : UserControl
    {
        public ConnectionView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<ConnectionViewModel>();
        }
    }
}
