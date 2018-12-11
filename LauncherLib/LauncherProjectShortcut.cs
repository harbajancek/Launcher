using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LauncherLib
{
    public class LauncherProjectShortcut
    {
        public string ProjectPath;
        public string ProjectDirectory
        {
            get
            {
                return ProjectPath.Remove(ProjectPath.LastIndexOf("\\"));
            }
        }
        public string ExePath
        {
            get
            {
                if (!Exists)
                {
                    return null;
                }

                if (ReleaseExePath != null)
                {
                    return ReleaseExePath;
                }

                if (DebugExePath != null)
                {
                    return DebugExePath;
                }

                return null;
            }
        }
        public string ReleaseExePath
        {
            get
            {
                if (!Exists)
                {
                    return null;
                }

                string path = null;

                try
                {
                    path = ProjectDirectory + @"\" + xdoc
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.HasAttributes && n.Attribute("Condition").Value.Any() &&
                        n.Descendants().Where(nS => nS.Name.LocalName == "Optimize" && nS.Value == "true").Any())
                    .FirstOrDefault()
                    .Descendants()
                    .Where(n => n.Name.LocalName == "OutputPath")
                    .FirstOrDefault()
                    .Value + Name + ".exe";
                }
                catch (NullReferenceException)
                {
                    return null;
                }

                if (File.Exists(path))
                {
                    return path;
                }

                return null;

            }
        }

        public string DebugExePath
        {
            get
            {
                if (!Exists)
                {
                    return null;
                }

                string path = null;

                try
                {
                    path = ProjectDirectory + @"\" + xdoc
                    .Descendants()
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.HasAttributes && n.Attribute("Condition").Value.Any() &&
                        n.Descendants().Where(nS => nS.Name.LocalName == "Optimize" && nS.Value == "false").Any())
                    .FirstOrDefault()
                    .Descendants()
                    .Where(n => n.Name.LocalName == "OutputPath")
                    .FirstOrDefault()
                    .Value + Name + ".exe";
                }
                catch (NullReferenceException)
                {
                    return null;
                }

                if (File.Exists(path))
                {
                    return path;
                }

                return null;

            }
        }

        private XDocument xdoc
        {
            get
            {
                try
                {
                    return XDocument.Parse(File.ReadAllText(ProjectPath));
                }
                catch (Exception)
                {
                    return null;
                }
                
            }
        } 
        public string Name
        {
            get
            {
                if (!Exists)
                {
                    return null;
                }

                try
                {
                    return xdoc
                    .Descendants()
                    .Where(
                        n => n.Name.LocalName == "PropertyGroup" &&
                        n.Descendants().Any(nS => nS.Name.LocalName == "AssemblyName"))
                    .FirstOrDefault()
                    .Descendants()
                    .Where(n => n.Name.LocalName == "AssemblyName")
                    .FirstOrDefault()
                    .Value;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
                
            }
        }
        public string IconPath
        {
            get
            {
                try
                {
                    return ProjectDirectory + xdoc
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
            }
        }

        public bool HasIcon
        {
            get
            {
                return IconPath != null;
            }
        }

        public bool HasExe
        {
            get
            {
                return ExePath != null;
            }
        }
        
        public bool Exists
        {
            get
            {
                return xdoc != null;
            }
        }
    }
}
