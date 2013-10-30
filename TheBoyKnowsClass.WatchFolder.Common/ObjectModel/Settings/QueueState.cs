using System.Configuration;
using TheBoyKnowsClass.Common.Desktop.Models.Configuration;

namespace TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings
{

    [ConfigurationCollection(typeof(QueueStateItem), AddItemName = "item")]
    public class QueueState : ConfigurationElementCollection<QueueStateItem>
    {
    }
}
