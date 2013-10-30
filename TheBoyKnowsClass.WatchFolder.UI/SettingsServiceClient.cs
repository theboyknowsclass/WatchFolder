using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using TheBoyKnowsClass.WatchFolder.Common.Interfaces;
using TheBoyKnowsClass.WatchFolder.Common.ObjectModel.Settings;

namespace TheBoyKnowsClass.WatchFolder.UI
{
    public class SettingsServiceClient
    {
        private SettingsServiceProxy _client;
        private readonly EndpointAddress _endPointAddress;

        public SettingsServiceClient(EndpointAddress endPointAddress)
        {
            _endPointAddress = endPointAddress;
        }

        public string Host
        {
            get { return _endPointAddress.Uri.Host; }
        }

        public int Port
        {
            get { return _endPointAddress.Uri.Port; }
        }

        public void Start()
        {
            Start(_endPointAddress);
        }

        protected void Start(EndpointAddress endpointAddress)
        {
            _client = new SettingsServiceProxy(new NetTcpBinding(), endpointAddress);
        }

        public void Stop()
        {
            _client.Close();
        }

        public WatchFoldersSettings GetWatchFoldersSettings()
        {
            if (_client == null)
            {
                return null;
            }
            return _client.GetWatchFoldersSettings();
        }

        public void SetWatchFoldersSettings(WatchFoldersSettings watchFoldersSettings)
        {
            if(_client != null)
            {
                _client.SetWatchFoldersSettings(watchFoldersSettings); 
            }
        }

        #region helper methods

        public static IEnumerable<EndpointAddress> FindService()
        {
            var discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            FindResponse settingsServices = discoveryClient.Find(new FindCriteria(typeof(ISettingsService)));
            discoveryClient.Close();

            if (settingsServices == null || settingsServices.Endpoints.Count == 0)
            {
                return new List<EndpointAddress>();
            }

            return from endpoints in settingsServices.Endpoints select endpoints.Address;
        }

        #endregion 

    }
}
