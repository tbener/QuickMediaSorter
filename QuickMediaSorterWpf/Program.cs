using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickMediaSorter
{
    /*
    How to get this in wpf -
    
    http://blog.clauskonrad.net/2010/08/wpf-where-is-main-method-or-how-to.html
    1) Select your project’s current startup file (app.xaml)
    2) Set BuildAction = Page
    3) Add a file as below (Program.cs)
     */
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
