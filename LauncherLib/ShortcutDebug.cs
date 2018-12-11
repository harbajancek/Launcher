using System;
using System.Collections.Generic;
using System.Text;

namespace LauncherLib
{
    public class ShortcutDebug
    {
        public static void PrintProjectShortcutInfo(LauncherProjectShortcut lps)
        {
            Console.Write("{0}\n\tName: {1}\n", lps.Name, lps.Name);

            if (lps.HasExe)
            {
                Console.Write("\tExePath: {0}\n", lps.ExePath);
            }

            if (lps.HasIcon)
            {
                Console.Write("\tIconPath: {0}\n", lps.IconPath);
            }
        }

        public static void PrintSolutionShortcutInfo(LauncherSolutionShortcut lss)
        {
            foreach (var item in lss.ProjectShortcuts)
            {
                PrintProjectShortcutInfo(item);
            }
        }
    }
}
