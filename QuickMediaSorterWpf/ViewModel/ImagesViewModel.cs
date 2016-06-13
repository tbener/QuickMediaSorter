using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QuickMediaSorter.ObjectModel;

namespace QuickMediaSorter.ViewModel
{
    public class ImagesViewModel : BaseViewModel
    {
        private QuickMediaSorterProject _qms;

        public ImagesViewModel(QuickMediaSorterProject qms)
        {
            _qms = qms;
            Refresh();
        }

        public void Refresh()
        {
            CurrentImage = new BitmapImage(new Uri(_qms.FileInfo.FullName));
            FileName = _qms.FileInfo.Name;
        }





        public ImageSource CurrentImage
        {
            get { return (ImageSource)GetValue(CurrentImageProperty); }
            set { SetValue(CurrentImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentImageProperty =
            DependencyProperty.Register("CurrentImage", typeof(ImageSource), typeof(ImagesViewModel), new PropertyMetadata(null));




        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(ImagesViewModel), new PropertyMetadata("No file"));

        
        

    }
}
