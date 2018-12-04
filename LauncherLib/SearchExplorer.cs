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

        public void Search()
        {
            List<string> exePaths = new List<string>();

            foreach (var item in Directory.GetFiles(SearchDirectoryPath, "*.csproj", SearchOption.AllDirectories))
            {
                

                XDocument dox = XDocument.Parse(File.ReadAllText(item));

                //var group = dox.Descendants().Descendants().Where(n => n.Attribute("Condition").Value == " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ").FirstOrDefault();

                /*var basicProperty = dox
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" && 
                        n.Descendants("AssemblyName").Any())
                    .FirstOrDefault();*/

                var basicProperty = dox
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.Descendants("AssemblyName").Any())// tohle nefunguje
                    .FirstOrDefault();

                var releaseProperty = dox
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.HasAttributes && n.Attribute("Condition").Value == " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ")
                    .FirstOrDefault();

                if (releaseProperty != null && basicProperty != null)
                {
                    var node = releaseProperty.Descendants().Where(n => n.Name.LocalName == "OutputPath").FirstOrDefault();

                    if (node != null)
                    {
                        Console.WriteLine(Path.GetDirectoryName(item) + @"\" + node.Value + basicProperty.Descendants("AssemblyName").FirstOrDefault().Value);
                    }
                }
            }
            
        }
    }
}
