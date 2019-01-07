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
using System.Windows.Shapes;

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro SearchDirectoryPopupWindow.xaml
    /// </summary>
    public partial class SearchDirectoryPopupWindow : Window
    {
        public SearchDirectoryPopupWindow()
        {
            InitializeComponent();
        }

        private void ReturnDirectory(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
