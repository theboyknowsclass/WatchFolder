using System;
using System.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    public class QueueStateItem : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("datetime", IsKey = true, IsRequired = false)]
        public DateTime? DateTime
        {
            get { return (DateTime)this["timerinterval"]; }
            set { this["timerinterval"] = value; }
        }
    }
}
