using System.Windows.Controls;
using TheBoyKnowsClass.Common.UI.WPF.ViewModels;

namespace TheBoyKnowsClass.WatchFolder.UI.Controls
{
    /// <summary>
    /// Interaction logic for WatchFolderTraySettingsControl.xaml
    /// </summary>
    public partial class WatchFolderTraySettingsControl : UserControl
    {
        public WatchFolderTraySettingsControl()
        {
            InitializeComponent();
        }

        private SystemTrayApplicationViewModel _systemTrayApplication;

        public SystemTrayApplicationViewModel SystemTrayApplication
        {
            get { return _systemTrayApplication; }
            set 
            { 
                _systemTrayApplication = value;
                DataContext = _systemTrayApplication;
            }
        }
    }
}
