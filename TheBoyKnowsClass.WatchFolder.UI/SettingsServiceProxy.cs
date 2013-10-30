using System.ServiceModel;
using System.ServiceModel.Channels;
using TheBoyKnowsClass.WatchFolder.Common.Interfaces;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.UI
{
    public class SettingsServiceProxy : ClientBase<ISettingsService>, ISettingsService
    {
        public SettingsServiceProxy(Binding binding, EndpointAddress endpointAddress)
            : base(binding, endpointAddress)
        { 
        }

        #region ISettingsService Members

        public int GetPort()
        {
            return Channel.GetPort();
        }

        public bool SetPort(int port)
        {
            return Channel.SetPort(port);
        }

        public WatchFoldersSettings GetWatchFoldersSettings()
        {
            return Channel.GetWatchFoldersSettings();
        }

        public bool SetWatchFoldersSettings(WatchFoldersSettings watchFoldersSettings)
        {
            return Channel.SetWatchFoldersSettings(watchFoldersSettings);
        }

        public WatchFolderSettings GetWatchFolderSettings(string folderName)
        {
            throw new System.NotImplementedException();
        }

        public bool SetWatchFolderSettings(WatchFolderSettings watchFolderSettings, string folderName)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
