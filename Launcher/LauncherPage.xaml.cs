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
using System.Windows.Interop;

namespace Launcher
{
    /// <summary>
    /// Interakční logika pro LauncherPage.xaml
    /// </summary>
    public partial class LauncherPage : Page
    {
        SearchExplorer search = new SearchExplorer();
        string searchDirectory;

        public LauncherPage(string directory)
        {
            searchDirectory = directory;
            InitializeComponent();
            InsertShortcuts();
        }

        private void InsertShortcuts()
        {
            List<LauncherSolutionShortcut> lssList = (List<LauncherSolutionShortcut>)search.Search(searchDirectory);

            foreach (var lss in lssList)
            {
                ShortcutPanel.Children.Add(getSolutionBox(lss));
            }

        }

        private UIElement getSolutionBox(LauncherSolutionShortcut lss)
        {

            StackPanel mainPanel = new StackPanel();
            mainPanel.Orientation = Orientation.Vertical;

            StackPanel labelPanel = new StackPanel();
            labelPanel.Orientation = Orientation.Vertical;

            Label LsolutionName = new Label()
            {
                Content = lss.Name
            };

            Label LsolutionDirectoryPath = new Label()
            {
                Content = lss.SolutionDirectory
            };

            Menu lssOptions = new Menu();
            lssOptions.Background = null;

            MenuItem options = new MenuItem()
            {
                Header = "..."
            };

            lssOptions.Items.Add(options);

            MenuItem copyPathClipItem = new MenuItem()
            {
                Header = "Copy path to clipboard",
            };
            copyPathClipItem.Click += delegate (object sender, RoutedEventArgs e) { Clipboard.SetText(lss.SolutionDirectory); };

            MenuItem deleteSolutionItem = new MenuItem()
            {
                Header = "Delete solution",
            };
            deleteSolutionItem.Click += delegate (object sender, RoutedEventArgs e) { deleteSolution(lss.SolutionDirectory); };

            options.Items.Add(copyPathClipItem);
            options.Items.Add(deleteSolutionItem);

            labelPanel.Children.Add(LsolutionName);
            labelPanel.Children.Add(LsolutionDirectoryPath);
            labelPanel.Children.Add(lssOptions);

            mainPanel.Children.Add(labelPanel);

            StackPanel projectsPanel = new StackPanel();
            projectsPanel.Orientation = Orientation.Horizontal;

            foreach (var lps in lss.ProjectShortcuts)
            {
                if (!lps.HasExe)
                {
                    continue;
                }
                projectsPanel.Children.Add(getProjectButton(lps));
            }

            if (projectsPanel.Children.Count == 0)
            {
                Label noProjects = new Label();
                noProjects.Content = "No projects with release or debug .exe file in solution.";

                projectsPanel.Children.Add(noProjects);
            }

            mainPanel.Children.Add(projectsPanel);

            Separator separator = new Separator();

            mainPanel.Children.Add(separator);

            return mainPanel;

        }

        private UIElement getProjectButton(LauncherProjectShortcut lps)
        {
            Button btn = new Button() { Margin = new Thickness(5), Width = 64, Height = 64, Background = null, BorderBrush = null };
            StackPanel sp2 = new StackPanel();

            TextBlock text = new TextBlock()
            {
                Text = lps.Name,
                FontSize = 10
            };

            btn.Content = sp2;
            btn.Click += delegate (object sender, RoutedEventArgs e) { launch(lps.ExePath, sender, e); };

            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Height = 32;
            img.Width = 32;


            img.Source = ToImageSource(Icon.ExtractAssociatedIcon(lps.ExePath));

            sp2.Children.Add(img);
            sp2.Children.Add(text);

            return btn;
        }

        private ImageSource ToImageSource(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        private void launch(string path, object sender, RoutedEventArgs e)
        {
            Process.Start(path);
        }

        private void deleteSolution(string directory)
        {
            if (MessageBox.Show("Are you sure? Solution will be completely deleted.", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                FileMethods.DeleteDirectory(directory);
            }

            Refresh();
        }

        private void Refresh()
        {
            ShortcutPanel.Children.RemoveRange(0, ShortcutPanel.Children.Count);
            InsertShortcuts();
        }
    }
}
