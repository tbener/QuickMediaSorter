using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMediaSorter.ObjectModel
{
    public class QmsFileInfo
    {
        private FileInfo _fileInfo;

        public QmsFileInfo(string fileFullName)
        {
            FullName = fileFullName;
        }

        public FileInfo Load(bool force)
        {
            if (force) _fileInfo = null;
            return FileInfo;
        }

        public FileInfo FileInfo {
            get { return _fileInfo ?? new FileInfo(FullName); }
            private set { _fileInfo = value; }
        }

        public string FullName { get; set; }
    }
}
