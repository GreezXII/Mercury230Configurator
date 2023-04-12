using DesktopClient.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace DesktopClient.View
{
    /// <summary>
    /// Interaction logic for JoudnalsView.xaml
    /// </summary>
    public partial class JoudnalsView : UserControl
    {
        public JoudnalsView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<JournalsViewModel>();
        }
    }
}
