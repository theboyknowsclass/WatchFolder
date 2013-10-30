using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using TheBoyKnowsClass.Common.Desktop.Enumerations;
using TheBoyKnowsClass.Common.Desktop.Operations.Log;
using TheBoyKnowsClass.Common.Enumerations;
using TheBoyKnowsClass.WatchFolder.Common.Interfaces;

namespace TheBoyKnowsClass.WatchFolder.Service
{
    public class SettingsServiceHost
    {
        private readonly LogHelper _logHelper;
        private ServiceHost _serviceHost;

        public SettingsServiceHost(LogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        public SettingsServiceHost()
        {
            _logHelper = new LogHelper(Properties.Resources.EventLog, Properties.Resources.EventLogName);
        }

        public void StartSettingsService()
        {
            var baseAddress = new Uri(String.Format(@"net.tcp://localhost/WatchFolderService"));
            _serviceHost = new ServiceHost(typeof(SettingsService), baseAddress);
            _serviceHost.Opened += ServiceHostOpened;

            try
            {
                _serviceHost.AddServiceEndpoint(typeof(ISettingsService), new NetTcpBinding(), "SettingsService");
                var discoveryBehavior = new ServiceDiscoveryBehavior();
                discoveryBehavior.AnnouncementEndpoints.Add(new UdpAnnouncementEndpoint());
                _serviceHost.Description.Behaviors.Add(discoveryBehavior);
                _serviceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());
                _serviceHost.Open();
            }
            catch (CommunicationException communicationException)
            {
                _logHelper.WriteEntry(String.Format("Error starting Settings Sevice : {0}", communicationException.Message), MessageType.Error);
                _serviceHost.Abort();
            }
        }

        public void StopSettingsService()
        {
            try
            {
                _serviceHost.Close();
            }
            catch (Exception exception)
            {
                _logHelper.WriteEntry(String.Format("Error stopping Settings Sevice : {0}", exception.Message), MessageType.Error);
                _serviceHost.Abort();
            }
        }

        private void ServiceHostOpened(object sender, EventArgs e)
        {
            _logHelper.WriteEntry("Settings Sevice Started", MessageType.Information);

            var host = (ServiceHost)sender;

            foreach (Uri baseAddress in host.BaseAddresses)
            {
                _logHelper.WriteEntry(String.Format("Settings Sevice Base Address : {0}", baseAddress), MessageType.Information);
            }

            foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
            {
                _logHelper.WriteEntry(String.Format("Settings Sevice Endpoint : {0}", endpoint), MessageType.Information);
            }
        }
    }
}
