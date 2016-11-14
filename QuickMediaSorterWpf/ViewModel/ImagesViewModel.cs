using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using QuickMediaSorter.ObjectModel;
using System.Collections.ObjectModel;

namespace QuickMediaSorter.ViewModel
{
    public class ImagesViewModel : BaseViewModel
    {
        public ImagesViewModel(QuickMediaSorterProject qms)
        {
            qms.OnChange += (s, e) => { Refresh(); } ;
            Refresh();
        }

        public void Refresh()
        {
            if (QuickMediaSorterProject.CurrentFileInfo == null)
            {
                ImageUri = "";
                FileName = "";
            }
            else
            {
                ImageUri = QuickMediaSorterProject.CurrentFileInfo.FileInfo.FullName;
                FileName = QuickMediaSorterProject.CurrentFileInfo.FileInfo.Name;
            }

            
        }

        public ObservableCollection<string> ImageList
        {
            get { return (ObservableCollection<string>)GetValue(ImageListProperty); }
            set { SetValue(ImageListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageListProperty =
            DependencyProperty.Register("ImageList", typeof(ObservableCollection<string>), typeof(ImagesViewModel), new PropertyMetadata(null));




        public ImageSource CurrentImage
        {
            get { return (ImageSource)GetValue(CurrentImageProperty); }
            set { SetValue(CurrentImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentImageProperty =
            DependencyProperty.Register("CurrentImage", typeof(ImageSource), typeof(ImagesViewModel), new PropertyMetadata(null));


        public string ImageUri
        {
            get { return (string)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageUri.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri", typeof(string), typeof(ImagesViewModel), new PropertyMetadata(""));




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
