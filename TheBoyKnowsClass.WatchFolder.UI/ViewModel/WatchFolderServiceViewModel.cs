using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using TheBoyKnowsClass.Common.UI.ViewModels;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.UI.ViewModel
{
    public class WatchFolderServiceViewModel : ViewModelBase
    {
        private readonly SettingsServiceClient _client;
        private WatchFolderSettingsViewModel _selectedWatchFolderSettings;

        public WatchFolderServiceViewModel(SettingsServiceClient client)
        {
            _client = client;
            _selectedWatchFolderSettings = null;
            AddWatchFolderCommand = new DelegateCommand(AddWatchFolder, CanAddWatchFolder);
            RemoveWatchFolderCommand = new DelegateCommand(RemoveWatchFolder, CanRemoveWatchFolder);
            EditWatchFolderCommand = new DelegateCommand(EditWatchFolder, CanEditWatchFolder);
        }

        public string Host
        {
            get { return _client == null ? null : _client.Host; }
        }

        public int? Port
        {
            get { return _client==null ? (int?)null : _client.Port; }
        }

        public ObservableCollection<WatchFolderSettingsViewModel> WatchFoldersSettings
        {
            get
            {
                if(_client == null)
                {
                    return new ObservableCollection<WatchFolderSettingsViewModel>();
                }

                var watchFolderSettingsViewModels = new ObservableCollection<WatchFolderSettingsViewModel>();

                WatchFoldersSettings watchFoldersSettings = _client.GetWatchFoldersSettings();

                if (watchFoldersSettings != null)
                {
                    foreach (Common.ObjectModel.Settings.WatchFolderSettings watchFolderSettings in watchFoldersSettings)
                    {
                        watchFolderSettingsViewModels.Add(new WatchFolderSettingsViewModel(watchFolderSettings));
                    }
                }

                return watchFolderSettingsViewModels;
            }
        }

        public WatchFolderSettingsViewModel SelectedWatchFolderSettings
        {
            get { return _selectedWatchFolderSettings; }
            set
            {
                _selectedWatchFolderSettings = value;
                RaisePropertyChanged("SelectedWatchFolderSettings");
            }
        }

        public bool IsWatchFolderSettingsSelected
        {
            get { return _selectedWatchFolderSettings != null; }
        }

        public bool HasChanges
        {
            get { return true; }
        }

        #region Commands

        public DelegateCommand AddWatchFolderCommand { get; set; }
        public DelegateCommand RemoveWatchFolderCommand { get; set; }
        public DelegateCommand EditWatchFolderCommand { get; set; }


        private void AddWatchFolder()
        {
            WatchFoldersSettings.Add(new WatchFolderSettingsViewModel(new Common.ObjectModel.Settings.WatchFolderSettings()){Name="<Enter Name>", FolderName = "<Enter Folder Name>"});
        }

        private bool CanAddWatchFolder()
        {
            return true;
        }

        private bool _isRemoving;

        private void RemoveWatchFolder()
        {
            _isRemoving = true;
            RemoveWatchFolderCommand.RaiseCanExecuteChanged();

            WatchFoldersSettings watchFoldersSettings = _client.GetWatchFoldersSettings();
            watchFoldersSettings.Remove(SelectedWatchFolderSettings.FolderName);
            //_client.SetWatchFoldersSettings(watchFoldersSettings);
            WatchFoldersSettings.Remove(SelectedWatchFolderSettings);

            _isRemoving = false;
            RemoveWatchFolderCommand.RaiseCanExecuteChanged();
        }

        private bool CanRemoveWatchFolder()
        {
            return IsWatchFolderSettingsSelected && !_isRemoving;
        }

        private void EditWatchFolder()
        {
            throw new System.NotImplementedException();
        }

        private bool CanEditWatchFolder()
        {
            return IsWatchFolderSettingsSelected;
        }

        #endregion
    }
}
