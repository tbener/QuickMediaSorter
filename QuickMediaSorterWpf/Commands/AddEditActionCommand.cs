using System;
using System.Windows;
using System.Windows.Input;
using QuickMediaSorter.View;
using QuickMediaSorter.ViewModel;
using QuickMediaSorter.ObjectModel;

namespace QuickMediaSorter.Commands
{
    //public class ShowSettingsCommand : ICommand
    //{
    //    private static ActionEditView _view;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public void Execute(object parameter)
    //    {
    //        if (_view == null)
    //        {
    //            _view = new ActionEditView();
    //            _view.Closed += (s, e) =>
    //            {
    //                _view = null;
    //            };
    //            QuickMediaSorterProjectBatch batch = parameter as QuickMediaSorterProjectBatch;
    //            var viewModel = new ActionViewModel(batch);
    //            _view.DataContext = viewModel;
    //            _view.Show();
    //        }
    //        else
    //        {
    //            if (_view.WindowState == WindowState.Minimized) _view.WindowState = WindowState.Normal;
    //            _view.Activate();
    //        }

    //    }
    //}
}
