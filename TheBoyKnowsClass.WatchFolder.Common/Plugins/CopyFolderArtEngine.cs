using System;
using TheBoyKnowsClass.Common.Interfaces.Plugin;
using TheBoyKnowsClass.Tasks.Common.Interfaces;

namespace TheBoyKnowsClass.WatchFolder.Common.Plugins
{
    public class CopyFolderArtEngine : ITaskEngine, IPlugin, IConfigurablePlugin
    {
        public void ProcessItem(object item)
        {
            //var audioTaskParameter = (AudioTaskParameter)parameter;

            //string sourceFileName = audioTaskParameter.InputFile;

            //string targetFileName = sourceFileName.Replace(audioTaskParameter.SourceFolderRoot,
            //                                               audioTaskParameter.TargetFolderRoot);

            //File.Copy(sourceFileName,targetFileName, true);

        }

        public bool CanProcessItem(object item)
        {
            //if (parameter is AudioTaskParameter)
            //{
            //    return ((AudioTaskParameter) parameter).InputFile.ToLower().Contains("folder.jpg")
            //           || ((AudioTaskParameter)parameter).InputFile.ToLower().Contains("folder.gif")
            //           || ((AudioTaskParameter)parameter).InputFile.ToLower().Contains("folder.bmp");
            //}

            return false;
        }

        public string Name
        {
            get { return "TheBoyKnowsClass Folder Art Copying Engine"; }
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
            throw new System.NotImplementedException();
        }

        public bool SetSettings(object settings)
        {
            throw new System.NotImplementedException();
        }

        public void DisplayConfig(ref object settings)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
