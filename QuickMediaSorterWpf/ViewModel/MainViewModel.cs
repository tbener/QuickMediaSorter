using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickMediaSorter.ObjectModel;
using QuickMediaSorter.View;

namespace QuickMediaSorter.ViewModel
{
    public class MainViewModel
    {
        private QuickMediaSorterProject _qms;

        public MainViewModel(QuickMediaSorterProject qms)
        {
            _qms = qms;
            ImagesViewViewModel = new ImagesViewModel(qms);
            
        }

        public ImagesViewModel ImagesViewViewModel { get; set; }

        internal void ApplyKey(System.Windows.Input.Key key)
        {
            if (_qms.Execute(key.ToString()))
            {
                ImagesViewViewModel.Refresh();
            }
        }
    }
}
