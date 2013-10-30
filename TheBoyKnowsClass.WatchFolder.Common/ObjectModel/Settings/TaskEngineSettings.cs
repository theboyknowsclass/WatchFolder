using System;
using System.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    public class TaskEngineSettings : ConfigurationElement
    {
        [ConfigurationProperty("sortorder", IsKey = true, IsRequired = true)]
        public int SortOrder
        {
            get { return (int)this["sortorder"]; }
            set { this["sortorder"] = value; }
        }

        [ConfigurationProperty("typename", IsKey = false, IsRequired = true)]
        public string TypeName
        {
            get { return (string)this["typename"]; }
            set { this["typename"] = value; }
        }

        [ConfigurationProperty("assemblyname", IsKey = false, IsRequired = true)]
        public string AssemblyName
        {
            get { return (string)this["assemblyname"]; }
            set { this["assemblyname"] = value; }
        }

        [ConfigurationProperty("taskdelay", IsKey = true, IsRequired = false)]
        public TimeSpan TaskDelay
        {
            get { return (TimeSpan)this["taskdelay"]; }
            set { this["taskdelay"] = value; }
        }
    }
}
