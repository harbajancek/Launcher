using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LauncherLib
{
    public class SearchExplorer
    {
        public string SearchDirectoryPath = @"D:\harbaja16\";

        public IEnumerable<LauncherSolutionShortcut> Search()
        {
            List<LauncherSolutionShortcut> shortcuts = new List<LauncherSolutionShortcut>();

            foreach (var item in Directory.GetFiles(SearchDirectoryPath, "*.sln", SearchOption.AllDirectories))
            {
                LauncherSolutionShortcut lss = new LauncherSolutionShortcut();
                lss.SolutionPath = item;
                shortcuts.Add(lss);
            }

            return shortcuts;
        }
        /*
        public IEnumerable<LauncherProjectShortcut> Search()
        {
            List<LauncherProjectShortcut> shortcuts = new List<LauncherProjectShortcut>();

            foreach (var item in Directory.GetFiles(SearchDirectoryPath, "*.csproj", SearchOption.AllDirectories))
            {

                LauncherProjectShortcut shortcut = new LauncherProjectShortcut();

                XDocument xdoc = XDocument.Parse(File.ReadAllText(item));

                var propertyWithAssemblyName = xdoc
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.Descendants().Any(nS => nS.Name.LocalName == "AssemblyName"))
                    .FirstOrDefault();

                var propertyWithReleaseOutputPath = xdoc
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.HasAttributes && n.Attribute("Condition").Value.Any() &&
                        n.Descendants().Where(nS => nS.Name.LocalName == "Optimize" && nS.Value == "true").Any())
                    .FirstOrDefault();

                var propertyWithDebugOutputPath = xdoc
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.HasAttributes && n.Attribute("Condition").Value.Any() &&
                        n.Descendants().Where(nS => nS.Name.LocalName == "Optimize" && nS.Value == "false").Any())
                    .FirstOrDefault();

                if (propertyWithAssemblyName != null && propertyWithReleaseOutputPath != null && propertyWithDebugOutputPath != null)
                {
                    string debugOutputPath = propertyWithDebugOutputPath
                        .Descendants()
                        .Where(n => n.Name.LocalName == "OutputPath")
                        .FirstOrDefault()
                        .Value;

                    string releaseOutputPath = propertyWithReleaseOutputPath
                        .Descendants()
                        .Where(n => n.Name.LocalName == "OutputPath")
                        .FirstOrDefault()
                        .Value;

                    string assemblyName = propertyWithAssemblyName
                        .Descendants()
                        .Where(n => n.Name.LocalName == "AssemblyName")
                        .FirstOrDefault()
                        .Value;

                    string exePathDebug = Path.GetDirectoryName(item) + @"\" + debugOutputPath + assemblyName + ".exe";
                    string exePathRelease = Path.GetDirectoryName(item) + @"\" + releaseOutputPath + assemblyName + ".exe";

                    string appIconPath = getIconPath(xdoc, Path.GetDirectoryName(item) + @"\");

                    shortcut.IconPath = appIconPath;
                    shortcut.Name = assemblyName;

                    if (File.Exists(exePathRelease))
                    {
                        shortcut.ExePath = exePathRelease;
                    }
                    else if (File.Exists(exePathDebug))
                    {
                        shortcut.ExePath = exePathDebug;
                    }

                    shortcuts.Add(shortcut);
                }
            }

            return shortcuts;

            //shortcuts.ForEach(item => ShortcutDebug.ShortcutInfo(item));
            
        }

        string getIconPath(XDocument xdoc, string prefix)
        {
            try
            {
                return prefix + xdoc
                    .Descendants()
                    .Descendants()
                    .Where(n => n.Name.LocalName == "ItemGroup" &&
                    n.Descendants().Where(nS => nS.Name.LocalName == "Resource" && nS.FirstAttribute.Value.Contains(".ico")).Any())
                    .FirstOrDefault()
                    .Descendants()
                    .Where(n => n.Name.LocalName == "Resource")
                    .FirstOrDefault()
                    .Attribute("Include")
                    .Value;
            }
            catch (Exception)
            {
                return null;
            }
            
        }*/
    }
}
