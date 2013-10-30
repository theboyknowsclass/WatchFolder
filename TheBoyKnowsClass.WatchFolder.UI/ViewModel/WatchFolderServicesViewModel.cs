using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using Microsoft.Practices.Prism.Commands;
using TheBoyKnowsClass.Common.UI.ViewModels;

namespace TheBoyKnowsClass.WatchFolder.UI.ViewModel
{
    public class WatchFolderServicesViewModel : ViewModelBase
    {
        private class FindServiceResult
        {
            public List<EndpointAddress> EndpointAddresses { get; set; }
        }

        private readonly ObservableCollection<WatchFolderServiceViewModel> _services;
        private readonly BackgroundWorker _backgroundWorker;
        private WatchFolderServiceViewModel _selectedWatchFolderServiceViewModel;
        private bool _hasChanges;

        public WatchFolderServicesViewModel()
        {
            _services = new ObservableCollection<WatchFolderServiceViewModel>();
            _selectedWatchFolderServiceViewModel = null;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorkerDoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            FindServicesCommand = new DelegateCommand(FindServices, CanFindServices);
        }

        public ObservableCollection<WatchFolderServiceViewModel> Services
        {
            get { return _services; }
        }

        public bool HasItems 
        { 
            get { return _services.Count > 0 && !_isFinding; } 
        }

        public bool IsNotFinding
        {
            get { return !_isFinding; }
            private set 
            {
                if (_isFinding != value)
                {
                    return;
                }
                _isFinding = !value;
                RaisePropertyChanged("IsNotFinding");
                RaisePropertyChanged("IsFinding");
            }
        }

        public bool IsFinding
        {
            get { return _isFinding; }
            private set
            {
                if (_isFinding == value)
                {
                    return;
                }
                _isFinding = value;
                RaisePropertyChanged("IsNotFinding");
                RaisePropertyChanged("IsFinding");
            }
        }

        public DelegateCommand FindServicesCommand { get; set; }

        private bool _isFinding;

        private bool CanFindServices()
        {
            return IsNotFinding;
        }

        public WatchFolderServiceViewModel SelectedWatchFolderServiceViewModel
        {
            get { return _selectedWatchFolderServiceViewModel; }
            set
            {
                if(_selectedWatchFolderServiceViewModel != value)
                {
                    _selectedWatchFolderServiceViewModel = value;
                    HasChanges = true;
                    RaisePropertyChanged("SelectedWatchFolderServiceViewModel");
                    RaisePropertyChanged("IsServiceSelected");
                }
            }
        }

        public bool IsServiceSelected
        {
            get { return _selectedWatchFolderServiceViewModel != null; }
        }

        public bool HasChanges
        {
            get { return IsServiceSelected ? SelectedWatchFolderServiceViewModel.HasChanges || _hasChanges : _hasChanges; }
            private set
            {
                _hasChanges = value;
                RaisePropertyChanged("HasChanges");
            }
        }

        private void FindServices()
        {
            IsFinding = true;
            RaisePropertyChanged("HasItems");
            FindServicesCommand.RaiseCanExecuteChanged();
            _services.Clear();
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var findServiceResult = (FindServiceResult)e.Result;

            foreach (EndpointAddress endpointAddress in findServiceResult.EndpointAddresses)
            {
                _services.Add(new WatchFolderServiceViewModel(new SettingsServiceClient(endpointAddress)));
                
            }

            IsFinding = false;
            RaisePropertyChanged("HasItems");
            RaisePropertyChanged("WatchFolderServices");
            SelectedWatchFolderServiceViewModel = (from s in _services select s).FirstOrDefault();
            FindServicesCommand.RaiseCanExecuteChanged();
        }

        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new FindServiceResult {EndpointAddresses = SettingsServiceClient.FindService().ToList()};
        }
    }
}
