using System.Reflection;
using System.IO;
using TheBoyKnowsClass.Common.UI.WPF.Controls;
using TheBoyKnowsClass.Common.UI.WPF.ViewModels;
using TheBoyKnowsClass.SystemServices.Common.Models;
using TheBoyKnowsClass.SystemServices.Common.UI.ViewModels;
using TheBoyKnowsClass.WatchFolder.UI.ViewModel;

namespace TheBoyKnowsClass.WatchFolder.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SystemTrayApplicationWindow
    {
        private readonly SystemServiceViewModel _service;
        private readonly SystemTrayApplicationViewModel _systemTrayApplication;
        private readonly WatchFolderServicesViewModel _watchFolderServices;
        private readonly string _serviceName = Properties.Resources.ServiceName;
        private readonly string _serviceAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + Properties.Resources.ServiceAssembly;

        public MainWindow()
        {
            InitializeComponent();

            _service =
                new SystemServiceViewModel(new SystemService(_serviceName, _serviceAssemblyLocation))
                    {
                        Description = Properties.Resources.ServiceDescription,
                        DisplayName = Properties.Resources.ServiceDisplayName,
                        ServiceStartMode = Properties.Settings.Default.ServiceStartMode,
                        Account = Properties.Settings.Default.ServiceAccount
                    };

            SystemServiceManagerControl.LoadFromService(_service);

            _systemTrayApplication = new SystemTrayApplicationViewModel {Icon = Properties.Resources.watchmen_ico, MinimiseOnClose = Properties.Settings.Default.MinimiseOnClose};
            Initialise(_systemTrayApplication);
            _watchFolderServices = new WatchFolderServicesViewModel();
            WatchFolderServiceControl.WatchFolderServices = _watchFolderServices;
            WatchFolderTraySettingsControl.SystemTrayApplication = _systemTrayApplication;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MinimiseOnClose = _systemTrayApplication.MinimiseOnClose;
            Properties.Settings.Default.Save();

            base.OnClosing(e);
        }
    }
}
