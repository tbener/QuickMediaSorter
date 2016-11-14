using System;
using QuickMediaSorter.ObjectModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows.Input;
using QuickMediaSorter.Commands;
using QuickMediaSorter.UserControls.ViewModel;
using QuickMediaSorter.Classes;

namespace QuickMediaSorter.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private QuickMediaSorterProject _qms;
        public ICommand SaveCommand { get; set; }
        public ICommand AddActionCommand { get; set; }

        public MainViewModel(QuickMediaSorterProject qms)
        {
            _qms = qms;
            ImagesViewViewModel = new ImagesViewModel(qms);

            // get the list of actions
            var lst = new List<QuickMediaSorterProjectBatch>(qms.Batch);
            // create a list of view models
            UserActions = new ObservableCollection<ActionViewModel>(lst.Select(a => new ActionViewModel(a, qms.Path)));

            SaveCommand = new RelayCommand(Save);
            AddActionCommand = new RelayCommand(AddNewAction);

            PathBrowserViewModel = new PathBrowserViewModel();
            PathBrowserViewModel.Path = _qms.Path;
            PathBrowserViewModel.OnPathChanged += (s, e) => { Refresh(); } ;
        }

        public void Refresh()
        {
            if (_qms.Path != PathBrowserViewModel.Path)
            {
                _qms.Path = PathBrowserViewModel.Path;
                _qms.ReadFiles();
            }
            ImageFocus = true;
        }

        public void AddNewAction()
        {
            QuickMediaSorterProjectBatch batch = new QuickMediaSorterProjectBatch();
            QuickMediaSorterProject.BatchList.Add(batch);
            ActionViewModel actvm = new ActionViewModel(batch, _qms.Path);
            UserActions.Add(actvm);
            actvm.OpenEditInNewWindow();
        }

        public PathBrowserViewModel PathBrowserViewModel
        {
            get { return (PathBrowserViewModel)GetValue(PathBrowserViewModelProperty); }
            set { SetValue(PathBrowserViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PathBrowserViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathBrowserViewModelProperty =
            DependencyProperty.Register("PathBrowserViewModel", typeof(PathBrowserViewModel), typeof(MainViewModel), new PropertyMetadata(null));


        private void Save()
        {
            Status = string.Format("Saving {0}...", QmsFactory.FileName);
            if (QmsFactory.Save())
                Status = string.Format("Saved {0}", QmsFactory.FileName);
            else
                Status = string.Format("Error saving {0}", QmsFactory.FileName);
        }

        public ImagesViewModel ImagesViewViewModel { get; set; }

        public ObservableCollection<ActionViewModel> UserActions { get; set; }


        internal void ApplyKey(ModifierKeys modifiers, Key key)
        {
            KeyClass keyClass = new KeyClass(modifiers, key);
            KeyPressed = keyClass.ToString();

            try
            {
                Status = "Executing...";
                _qms.Execute(keyClass);
                Status = "Ready";
            }
            catch (Exception ex)
            {
                Status = "Error: " + ex.Message;
            }
        }


        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(MainViewModel), new PropertyMetadata("Ready"));



        public string KeyPressed
        {
            get { return (string)GetValue(KeyPressedProperty); }
            set { SetValue(KeyPressedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KeyPressed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyPressedProperty =
            DependencyProperty.Register("KeyPressed", typeof(string), typeof(MainViewModel), new PropertyMetadata(""));

        public bool ImageFocus
        {
            get { return (bool)GetValue(ImageFocusProperty); }
            set { SetValue(ImageFocusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageFocusProperty =
            DependencyProperty.Register("ImageFocus", typeof(bool), typeof(MainViewModel), new PropertyMetadata(true));


    }
}
