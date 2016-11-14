using QuickMediaSorter.Classes;
using QuickMediaSorter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickMediaSorter.View
{
    /// <summary>
    /// Interaction logic for ActionView.xaml
    /// </summary>
    public partial class ActionView : UserControl
    {
        public ActionView()
        {
            InitializeComponent();
        }


        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //var viewModel = (ActionViewModel)this.DataContext;
            //viewModel.Key = e.Key;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            var viewModel = (ActionViewModel)this.DataContext;
            viewModel.SetKeys(Keyboard.Modifiers, e.Key);
            ((TextBox)sender).SelectionStart = 0;
        }
    }
}
