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
using System.Drawing;
using System.Diagnostics;
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
            InitializeComponent();
            InsertShortcuts();

            
        }

        private void InsertShortcuts()
        {
            List<LauncherSolutionShortcut> lssList = (List<LauncherSolutionShortcut>)search.Search();

            foreach (var lss in lssList)
            {
                StackPanel lw = new StackPanel();
                lw.Orientation = Orientation.Horizontal;
                lw.Margin = new Thickness(0, 0, 0, 5);

                foreach (var lps in lss.ProjectShortcuts)
                {
                    if (!lps.HasExe)
                    {
                        continue;
                    }
                    Button btn = new Button() { Margin = new Thickness(5), Width = 64, Height = 64, Background = null/*, BorderBrush = null*/};
                    StackPanel sp2 = new StackPanel();

                    TextBlock text = new TextBlock()
                    {
                        Text = lps.Name,
                        FontSize = 10
                    };

                    btn.Content = sp2;
                    btn.Click += delegate (object sender, RoutedEventArgs e) { launch(lps.ExePath, sender, e); };

                    Image img = new Image();
                    img.Height = 32;
                    img.Width = 32;
                    

                    img.Source =
                    sp2.Children.Add(img);

                    sp2.Children.Add(text);
                    lw.Children.Add(btn);
                }

                ShortcutPanel.Children.Add(lw);
            }
        }

        private void launch(string path, object sender, RoutedEventArgs e)
        {
            Process.Start(path);
        }
    }
}
