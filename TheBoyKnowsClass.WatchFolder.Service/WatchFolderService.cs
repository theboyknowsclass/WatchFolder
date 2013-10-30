using System.ServiceProcess;
using TheBoyKnowsClass.Common.Desktop.Operations.Log;
using TheBoyKnowsClass.WatchFolder.Service.Properties;

namespace TheBoyKnowsClass.WatchFolder.Service
{
    public sealed partial class WatchFolderService : ServiceBase
    {

        private readonly SettingsServiceHost _settingsServiceHost;
        private readonly WatchFolderOperations _watchFolderOperations;
        private readonly LogHelper _logHelper;

        public WatchFolderService()
        {
            InitializeComponent();
            _logHelper = new LogHelper(Resources.EventLogName, Resources.EventLog);
            _settingsServiceHost = new SettingsServiceHost(_logHelper);
            _watchFolderOperations = new WatchFolderOperations(_logHelper);
            CanHandlePowerEvent = false;
            CanHandleSessionChangeEvent = false;
            CanPauseAndContinue = false;
            CanShutdown = true;
            CanStop = true;
        }

        #region ServiceViewModel Overrides

        protected override void OnStart(string[] args)
        {
            _settingsServiceHost.StartSettingsService();
            _watchFolderOperations.StartWatchFolders();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _settingsServiceHost.StopSettingsService();
            _watchFolderOperations.StopWatchFolders();
            base.OnStop();
        }

        protected override void  OnShutdown()
        {
             OnStop();
 	         base.OnShutdown();
        }

        #endregion

    }
}
