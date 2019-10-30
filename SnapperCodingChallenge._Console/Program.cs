using SnapperCodingChallenge.Core;
using SnapperCodingChallenge.Core.OOP;
using SnapperCodingChallenge.Core.OOP.Logging;
using SnapperCodingChallenge.Core.Static_Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnapperCodingChallenge._Console
{
    class Program
    {
        public const string mapFileDirectoryFilePath = @"ScannerImage";
        public const string targetsFileDirectoryFilePath = @"Targets";
        public const string fileExtension = "*.blf";
        public const char blankCharacter = ' ';


        static void Main(string[] args)
        {
            using (var cc = new ConsoleCopy("outputDump.txt"))
            {
                Console.WriteLine(@"***********************************************");
                Console.WriteLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
                Console.WriteLine(@"***********************************************");
                Console.WriteLine();
                Console.WriteLine();

                //1. Get the filepaths for the map and targets as an array of strings[]
                var mapFilePaths = DirectoryHelpers.GetFilesWithiNDirectoryWithCertainFileExtension(mapFileDirectoryFilePath, fileExtension);
                var targetsFilePaths = DirectoryHelpers.GetFilesWithiNDirectoryWithCertainFileExtension(targetsFileDirectoryFilePath, fileExtension);

                //2. There should be exactly one .blf file in the Map directory which we are going to scan.
                LogToConsoleWithDateTime_WriteLine("Loading Map Data.");
                VerifyMapDataPathData(mapFilePaths);
                ISnapperImage img = new SnapperImage("Map", mapFilePaths[0]);
                PrintScannerImageInformation(img);
                LogToConsoleWithDateTime_WriteLine("Map data successfully loaded!");


                Console.WriteLine();

                //3. There must be at least one .blf file in the Targets directory which we are going to scan for. 
                LogToConsoleWithDateTime_WriteLine("Loading Targets to Scan For.");
                VerifyTargetsDataPathData(targetsFilePaths);
                List<ITarget> targets = new List<ITarget>();
                foreach (string s in targetsFilePaths)
                {
                    Target t = new Target(Path.GetFileNameWithoutExtension(s), s, blankCharacter);
                    targets.Add(t);
                    PrintTargetInformation(t);
                    Console.WriteLine();
                }

                LogToConsoleWithDateTime_WriteLine("Targets to scan for successfully loaded!");

                //4. We must specify the minimum acceptable precision to assume that a target has been identified:
                Console.WriteLine();
                LogToConsoleWithDateTime_Write("Specify the minimum precision to scan for targets with:");
                //double minimumPrecision = double.Parse(Console.ReadLine());
                double minimumPrecision = 0.7;


                //5. Report that we are now scanning for targets...
                Console.WriteLine();
                LogToConsoleWithDateTime_Write("Now scanning for targets....");

                //6. For each target, perform a scan on the map....
                Scanner scanner = new Scanner(img, minimumPrecision);

                foreach (ITarget t in targets)
                {
                    Console.WriteLine($"Scanning for {t.Name}");
                    scanner.ScanForTarget(t);
                    Console.WriteLine("Scanning complete");
                    foreach (Scan scan in scanner.Scans)
                    {
                        if (scan.Target.Name == t.Name && scan.TargetFound == true)
                        {
                            Console.WriteLine(scan.ScanSummary());
                        }
                    }
                }
            }
        }

        public static void VerifyMapDataPathData(string[] filePaths)
        {
            if (filePaths.Length != 1 || filePaths == null)
            {
                Console.WriteLine();
                Console.WriteLine($"{DateTime.Now} *ERROR* Invalid map data: directory must contain exactly 1 .blf file.");
                Console.WriteLine($"{DateTime.Now} *ERROR* Please check input and restart the program to proceed.");
                Environment.Exit(0);
            }
        }

        public static void VerifyTargetsDataPathData(string[] filePaths)
        {
            if (filePaths.Length < 1 || filePaths == null)
            {
                Console.WriteLine();
                Console.WriteLine($"{DateTime.Now} *ERROR* Invalid targets data: directory must contain at least 1 .blf file to scan for.");
                Console.WriteLine($"{DateTime.Now} *ERROR* Please check input and restart the program to proceed.");
                Environment.Exit(0);
            }
        }

        public static void LogToConsoleWithDateTime_WriteLine(string msg)
        {
            Console.WriteLine($"{DateTime.Now} {msg}.");
        }

        public static void LogToConsoleWithDateTime_Write(string msg)
        {
            Console.Write($"{DateTime.Now} {msg}.");
        }

        public static void PrintTargetInformation(Target t)
        {
            Console.WriteLine();
            Console.WriteLine($"Target Name = {t.Name}");
            Console.WriteLine($"File Path = {t.FilePath}");
            Console.WriteLine(t.GridDimensions);
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(t.GridRepresentation);
        }

        public static void PrintScannerImageInformation(ISnapperImage s)
        {
            Console.WriteLine();
            Console.WriteLine($"Scanner Image Name = {s.Name}");
            Console.WriteLine($"File Path = {s.FilePath}");
            Console.WriteLine(s.GridDimensions);
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(s.GridRepresentation);
        }
    }
}


