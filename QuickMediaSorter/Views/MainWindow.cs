using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using QuickMediaSorter.Helpers;
using QuickMediaSorter.ObjectModel;
using TalUtils;

namespace QuickMediaSorter.Views
{
    public partial class MainWindow : Form
    {
        public QuickMediaSorterProject Qms { get; set; }

        private List<string> _fileList;
        private Stack<string> _filesStack;
        private int _current;
        private FileInfo _fileInfo;

        public MainWindow(QuickMediaSorterProject projectClass)
        {
            InitializeComponent();

            Qms = projectClass;
        }

        public void ReadFiles()
        {
            Qms.ReadFiles();
            Refresh();
        }

        public override void Refresh()
        {
            base.Refresh();

            if (Qms.FileInfo == null)
            {
                pic.Load();
                label1.Text = "No file";
            }
            else
            {
                pic.Load(Qms.FileInfo.FullName);
                label1.Text = Qms.FileInfo.Name;
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            StatusLabel.Text = e.KeyCode.ToString();
            if (Qms.Execute(e.KeyCode.ToString()))
                Refresh();

            
            
        }
    }
}
