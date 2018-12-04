using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LauncherLib
{
    public class FileHelper
    {
        public string Filename { get; set; } = "script.cs";
        Encoding Encoding = Encoding.Default;
        public bool FileExists
        {
            get
            {
                return File.Exists(Filename);
            }
        }

        public string GetStringData()
        {
            try
            {
                return File.ReadAllText(Filename, Encoding);
            }
            catch
            {
                Console.WriteLine("Error - File \"{0}\" does not exist.", Filename);
                return null;
            }
        }

        public void InsertData(string str)
        {
            File.WriteAllText(Filename, str, Encoding);
        }
    }
}
