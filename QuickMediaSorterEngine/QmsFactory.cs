using System;
using System.IO;
using log4net;
using QuickMediaSorter.ObjectModel;
using TalUtils;

namespace QuickMediaSorter
{
    public static class QmsFactory
    {
        private const string DEFAULT_FILE = "defuserset.qms";

        private static readonly ILog _log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly ErrorHandler _errorHandler = new ErrorHandler(_log);

        public static QuickMediaSorterProject GetDefault()
        {
            string file = PathHelper.GetFullPath(DEFAULT_FILE);

            if (File.Exists(file))
                return Load(file);

            QuickMediaSorterProject qms = QuickMediaSorterProject.GetDefault();
            Save(qms, file);
            return qms;
        }

        public static QuickMediaSorterProject Load(string file)
        {
            try
            {
                QuickMediaSorterProject qms = SerializeHelper.Load(typeof(QuickMediaSorterProject), file) as QuickMediaSorterProject;
                //if (OnConfigurationLoaded != null)
                //    OnConfigurationLoaded(null, new EventArgs());
                return qms;
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex, true, "Error while loading configuration file '{0}'", file);
                return null;
            }
        }

        public static bool Save(QuickMediaSorterProject qms, string file)
        {
            
            try
            {
                return SerializeHelper.Save(qms, file);
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex, true, "Error while saving configuration file '{0}'", file);
                return false;
            }
        }
    }
}
