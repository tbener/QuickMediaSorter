using System;
using System.Linq;
using System.Windows.Forms;
using QuickMediaSorter.ObjectModel;
using QuickMediaSorter.Views;
using TalUtils;

namespace QuickMediaSorter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // if qms file
            //      load it (it includes folder)
            // else
            //      load default qms
            
            // if not folder
            //      open dialog for folder
            //

            QuickMediaSorterProject qms = null;

            if ((args != null) && (args.Any()))
            {
                string path = args[0];
                if (PathHelper.IsFolder(path))
                {
                    // We have only path
                    // - Read or generate default project
                    qms = QmsFactory.GetDefault();
                    // - Set its path
                    qms.Folder = path;
                }
                else
                {
                    // We have a qms file - try to read it
                    try
                    {
                        qms = QmsFactory.Load(path);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler.Handle(ex, "Couldn't load file", true);
                    }
                }
            }
            else
            {
                // No arguments - read or generate default project
                qms = QmsFactory.GetDefault();
            }

            if (qms != null && qms.Folder == null)
            {
                // Our project has no folder - open browse window to the user
                String f = qms.Folder;
                if (DialogHelper.BrowseOpenFolderDialog(ref f))
                {
                    qms.Folder = f;
                }
                 
            }

            qms.InitializeActions();

            MainWindow view = new MainWindow(qms);
            view.ReadFiles();

            Application.Run(view);
        }
    }
}
