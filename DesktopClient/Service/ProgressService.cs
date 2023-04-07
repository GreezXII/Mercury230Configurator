﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopClient.Service
{
    public partial class ProgressService : ObservableObject
    {
        const string taskNotRunningMessage = "Приложение готово к работе";
        const string taskRunningMessage = "Выполнение...";

        bool _isTaskRunning;
        public bool IsTaskRunning
        {
            get => _isTaskRunning;
            set
            {
                SetProperty(ref _isTaskRunning, value);
                Message = value ? taskRunningMessage : taskNotRunningMessage;
            }
        }

        [ObservableProperty]
        string? _message;

        public ProgressService() => Message = taskNotRunningMessage;
    }
}
