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

        public IEnumerable<LauncherShortcut> Search()
        {
            List<LauncherShortcut> shortcuts = new List<LauncherShortcut>();

            foreach (var item in Directory.GetFiles(SearchDirectoryPath, "*.csproj", SearchOption.AllDirectories))
            {

                LauncherShortcut shortcut = new LauncherShortcut();

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

                    if (File.Exists(exePathRelease))
                    {
                        shortcuts.Add(exePathRelease);
                    }
                    else if (File.Exists(exePathDebug))
                    {
                        shortcuts.Add(exePathDebug);
                    }
                }
            }

            shortcuts.ForEach(item => Console.WriteLine(item));
            
        }
    }
}
