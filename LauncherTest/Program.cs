using LauncherLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FileHelper fileHelper = new FileHelper();
            SearchExplorer searchExplorer = new SearchExplorer();

            searchExplorer.Search();
        }
    }
}
