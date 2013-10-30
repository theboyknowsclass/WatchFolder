using System.Configuration;
using TheBoyKnowsClass.Common.Desktop.Enumerations;
using TheBoyKnowsClass.Common.Desktop.Operations.Log;
using TheBoyKnowsClass.Common.Desktop.Operations.Settings;
using TheBoyKnowsClass.Common.Enumerations;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;
using TheBoyKnowsClass.WatchFolder.Service.Properties;

namespace TheBoyKnowsClass.WatchFolder.Service
{
    public class WatchFolderSettingsHelper : SettingsHelper<WatchFolderServiceConfigurationSection>
    {
        public WatchFolderSettingsHelper()
            : base(Properties.Settings.Default, new LogHelper(Resources.EventLogName, Resources.EventLog))
        {
        }

        public WatchFolderSettingsHelper(LogHelper logHelper)
            : base(Properties.Settings.Default, logHelper)
        {
        }

        public override WatchFolderServiceConfigurationSection Settings
        {
            get
            {
                if (UserConfigSettings.WatchFoldersSettings.Count == 0 && AppConfigSettings.WatchFoldersSettings.Count > 0 && !CanSaveAppSettings)
                {
                    foreach (WatchFolderSettings watchFolderSettings in AppConfigSettings.WatchFoldersSettings)
                    {
                        UserConfigSettings.WatchFoldersSettings.Add(watchFolderSettings);
                    }
                }

                return base.Settings;
            }
        }

        protected override WatchFolderServiceConfigurationSection AppConfigSettings
        {
            get
            {
                WatchFolderServiceConfigurationSection watchFolderServiceConfigurationSection;

                if (Configuration.Sections["watchfoldersection"] == null)
                {
                    if (CanSaveAppSettings)
                    {
                        watchFolderServiceConfigurationSection = new WatchFolderServiceConfigurationSection();
                        Configuration.Sections.Add("watchfoldersection", watchFolderServiceConfigurationSection);
                        Configuration.Save(ConfigurationSaveMode.Modified);
                        LogHelper.WriteEntry(string.Format("Created custom section in application configuration file : {0}", Configuration.FilePath), MessageType.Information);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    watchFolderServiceConfigurationSection = (WatchFolderServiceConfigurationSection)Configuration.Sections["watchfoldersection"];
                }

                return watchFolderServiceConfigurationSection;
            }
            set
            {
                ((WatchFolderServiceConfigurationSection)Configuration.Sections["watchfoldersection"]).Port = value.Port;
                ((WatchFolderServiceConfigurationSection) Configuration.Sections["watchfoldersection"]).WatchFoldersSettings = value.WatchFoldersSettings;
            }
        }

        protected override WatchFolderServiceConfigurationSection UserConfigSettings
        {
            get 
            {
                return Properties.Settings.Default.ServiceSettings ?? (Properties.Settings.Default.ServiceSettings = new WatchFolderServiceConfigurationSection());
            }
            set
            {
                Properties.Settings.Default.ServiceSettings = value;
            }
        }
    }
}
