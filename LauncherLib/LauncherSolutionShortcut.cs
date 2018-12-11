using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LauncherLib
{
    public class LauncherSolutionShortcut
    {
        public string SolutionPath;
        public string SolutionDirectory
        {
            get
            {
                return SolutionPath.Remove(SolutionPath.LastIndexOf("\\"));
            }
        }
        public List<LauncherProjectShortcut> ProjectShortcuts
        {
            get
            {
                List<LauncherProjectShortcut> projectShortcuts = new List<LauncherProjectShortcut>();

                foreach (var item in getProjectPaths())
                {
                    projectShortcuts.Add(new LauncherProjectShortcut()
                    {
                        ProjectPath = item
                    });
                }

                return projectShortcuts;
            }
        }

        private List<string> getProjectPaths()
        {
            List<string> ProjectPaths = new List<string>();

            foreach (var line in File.ReadAllLines(SolutionPath))
            {
                if (line.Contains("Project("))
                {
                    ProjectPaths.Add(SolutionDirectory + "\\" + line.Split(char.Parse(","))[1].Trim().Replace("\"", ""));
                }
            }

            return ProjectPaths;
        }
    }
}
