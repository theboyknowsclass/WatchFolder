using TheBoyKnowsClass.Common.Desktop.Operations.Log;
using TheBoyKnowsClass.WatchFolder.Common.Interfaces;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.Service
{
    public class SettingsService : ISettingsService
    {
        private readonly LogHelper _logHelper;
        private readonly WatchFolderSettingsHelper _watchFolderSettingsHelper;

        public SettingsService()
        {
            _logHelper = new LogHelper(Properties.Resources.EventLog, Properties.Resources.EventLogName);
            _watchFolderSettingsHelper = new WatchFolderSettingsHelper(_logHelper);
        }

        public SettingsService(LogHelper logHelper, WatchFolderSettingsHelper watchFolderSettingsHelper)
        {
            _logHelper = logHelper;
            _watchFolderSettingsHelper = watchFolderSettingsHelper;
        }

        public int GetPort()
        {
            return _watchFolderSettingsHelper.Settings.Port;
        }

        public bool SetPort(int port)
        {
            _watchFolderSettingsHelper.Settings.Port = port;
            return true;
        }

        public WatchFoldersSettings GetWatchFoldersSettings()
        {
            return _watchFolderSettingsHelper.Settings.WatchFoldersSettings;
        }

        public bool SetWatchFoldersSettings(WatchFoldersSettings watchFoldersSettings)
        {
            _watchFolderSettingsHelper.Settings.WatchFoldersSettings = watchFoldersSettings;
            return true;
        }

        public WatchFolderSettings GetWatchFolderSettings(string folderName)
        {
            WatchFoldersSettings watchFoldersSettings = GetWatchFoldersSettings();

            try
            {
                if (watchFoldersSettings.Contains(folderName))
                {
                    return watchFoldersSettings[folderName];
                }
                return null;
            }
            catch
            {
                return null;
            }

        }

        public bool SetWatchFolderSettings(WatchFolderSettings watchFolderSettings, string folderName)
        {
            try
            {
                WatchFoldersSettings watchFoldersSettings = GetWatchFoldersSettings();

                if (watchFoldersSettings.Contains(folderName))
                {
                    watchFoldersSettings[folderName] = watchFolderSettings;
                }
                else
                {
                    watchFoldersSettings.Add(watchFolderSettings);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
