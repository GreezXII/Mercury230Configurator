using DesktopClient.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace DesktopClient.ViewModel
{
    class MainViewModel : BaseViewModel
    {
		private BaseViewModel _selectedViewModel;
		public BaseViewModel SelectedViewModel
		{
			get { return _selectedViewModel; }
			set { _selectedViewModel = value; RaisePropertyChanged(); }
		}

		private ConnectionViewModel _connectionViewModel;
		public ConnectionViewModel ConnectionViewModel
		{
			get { return _connectionViewModel; }
			set { _connectionViewModel = value; RaisePropertyChanged(); }
		}

		private AboutMeterViewModel _aboutMeterViewModel;
		public AboutMeterViewModel AboutMeterViewModel
		{
			get { return _aboutMeterViewModel; }
			set { _aboutMeterViewModel = value; RaisePropertyChanged(); }
		}

		private EnergyViewModel _energyViewModel;
		public EnergyViewModel EnergyViewModel
		{
			get { return _energyViewModel; }
			set { _energyViewModel = value; RaisePropertyChanged(); }
		}

		private JournalsViewModel _journalsViewModel;
		public JournalsViewModel JournalsViewModel
		{
			get { return _journalsViewModel; }
			set { _journalsViewModel = value; RaisePropertyChanged(); }
		}

		public DelegateCommand SelectViewModel { get; private set; }

		public MainViewModel()
		{
            SelectViewModel = new DelegateCommand(ExecutedSelectViewModel, CanExecuteSelectedViewModel);
			ConnectionViewModel = new ConnectionViewModel();
			AboutMeterViewModel = new AboutMeterViewModel();
			EnergyViewModel = new EnergyViewModel();
			JournalsViewModel = new JournalsViewModel();
			SelectedViewModel = ConnectionViewModel;
		}

        public void ExecutedSelectViewModel(object? newViewModel)
        {
			ArgumentNullException.ThrowIfNull(newViewModel);
			SelectedViewModel = (BaseViewModel)newViewModel;
        }

        public bool CanExecuteSelectedViewModel(object? parameter) => true;
    }
}
