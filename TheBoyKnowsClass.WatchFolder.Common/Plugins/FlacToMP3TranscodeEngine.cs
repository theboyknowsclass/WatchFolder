using System;
using System.Threading;
using TheBoyKnowsClass.Common.Interfaces.Plugin;
using TheBoyKnowsClass.Tasks.Common.Interfaces;

namespace TheBoyKnowsClass.WatchFolder.Common.Plugins
{
    public class FlacToMp3TranscodeEngine : ITaskEngine, IPlugin, IConfigurablePlugin
    {
        private static string OutputExtension
        {
            get { return ".mp3"; }
        }

        private void Transcode(string sourceFileName, string targetFileName)
        {
            Console.WriteLine("Started Encoding" + sourceFileName + " to " + targetFileName);
            Thread.Sleep(10000);
            Console.WriteLine("Finished Encoding Job");
        }

        private void UpdateTags(string sourceFileName, string targetFileName)
        {
            Console.WriteLine("Started Updating Tags from " + sourceFileName + " to " + targetFileName);
            Thread.Sleep(5000);
            Console.WriteLine("Finished Update Tags Job");
        }

        #region ITaskEngine Members

        public void ProcessItem(object parameter)
        {
            //var audioTaskParameter = (AudioTaskParameter)parameter;

            //string sourceFileName = audioTaskParameter.InputFile;
            ////do some checks to see if it's ready for transcoding

            //string fileNameWithoutExtension = Path.GetDirectoryName(sourceFileName) + @"\" + Path.GetFileNameWithoutExtension(sourceFileName);

            //string targetFileName =
            //    fileNameWithoutExtension.Replace(audioTaskParameter.SourceFolderRoot, audioTaskParameter.TargetFolderRoot)
            //    + (!OutputExtension.Contains(".") ? "." : "")
            //    + OutputExtension;

            //if (File.Exists(targetFileName))
            //{
            //    UpdateTags(sourceFileName, targetFileName);
            //}
            //else
            //{
            //    Transcode(sourceFileName, targetFileName);
            //}

        }

        public bool CanProcessItem(object item)
        {
            return ((string)item).Contains(".flac");
        }

        #endregion

        public string Name
        {
            get { return "TheBoyKnowsClass Flac to MP3 Transcoding Engine"; }
        }

        public Version Version
        {
            get { return new Version(1,0,0,0); }
        }

        public string Author
        {
            get { return "Gautam Bhatnagar"; }
        }

        #region IConfigurablePlugin Members

        public object GetSettings()
        {
            throw new NotImplementedException();
        }

        public bool SetSettings(object settings)
        {
            throw new NotImplementedException();
        }

        public void DisplayConfig(ref object settings)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
