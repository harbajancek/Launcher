using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LauncherLib
{
    public class FileMethods
    {
        static Encoding Encoding = Encoding.Default;
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static string GetStringData(string path)
        {

            if (!FileExists(path))
            {
                Console.WriteLine("Error - File \"{0}\" does not exist.", path);
                return null;
            }

            return File.ReadAllText(path, Encoding);
        }

        public static void InsertData(string path, string str)
        {
            File.WriteAllText(path, str, Encoding);
        }

        public static void MoveDirectory(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                Console.WriteLine("Error - Directory \"{0}\" does not exist.", sourcePath);
                return;
            }

            sourcePath = sourcePath.TrimEnd(char.Parse(@"\"));
            destinationPath = destinationPath.TrimEnd(char.Parse(@"\"));

            string directoryName = Path.GetFileName(sourcePath);

            Directory.Move(sourcePath, destinationPath+@"\"+directoryName);
        }
    }
}
