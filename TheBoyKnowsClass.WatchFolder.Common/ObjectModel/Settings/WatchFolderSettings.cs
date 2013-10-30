using System;
using System.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    public class WatchFolderSettings : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("foldername", IsKey = true, IsRequired = true)]
        public string FolderName
        {
            get { return (string)this["foldername"]; }
            set { this["foldername"] = value; }
        }

        [ConfigurationProperty("timerinterval", IsRequired = false)]
        public TimeSpan TimerInterval
        {
            get { return (TimeSpan)this["timerinterval"]; }
            set { this["timerinterval"] = value; }
        }

        [ConfigurationProperty("initialtaskdelay", IsRequired = false)]
        public TimeSpan InitialTaskDelay
        {
            get { return (TimeSpan)this["initialtaskdelay"]; }
            set { this["initialtaskdelay"] = value; }
        }

        [ConfigurationProperty("schedule", IsRequired = false, IsDefaultCollection = false)]
        public ScheduleSettings ScheduleSettings
        {
            get { return (ScheduleSettings)this["schedule"]; }
            set { this["schedule"] = value; }
        }

        [ConfigurationProperty("taskengines", IsRequired = true, IsDefaultCollection = false)]
        //[ConfigurationCollection(typeof(Plugin), AddItemName = "plugin")]
        public TaskEnginesSettings TaskEnginesSettings
        {
            get { return (TaskEnginesSettings)this["taskengines"]; }
            set { this["taskengines"] = value; }
        }

        [ConfigurationProperty("queue", IsRequired = true, IsDefaultCollection = false)]
        //[ConfigurationCollection(typeof(Plugin), AddItemName = "plugin")]
        public QueueState Queue
        {
            get { return (QueueState)this["queue"]; }
            set { this["queue"] = value; }
        }
    }
}
