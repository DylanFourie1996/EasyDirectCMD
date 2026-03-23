using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyDirectCMD
{
    public class Printlins
    {

        public  void ShowHelp()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 EasyDirectCMD - COMMAND MENU                   ║");
            Console.WriteLine("╠══════════════╦═══════════════════╦═════════════════════════════╣");
            Console.WriteLine("║   COMMAND    ║     ARGUMENT      ║         DESCRIPTION         ║");
            Console.WriteLine("╠══════════════╬═══════════════════╬═════════════════════════════╣");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("║  cmd         ║  [Path/File]      ║ Open a specific path/file   ║");
            Console.WriteLine("║  build       ║  (None)           ║ Build fast-search index     ║");
            Console.WriteLine("║  search      ║  [Filename]       ║ Instant search across PC    ║");
            Console.WriteLine("║  clear       ║  (None)           ║ Clear the console screen    ║");
            Console.WriteLine("║  Press F1    ║  (None)           ║ Open help                   ║");
            Console.WriteLine("║  Press F2    ║  (None)           ║ Clear the console screen    ║");
            Console.WriteLine("║  Press F5    ║  (None)           ║ Rebuild Index               ║");
            Console.WriteLine("║  exit        ║  (None)           ║ Close the application       ║");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════════════╩═══════════════════╩═════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\n Tip: Use 'build' first to enable O(1) instant searching.");
            Console.WriteLine();
        }

        public static void ShowSplashScreen()
        {
            Console.Clear();
            string[] logo = new string[]
            {
        " ███████╗ █████╗ ███████╗██╗   ██╗██████╗ ██╗██████╗ ███████╗ ██████╗███╗   ███╗██████╗ ",
        " ██╔════╝██╔══██╗██╔════╝╚██╗ ██╔╝██╔══██╗██║██╔══██╗██╔════╝██╔════╝████╗ ████║██╔══██╗",
        " █████╗  ███████║███████╗ ╚████╔╝ ██║  ██║██║██████╔╝█████╗  ██║     ██╔████╔██║██║  ██║",
        " ██╔══╝  ██╔══██║╚════██║  ╚██╔╝  ██║  ██║██║██╔══██╗██╔══╝  ██║     ██║╚██╔╝██║██║  ██║",
        " ███████╗██║  ██║███████║   ██║   ██████╔╝██║██║  ██║███████╗╚██████╗██║ ╚═╝ ██║██████╔╝",
        " ╚══════╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚═════╝ ╚═╝╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝     ╚═╝╚═════╝ "
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (string line in logo)
            {
                Console.WriteLine(line.PadLeft((Console.WindowWidth + line.Length) / 2));
            }

            Console.ForegroundColor = ConsoleColor.White;
            string sub = "v0.5 | Advanced File Indexing System";
            Console.WriteLine(sub.PadLeft((Console.WindowWidth + sub.Length) / 2));
            Console.ResetColor();
            Thread.Sleep(1500);
        }
    }
}
