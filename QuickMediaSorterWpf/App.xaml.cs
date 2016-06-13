using System;
using System.Linq;
using System.Windows;
using QuickMediaSorter.ObjectModel;
using QuickMediaSorter.View;
using QuickMediaSorter.ViewModel;
using TalUtils;

namespace QuickMediaSorter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            
            QuickMediaSorterProject qms = null;

            if ((e.Args.Any()))
            {
                string path = e.Args[0];
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
                    qms.ReadFiles();
                }

            }

            qms.InitializeActions();

            MainView view = new MainView();
            view.DataContext = new MainViewModel(qms);
            view.Show();
        }
    }
}
