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
using LauncherLib;

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro LauncherPage.xaml
    /// </summary>
    public partial class LauncherPage : Page
    {
        SearchExplorer search = new SearchExplorer();

        public LauncherPage(string directory)
        {
            search.SearchDirectoryPath = directory;

            InsertShortcuts();

            InitializeComponent();
        }

        private void InsertShortcuts()
        {
            List<LauncherSolutionShortcut> lssList = (List<LauncherSolutionShortcut>)search.Search();

            foreach (var lss in lssList)
            {
                StackPanel sp = new StackPanel();

                foreach (var lps in lss.ProjectShortcuts)
                {
                    Button btn = new Button();

                    btn.Content = lps.ExePath;
                    btn.Click += delegate (object sender, RoutedEventArgs e) { launch(lps.ExePath, sender, e); };
                    // pridat button do stackpanelu
                }
            }
        }

        private void launch(string path, object sender, RoutedEventArgs e)
        {
            // launch
        }
    }
}
