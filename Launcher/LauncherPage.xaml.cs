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

            Label solutionName = new Label();
            solutionName.Content = lss.Name;

            mainPanel.Children.Add(solutionName);

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
                noProjects.Content = "No projects in solution.";

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
    }
}
