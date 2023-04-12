using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DesktopClient.Helpers.Types;
using DesktopClient.Service;
using M230Protocol;
using MeterClient;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopClient.ViewModel
{
    partial class EnergyViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedMonthString = string.Empty;
        public List<string> MonthsStrings { get; set; }
        public ObservableCollection<Energy> TotalEnergy { get; }
        public bool[] RadioButtonsStatus { get; }

        Meter Meter { get; }
        public MeterCommandService CommandService { get; }

        public EnergyViewModel()
        {
            RadioButtonsStatus = new bool[6];
            RadioButtonsStatus[0] = true;
            TotalEnergy = new ObservableCollection<Energy>
            {
                new Energy("Тариф 1"),
                new Energy("Тариф 2"),
                new Energy("Тариф 3"),
                new Energy("Сумма")
            };
            MonthsStrings = new List<string>()
            {
                "январь",
                "февраль",
                "март",
                "апрель",
                "май",
                "июнь",
                "июль",
                "август",
                "сентябрь",
                "октябрь",
                "ноябрь",
                "декабрь"
            };
            SelectedMonthString = MonthsStrings[0];

            Meter = App.Current.Services.GetService<Meter>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса Meter.");
            CommandService = App.Current.Services.GetService<MeterCommandService>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса MeterCommandService.");
        }

        [RelayCommand]
        private async Task ReadEnergyAsync(CancellationToken token)
        {
            var selectedRadioButton = Array.IndexOf(RadioButtonsStatus, true);
            switch(selectedRadioButton)
            {
                case 0:
                    await CommandService.RunCommand(async () 
                        => await RunEnergyReading(Meter.ReadStoredEnergyFromResetAsync, token));
                    break;
                case 1:
                    await CommandService.RunCommand(async () 
                        => await RunEnergyReading(Meter.ReadStoredEnergyCurrentYearAsync, token));
                    break;
                case 2:
                    await CommandService.RunCommand(async () 
                        => await RunEnergyReading(Meter.ReadStoredEnergyPastYearAsync, token));
                    break;
                case 3:
                    await CommandService.RunCommand(async () 
                        => await RunEnergyReading(Meter.ReadStoredEnergyPastYearAsync, token));
                    break;
                case 4:
                    await CommandService.RunCommand(async () 
                        =>
                    {
                        Months selectedMonth = StringToMonth(SelectedMonthString);
                        var t1 = await Meter.ReadStoredEnergyByMonthAsync(MeterRates.Tariff1, selectedMonth, token);
                        var t2 = await Meter.ReadStoredEnergyByMonthAsync(MeterRates.Tariff2, selectedMonth, token);
                        var t3 = await Meter.ReadStoredEnergyByMonthAsync(MeterRates.Tariff3, selectedMonth, token);
                        var sum = await Meter.ReadStoredEnergyByMonthAsync(MeterRates.Sum, selectedMonth, token);
                        SetEnergy(t1, t2, t3, sum);
                    });
                    break;
                case 5:
                    await CommandService.RunCommand(async () 
                        => await RunEnergyReading(Meter.ReadStoredEnergyCurrentDayAsync, token));
                    break;
                case 6:
                    await CommandService.RunCommand(async () 
                        => await RunEnergyReading(Meter.ReadStoredEnergyPastDayAsync, token));
                    break;
                default:
                    MessageBox.Show("Not implemented");
                    break;
            }
        }

        [RelayCommand]
        private void CancelReadEnergy() => ReadEnergyCommand.Cancel();

        private static Months StringToMonth(string selectedMonthString)
        {
            return selectedMonthString switch
            {
                "январь" => Months.January,
                "февраль" => Months.February,
                "март" => Months.March,
                "апрель" => Months.April,
                "май" => Months.May,
                "июнь" => Months.June,
                "июль" => Months.July,
                "август" => Months.August,
                "сентябрь" => Months.September,
                "октябрь" => Months.October,
                "ноябрь" => Months.November,
                "декабрь" => Months.December,
                _ => throw new ArgumentException("Wrong string name")
            };
        }

        private async Task RunEnergyReading(Func<MeterRates, CancellationToken, Task<(double, double, double, double)>> func, CancellationToken token)
        {
            var t1 = await func(MeterRates.Tariff1, token);
            var t2 = await func(MeterRates.Tariff2, token);
            var t3 = await func(MeterRates.Tariff3, token);
            var sum = await func(MeterRates.Sum, token);
            SetEnergy(t1, t2, t3, sum);
        }

        private void SetEnergy(params (double, double, double, double)[] tariffs)
        {
            TotalEnergy[0] = new Energy("Тариф 1", tariffs[0]);
            TotalEnergy[1] = new Energy("Тариф 2", tariffs[1]);
            TotalEnergy[2] = new Energy("Тариф 3", tariffs[2]);
            TotalEnergy[3] = new Energy("Сумма", tariffs[3]);
        }
    }
}
