using System;
using System.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    public class ScheduleActivitySetting : ConfigurationElement
    {
        [ConfigurationProperty("id", IsKey = true, IsRequired = true)]
        public int ID
        {
            get { return (int)this["id"]; }
            set { this["id"] = value; }
        }

        [ConfigurationProperty("dayofweek", IsRequired = true)]
        public DayOfWeek DayOfWeek
        {
            get { return (DayOfWeek)this["dayofweek"]; }
            set { this["dayofweek"] = value; }
        }

        [ConfigurationProperty("starttime", IsRequired = true)]
        public TimeSpan StartTime
        {
            get { return (TimeSpan)this["starttime"]; }
            set { this["starttime"] = value; }
        }

        [ConfigurationProperty("endtime", IsRequired = true)]
        public TimeSpan EndTime
        {
            get { return (TimeSpan)this["endtime"]; }
            set { this["endtime"] = value; }
        }
    }
}
