using QuickMediaSorter.Commands;
using QuickMediaSorter.ObjectModel;
using QuickMediaSorter.UserControls.ViewModel;
using QuickMediaSorter.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuickMediaSorter.Classes;

namespace QuickMediaSorter.ViewModel
{
    public class ActionViewModel : BaseViewModel, IDisposable
    {

        public ICommand EditInNewWindowCommand { get; set; }

        QuickMediaSorterProjectBatch _batch;
        Key _key;
        ModifierKeys _modifiers = ModifierKeys.None;
        bool _editable;
        ActionEditView _editView;

        public ActionViewModel(QuickMediaSorterProjectBatch batch, string basePath)
        {
            _batch = batch;
            //Refresh();

            EditInNewWindowCommand = new RelayCommand(OpenEditInNewWindow);

            ActionTypes = Enum.GetNames(typeof(QuickMediaSorter.ObjectModel.ActionType));

            PathBrowserViewModel = new PathBrowserViewModel();
            PathBrowserViewModel.BasePath = basePath;
            PathBrowserViewModel.Path = Action.Folder;
            PathBrowserViewModel.OnPathChanged += (s, e) =>
            {
                Action.Path = PathBrowserViewModel.Path;
                Refresh();
            };
        }

        internal void SetKeys(KeyClass keyClass)
        {
            throw new NotImplementedException();
        }

        //public ActionViewModel(QuickMediaSorterProjectBatch batch, string folder) : this(batch)
        //{
        //    this.Folder = folder;
        //}

        public void OpenEditInNewWindow()
        {
            if (_editView == null)
            {
                _editView = new ActionEditView();
                _editView.DataContext = this;
                _editView.Closed += (s, e) =>
                {
                    _editView = null;
                };
            }
            _editView.Show();
        }

        public QuickMediaSorterProjectBatchAction Action
        {
            get
            {
                return _batch.Actions[0];
            }

        }

        public void Refresh()
        {
            ActionKeys = _batch.Triggers[0].ToString();
            ActionType = Action.ActionType;
            FolderShortDisplay = Action.Folder; // Action.Folder should provide short if available
            OnPropertyChanged("HotKeyString");
        }


        internal void SetKeys(ModifierKeys modifiers, Key key)
        {
            _batch.Triggers[0].SetKeys(modifiers, key);
            OnPropertyChanged("HotKeyString");
        }

        public void Dispose()
        {
            if (_editView != null)
            {
                _editView.Close();
            }
        }

        public bool Editable
        {
            get { return _editable; }
            set
            {
                _editable = value;
                Action.IsEdited = value;
                OnPropertyChanged("Editable");
            }
        }


        public string HotKeyString
        {
            get { return _batch.Triggers[0].ToString(); }
        }

        public string[] ActionTypes
        {
            get { return (string[])GetValue(ActionTypesProperty); }
            set { SetValue(ActionTypesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionTypes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionTypesProperty =
            DependencyProperty.Register("ActionTypes", typeof(string[]), typeof(ActionViewModel), new PropertyMetadata(null));




        public string KeyPressed
        {
            get { return (string)GetValue(KeyPressedProperty); }
            set { SetValue(KeyPressedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionKeys.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyPressedProperty =
            DependencyProperty.Register("KeyPressed", typeof(string), typeof(ActionViewModel), new PropertyMetadata(""));


        public string ActionKeys
        {
            get { return (string)GetValue(ActionKeysProperty); }
            set { SetValue(ActionKeysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActionKeys.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionKeysProperty =
            DependencyProperty.Register("ActionKeys", typeof(string), typeof(ActionViewModel), new PropertyMetadata(""));

        public string ActionType
        {
            get
            {
                return Action.ActionType;
            }
            set
            {
                Action.ActionType = value;
                OnPropertyChanged("ActionType");
            }
        }

        //public string Folder
        //{
        //    get
        //    {
        //        return Action.Folder;
        //    }
        //    set
        //    {
        //        Action.Folder = value;
        //        FolderShortDisplay = TalUtils.PathHelper.GetInnerPath(QuickMediaSorterProject.BasePath, Action.Folder);
        //        OnPropertyChanged("Folder");
        //    }
        //}

        public string FolderShortDisplay
        {
            get { return (string)GetValue(FolderShortDisplayProperty); }
            set { SetValue(FolderShortDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FolderShortDisplay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FolderShortDisplayProperty =
            DependencyProperty.Register("FolderShortDisplay", typeof(string), typeof(ActionViewModel), new PropertyMetadata(""));



        public PathBrowserViewModel PathBrowserViewModel
        {
            get { return (PathBrowserViewModel)GetValue(PathBrowserViewModelProperty); }
            set { SetValue(PathBrowserViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PathBrowserViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathBrowserViewModelProperty =
            DependencyProperty.Register("PathBrowserViewModel", typeof(PathBrowserViewModel), typeof(ActionViewModel), new PropertyMetadata(null));






    }
}
