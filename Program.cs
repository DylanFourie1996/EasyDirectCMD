using EasyDirectCMD.Interface;
using EasyDirectCMD.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDirectCMD
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "EasyDirectCMD v2.0 | Pro Indexer";
            Printlins.ShowSplashScreen();

            //Services Initialization
            IFileIndexService indexService = new FileIndexService();
            IFileSearchService searchService = new FileSearchService(indexService);
            IFileOpener fileOpener = new FileOpener();

            Printlins printlins = new Printlins();
            StringBuilder inputBuffer = new StringBuilder();

            indexService.Load();
            bool running = true;

            while (running)
            {
                Console.Write("\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" ➜ ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("EasyDirect");

                int count = indexService.GetCount();
                if (count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" [{count:N0}]");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" ❯ ");
                Console.ResetColor();

                inputBuffer.Clear();
                string commandLine = "";

                while (true)
                {
                    var keyInfo = Console.ReadKey(intercept: true);

                    //  F5 – Rebuild Index
                    if (keyInfo.Key == ConsoleKey.F5)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n [F5] Triggering Index Rebuild...");
                        indexService.BuildIndex();
                        indexService.Save();
                        commandLine = "SKIP";
                        break;
                    }

                    // F1 – Help
                    if (keyInfo.Key == ConsoleKey.F1)
                    {
                        printlins.ShowHelp();
                        commandLine = "SKIP";
                        break;
                    }

                    // F2 – Clear Console
                    if (keyInfo.Key == ConsoleKey.F2)
                    {
                        Console.Clear();
                        Printlins.ShowSplashScreen();
                        commandLine = "SKIP";
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        commandLine = inputBuffer.ToString();
                        break;
                    }

                    if (keyInfo.Key == ConsoleKey.Backspace && inputBuffer.Length > 0)
                    {
                        inputBuffer.Remove(inputBuffer.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        inputBuffer.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar);
                    }
                }

                if (string.IsNullOrWhiteSpace(commandLine) || commandLine == "SKIP")
                    continue;

                string[] parts = commandLine.Split(' ', 2);
                string command = parts[0].ToLower();
                string argument = parts.Length > 1 ? parts[1] : string.Empty;

                try
                {
                    switch (command)
                    {
                        case "help":
                        case "?":
                            printlins.ShowHelp();
                            break;

                        case "clear":
                        case "clr":
                            Console.Clear();
                            break;

                        case "exit":
                        case "quit":
                            running = false;
                            break;

                        case "cmd":
                            if (!string.IsNullOrWhiteSpace(argument))
                                fileOpener.Open(argument);
                            break;

                        case "search":
                        case "s":
                            var results = searchService.Search(argument);

                            if (results == null || results.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" [!] File not found.");
                                Console.ResetColor();
                            }

                 
                            else
                            {
                                for (int i = 0; i < results.Count; i++)
                                {
                                    Console.WriteLine($" {i + 1}. {results[i]}");
                                }

                                Console.Write("\n Open which file? (number or 0 to cancel): ");

                                if (int.TryParse(Console.ReadLine(), out int selected)
                                    && selected > 0
                                    && selected <= results.Count)
                                {
                                    fileOpener.Open(results[selected - 1]);
                                }
                            }
                            break;

                        //Search For application 
                        case "Search app":
                        case "sa":
                            var resultsApp = searchService.AppSearch(argument);
                            if (resultsApp == null || resultsApp.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(" [!] File not found.");
                                Console.ResetColor();
                            }
                            else
                                    {
                                        for (int i = 0; i < resultsApp.Count; i++)
                                        {
                                            Console.WriteLine($" {i + 1}. {resultsApp[i]}");
                                        }

                                        Console.Write("\n Open which file? (number or 0 to cancel): ");

                                        if (int.TryParse(Console.ReadLine(), out int selected)
                                            && selected > 0
                                            && selected <= resultsApp.Count)
                                        {
                                            fileOpener.Open(resultsApp[selected - 1]);
                                        }
                                    }
                            break;

                        case "build":
                            Console.WriteLine(" Building index...");
                            indexService.BuildIndex();
                            indexService.Save();
                            Console.WriteLine($" Index built successfully. [{indexService.GetCount():N0}] files indexed.");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($" [!] Unknown command: '{command}'. Press F1 for help.");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [!] Error: " + e.Message);
                }
            }
        }
    }
}