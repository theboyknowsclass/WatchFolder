using System;
using System.Collections.ObjectModel;
using TheBoyKnowsClass.Common.UI.ViewModels;
using TheBoyKnowsClass.Schedules.Common.UI.ViewModels;

namespace TheBoyKnowsClass.WatchFolder.UI.ViewModel
{
    public class WatchFolderSettingsViewModel : ViewModelBase
    {
        private readonly Common.ObjectModel.Settings.WatchFolderSettings _watchFolderSettings;
        private ScheduleViewModel _schedule;
        private ObservableCollection<TaskEngineSettingsViewModel> _taskEnginesSettingsViewModels;

        public WatchFolderSettingsViewModel()
        {
            _watchFolderSettings = new Common.ObjectModel.Settings.WatchFolderSettings();
            _schedule = new ScheduleViewModel(_watchFolderSettings.ScheduleSettings.GetSchedule());
            _taskEnginesSettingsViewModels = new ObservableCollection<TaskEngineSettingsViewModel>();
        }

        public WatchFolderSettingsViewModel(Common.ObjectModel.Settings.WatchFolderSettings watchFolderSettings)
        {
            _watchFolderSettings = watchFolderSettings;
            _schedule = new ScheduleViewModel(_watchFolderSettings.ScheduleSettings.GetSchedule());
            _taskEnginesSettingsViewModels = new ObservableCollection<TaskEngineSettingsViewModel>();
        }

        public string Name
        {
            get { return _watchFolderSettings.Name; }
            set
            {
                _watchFolderSettings.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string FolderName
        {
            get { return _watchFolderSettings.FolderName; }
            set 
            { 
                _watchFolderSettings.FolderName = value; 
                RaisePropertyChanged("FolderName");
            }
        }

        public TimeSpan TimerInterval
        {
            get { return _watchFolderSettings.TimerInterval; }
            set
            {
                _watchFolderSettings.TimerInterval = value;
                RaisePropertyChanged("TimerInterval");
            }
        }

        public TimeSpan InitialTaskDelay
        {
            get { return _watchFolderSettings.InitialTaskDelay; }
            set
            {
                _watchFolderSettings.InitialTaskDelay = value;
                RaisePropertyChanged("InitialTaskDelay");
            }
        }

        public ScheduleViewModel Schedule 
        { 
            get { return _schedule; }
            set 
            { 
                _schedule = value;
                RaisePropertyChanged("ScheduleViewModel");
            }
        }

        public ObservableCollection<TaskEngineSettingsViewModel> TaskEngineSettings
        {
            get { return _taskEnginesSettingsViewModels; }
            set 
            { 
                    _taskEnginesSettingsViewModels = value;
                    RaisePropertyChanged("TaskEngineSettings");
            }
        }
    }
}
