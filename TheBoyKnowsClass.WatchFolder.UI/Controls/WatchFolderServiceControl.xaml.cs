using System.Windows.Controls;
using TheBoyKnowsClass.WatchFolder.UI.ViewModel;

namespace TheBoyKnowsClass.WatchFolder.UI.Controls
{
    /// <summary>
    /// Interaction logic for WatchFolderServiceControl.xaml
    /// </summary>
    public partial class WatchFolderServiceControl : UserControl
    {
        public WatchFolderServiceControl()
        {
            InitializeComponent();
        }

        private WatchFolderServicesViewModel _watchFolderServices;

        public WatchFolderServicesViewModel WatchFolderServices
        {
            get { return _watchFolderServices; }
            set 
            {
                _watchFolderServices = value;
                DataContext = _watchFolderServices;
            }
        }
    }
}
