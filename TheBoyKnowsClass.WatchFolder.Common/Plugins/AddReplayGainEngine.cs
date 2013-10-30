using System;
using System.IO;
using TheBoyKnowsClass.Common.Interfaces.Plugin;
using TheBoyKnowsClass.Tasks.Common.Interfaces;

namespace TheBoyKnowsClass.WatchFolder.Common.Plugins
{
    public class AddReplayGainEngine : ITaskEngine, IPlugin
    {
        public void ProcessItem(object item)
        {
            throw new System.NotImplementedException();
        }

        public bool CanProcessItem(object item)
        {
            return Directory.Exists((string)item);
        }

        public string Name
        {
            get { return "TheBoyKnowsClass Replay Gain Engine"; }
        }

        public Version Version
        {
            get { return new Version(1,0,0,0); }
        }

        public string Author
        {
            get { return "Gautam Bhatnagar"; }
        }
    }
}
