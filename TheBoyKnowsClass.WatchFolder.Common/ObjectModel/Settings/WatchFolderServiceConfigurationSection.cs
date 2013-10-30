using System.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    public class WatchFolderServiceConfigurationSection : ConfigurationSection
    {
        public WatchFolderServiceConfigurationSection()
        {
            WatchFoldersSettings = new WatchFoldersSettings();
            Port = 0;
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("watchfolders", IsDefaultCollection = false)]
        public WatchFoldersSettings WatchFoldersSettings
        {
            get { return (WatchFoldersSettings) this["watchfolders"]; }
            set { this["watchfolders"] = value; }
        }
    }
}
