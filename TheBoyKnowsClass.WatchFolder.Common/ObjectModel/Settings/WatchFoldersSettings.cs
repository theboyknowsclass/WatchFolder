using System.Configuration;
using TheBoyKnowsClass.Common.Desktop.Models.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    [ConfigurationCollection(typeof(WatchFolderSettings), AddItemName = "watchfolder")]
    public class WatchFoldersSettings : ConfigurationElementCollection<WatchFolderSettings>
    {

    }
}
