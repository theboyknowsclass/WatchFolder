using System;

namespace TheBoyKnowsClass.WatchFolder.Service.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var watchFolderService = new SettingsServiceHost();

            watchFolderService.StartSettingsService();

            Console.WriteLine(@"Press Enter to quit.");
            Console.ReadLine();

            watchFolderService.StopSettingsService();
        }
    }
}
