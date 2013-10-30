using System.ServiceModel;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.Common.Interfaces
{
    [ServiceContract(Namespace = "http://theboyknowsclass/watchfolder")]
    public interface ISettingsService
    {
        [OperationContract]
        int GetPort();

        [OperationContract]
        bool SetPort(int port);

        [OperationContract]
        WatchFoldersSettings GetWatchFoldersSettings();

        [OperationContract]
        bool SetWatchFoldersSettings(WatchFoldersSettings watchFoldersSettings);

        [OperationContract]
        WatchFolderSettings GetWatchFolderSettings(string folderName);

        [OperationContract]
        bool SetWatchFolderSettings(WatchFolderSettings watchFolderSettings, string folderName);
    }
}
