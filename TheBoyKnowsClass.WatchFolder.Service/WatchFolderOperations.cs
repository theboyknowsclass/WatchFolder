using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TheBoyKnowsClass.Common.Desktop.Enumerations;
using TheBoyKnowsClass.Common.Desktop.Models.Plugin;
using TheBoyKnowsClass.Common.Desktop.Operations.Log;
using TheBoyKnowsClass.Common.Enumerations;
using TheBoyKnowsClass.Common.Interfaces.Plugin;
using TheBoyKnowsClass.Common.Models.Queue;
using TheBoyKnowsClass.FolderWatchers.Common.Models;
using TheBoyKnowsClass.Schedules.Common.Models;
using TheBoyKnowsClass.Tasks.Common.Interfaces;
using TheBoyKnowsClass.Tasks.Common.Models;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.Service
{
    public class WatchFolderOperations
    {
        private WatchFolderSettingsHelper _watchFolderSettingsHelper;
        private readonly Dictionary<string, Thread> _producerThreads;
        private readonly Dictionary<string, Thread> _consumerThreads;
        private readonly Dictionary<string, EventWaitHandle> _exitThreadEvents;
        private readonly Dictionary<string, SyncItemQueue<FolderWatcherQueueItem>> _queues;
        private readonly LogHelper _logHelper;

        public WatchFolderOperations(LogHelper logHelper)
        {
            _exitThreadEvents = new Dictionary<string, EventWaitHandle>();
            _producerThreads = new Dictionary<string, Thread>();
            _consumerThreads = new Dictionary<string, Thread>();
            _queues = new Dictionary<string, SyncItemQueue<FolderWatcherQueueItem>>();
            _logHelper = logHelper;
        }

        public void StartWatchFolders()
        {
            _logHelper.WriteEntry("Starting WatchFolder ServiceViewModel", MessageType.Information);

            try
            {
                _watchFolderSettingsHelper = new WatchFolderSettingsHelper(_logHelper);
                WatchFoldersSettings watchFoldersSettings = _watchFolderSettingsHelper.Settings.WatchFoldersSettings;

                foreach (WatchFolderSettings watchFolderSettings in watchFoldersSettings)
                {
                    StartWatchFolder(watchFolderSettings.FolderName);
                }

                _logHelper.WriteEntry("Started WatchFolder ServiceViewModel", MessageType.Information);
            }
            catch (Exception exception)
            {
                _logHelper.WriteEntry(exception.Message, MessageType.Error);
            }
        }

        public void StartWatchFolder(string folderName)
        {
            _logHelper.WriteEntry(String.Format("Starting WatchFolder ServiceViewModel for {0}", folderName), MessageType.Information);

            WatchFolderSettings watchFolderSettings = _watchFolderSettingsHelper.Settings.WatchFoldersSettings[folderName];

            // load task engines
            var pluginLoader = new PluginLoader<IPlugin>();
            var taskFactory = new TaskFactory();
            pluginLoader.LoadPlugins(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));

            foreach (TaskEngineSettings taskEngineSettings in watchFolderSettings.TaskEnginesSettings)
            {
                taskFactory.AddToTaskEngines((ITaskEngine)pluginLoader.GetPlugin(taskEngineSettings.TypeName));
            }

            _logHelper.WriteEntry(String.Format("{0} Task Engines detected for WatchFolder ServiceViewModel for {1}", taskFactory.TaskEngines.Count, folderName), MessageType.Information);

            if(taskFactory.TaskEngines.Count == 0)
            {
                _logHelper.WriteEntry(String.Format("{0} Task Engines detected for WatchFolder ServiceViewModel for {1}.  Stopping", taskFactory.TaskEngines.Count, folderName), MessageType.Warning);
                return;
            }

            if (!_queues.ContainsKey(folderName))
            {
                _queues[folderName] = new SyncItemQueue<FolderWatcherQueueItem>();
            }
            else
            {
                _queues.Add(folderName, new SyncItemQueue<FolderWatcherQueueItem>());
            }

            if (_exitThreadEvents.ContainsKey(folderName))
            {
                _exitThreadEvents[folderName] = new ManualResetEvent(false);
            }
            else
            {
                _exitThreadEvents.Add(folderName, new ManualResetEvent(false)); 
            }

            //deserialise queue from settings

            foreach (QueueStateItem queueItem in watchFolderSettings.Queue)
            {
                _queues[folderName].EnqueueSync(new FolderWatcherQueueItem {Path = queueItem.Path, Time = queueItem.DateTime});
            }

            //var transcodeEngine = new FlacToMp3TranscodeEngine();
            //var copyFolderArtEngine = new CopyFolderArtEngine();
            //taskFactory.AddToTaskEngines(transcodeEngine);
            //taskFactory.AddToTaskEngines(copyFolderArtEngine);

            var scheduleTicker = new ScheduleTicker(watchFolderSettings.ScheduleSettings.GetSchedule(), watchFolderSettings.TimerInterval);
            var folderWatcher = new FolderWatcher(_queues[folderName], _exitThreadEvents[folderName], folderName, "*.*", true);
            var taskScheduler = new TaskScheduler(_queues[folderName], _exitThreadEvents[folderName], taskFactory, watchFolderSettings.InitialTaskDelay, scheduleTicker);

            if (!_producerThreads.ContainsKey(watchFolderSettings.FolderName))
            {
                _producerThreads.Add(folderName, new Thread(folderWatcher.ThreadRun) { Name = String.Format("ProducerThread for {0}", watchFolderSettings.FolderName) });
            }

            if (!_consumerThreads.ContainsKey(watchFolderSettings.FolderName))
            {
                _consumerThreads.Add(folderName, new Thread(taskScheduler.ThreadRun) { Name = String.Format("ConsumerThread for {0}", watchFolderSettings.FolderName) });
            }

            _producerThreads[folderName].Start();
            _consumerThreads[folderName].Start();

            _logHelper.WriteEntry(String.Format("Started WatchFolder ServiceViewModel for {0}", folderName), MessageType.Information);
        }

        public void StopWatchFolders()
        {
            _logHelper.WriteEntry("Stopping WatchFolder ServiceViewModel", MessageType.Information);

            foreach (KeyValuePair<string, EventWaitHandle> keyValuePair in _exitThreadEvents)
            {
                StopWatchFolder(keyValuePair.Key);
            }

            _logHelper.WriteEntry("Stopped WatchFolder ServiceViewModel", MessageType.Information);
        }

        public void StopWatchFolder(string folderName)
        {
            _logHelper.WriteEntry(String.Format("Stopping WatchFolder ServiceViewModel for {0}", folderName), MessageType.Information);

            _exitThreadEvents[folderName].Set();
            _producerThreads[folderName].Join();
            _consumerThreads[folderName].Join();

            //serialise queue out to settings

            var queueState = new QueueState();

            foreach (FolderWatcherQueueItem item in _queues[folderName].GetItems())
            {
                queueState.Add(new QueueStateItem { Path = item.Path, DateTime = item.Time });
            }

            _watchFolderSettingsHelper.Settings.WatchFoldersSettings[folderName].Queue = queueState;
            _watchFolderSettingsHelper.SaveSettings();

            _logHelper.WriteEntry(String.Format("Stopped WatchFolder ServiceViewModel for {0}", folderName), MessageType.Information);
        }
    }
}
