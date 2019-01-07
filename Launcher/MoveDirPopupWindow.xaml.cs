using LauncherLib;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interakční logika pro MoveDirPopupWindow.xaml
    /// </summary>
    public partial class MoveDirPopupWindow : Window
    {
        string sourceDirectory;
        public MoveDirPopupWindow(string sourceDirectory)
        {
            this.sourceDirectory = sourceDirectory;
            InitializeComponent();
        }

        private void MoveDir(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(dirTextBox.Text))
            {
                Error.Visibility = Visibility.Visible;
            }
            else
            {
                FileMethods.MoveDirectory(sourceDirectory, dirTextBox.Text);
                Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
