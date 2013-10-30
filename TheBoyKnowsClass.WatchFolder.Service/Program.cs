using System.ServiceProcess;

namespace TheBoyKnowsClass.WatchFolder.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var servicesToRun = new ServiceBase[] 
                                              { 
                                                  new WatchFolderService() 
                                              };
            ServiceBase.Run(servicesToRun);
        }
    }
}
