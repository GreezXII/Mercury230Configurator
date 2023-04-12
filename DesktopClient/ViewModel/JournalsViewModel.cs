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
    partial class JournalsViewModel : ObservableObject
    {
        public bool[] RadioButtonsStatus { get; }
        public ObservableCollection<JournalRecord> JournalRecords { get; }

        Meter Meter { get; }
        public MeterCommandService CommandService { get; }

        public JournalsViewModel()
        {
            RadioButtonsStatus = new bool[8];
            RadioButtonsStatus[0] = true;

            JournalRecords = new ObservableCollection<JournalRecord>();
            for (int i = 0; i < 10; i++)
                JournalRecords.Add(new JournalRecord());

            Meter = App.Current.Services.GetService<Meter>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса Meter.");
            CommandService = App.Current.Services.GetService<MeterCommandService>() ?? throw new NullReferenceException("Не удалось создать экземпляр класса MeterCommandService.");
        }

        [RelayCommand]
        private async Task ReadJournalAsync(CancellationToken token)
        {
            await CommandService.RunCommandAsync(async () =>
            {
                var selectedJournal = Array.IndexOf(RadioButtonsStatus, true);
                switch (selectedJournal)
                {
                    case 0:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.OnOff, token));
                        break;
                    case 1:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.OpeningClosing, token));
                        break;
                    case 2:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase1OnOff, token));
                        break;
                    case 3:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase2OnOff, token));
                        break;
                    case 4:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase3OnOff, token));
                        break;
                    case 5:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase1CurrentOnOff, token));
                        break;
                    case 6:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase2CurrentOnOff, token));
                        break;
                    case 7:
                        SetJournalRecords(await Meter.ReadAllJournalRecordsAsync(MeterJournals.Phase3CurrentOnOff, token));
                        break;
                    default:
                        MessageBox.Show("Not implemented");
                        break;
                }
            });
        }

        [RelayCommand]
        private void CancelReadJournal() => ReadJournalCommand.Cancel();

        private void SetJournalRecords(List<(DateTime, DateTime)> journal)
        {
            for (int i = 0; i < 10; i++)
                JournalRecords[i] = new JournalRecord(journal[i].Item1, journal[i].Item2);
        }
    }
}
