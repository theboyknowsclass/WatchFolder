using System;
using TheBoyKnowsClass.Common.UI.ViewModels;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.UI.ViewModel
{
    public class TaskEngineSettingsViewModel : ViewModelBase
    {
        private readonly TaskEngineSettings _taskEngineSettings;

        public TaskEngineSettingsViewModel(TaskEngineSettings taskEngineSettings)
        {
            _taskEngineSettings = taskEngineSettings;
        }

        public string Name
        {
            get { return _taskEngineSettings.AssemblyName; }
        }

        public string TypeName
        {
            get { return _taskEngineSettings.TypeName; }
        }

        public TimeSpan TaskDelay
        {
            get { return _taskEngineSettings.TaskDelay; }
            set
            {
                _taskEngineSettings.TaskDelay = value;
                RaisePropertyChanged("TaskDelay");
            }
        }

        public int SortOrder
        {
            get { return _taskEngineSettings.SortOrder; }
            set
            {
                _taskEngineSettings.SortOrder = value;
                RaisePropertyChanged("SortOrder");
            }
        }

    }
}
