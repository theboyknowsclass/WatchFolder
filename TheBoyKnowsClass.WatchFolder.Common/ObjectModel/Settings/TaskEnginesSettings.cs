using System.Configuration;
using TheBoyKnowsClass.Common.Desktop.Models.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{
    [ConfigurationCollection(typeof(TaskEngineSettings), AddItemName = "taskengine")]
    public class TaskEnginesSettings : ConfigurationElementCollection<TaskEngineSettings>
    {
    }
}
