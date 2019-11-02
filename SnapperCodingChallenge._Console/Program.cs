using SnapperCodingChallenge.Core;
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
        public const string optionsFilePath = @"Options/options.txt";
        public const string fileExtension = "*.blf";
        public const char blankCharacter = ' ';

        static void Main(string[] args)
        {
            using (var cc = new EchoConsoleToTextFile("outputDump.txt"))
            {
                Console.WriteLine(@"***********************************************");
                Console.WriteLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
                Console.WriteLine(@"***********************************************");
                Console.WriteLine();
                Console.WriteLine(@"Developer: Peter Cox");
                Console.WriteLine(@"Github: https://github.com/EurocodeHelpers");

                Console.WriteLine();
                Console.WriteLine("===============================================");
                Console.WriteLine();

                //1. Loading options (i.e. double minimumPrecision of Analysis to yield a target and boolean flag for whether analysis should 
                //be paused to check data before conducted. 
                LogToConsoleWithDateTime_WriteLine($"***Setting Options***"); 
                Console.WriteLine();
                Tuple<double, bool> options = ProcessOptionsFile(optionsFilePath);
                double minimumPrecision = options.Item1;
                bool anyKeyToStartAnalysis = options.Item2;

                LogToConsoleWithDateTime_WriteLine($"Minimum precision of analysis set to {options.Item1}");
                LogToConsoleWithDateTime_WriteLine($"Program pause before running scan = {options.Item2}");
                Console.WriteLine();
                LogToConsoleWithDateTime_WriteLine($"***Options Set!***");

                Console.WriteLine();
                Console.WriteLine("===============================================");
                Console.WriteLine();

                //2. Loading the Snapper Image that is to be scanned for targets.
                //There must be exactly one .blf file in the ScannerImage directory which we will scan. 
                //TODO - in V1.1 we will allow users to scan for multiple Snapper Images in a single solver.
                LogToConsoleWithDateTime_WriteLine("***Loading Snapper Image****");
                string[] mapFilePaths = DirectoryHelpers.GetFilesWithiNDirectoryWithCertainFileExtension(mapFileDirectoryFilePath, fileExtension);
                VerifySnapperImageDataPath(mapFilePaths);
                SnapperImageTxt snapperImage = new SnapperImageTxt(Path.GetFileNameWithoutExtension(mapFilePaths[0]), mapFilePaths[0]);
                PrintScannerImageInformation(snapperImage);
                LogToConsoleWithDateTime_WriteLine("***Snapper Image successfully loaded!***");

                Console.WriteLine();
                Console.WriteLine("===============================================");
                Console.WriteLine();

                //3. Loading the Targets that we are scanning the Snapper Image for.
                //There must be at least one .blf file in the Targets directory which we are going to scan for - else quit application.
                LogToConsoleWithDateTime_WriteLine("***Loading Targets to Scan For***");
                string[] targetsFilePaths = 
                    DirectoryHelpers.GetFilesWithiNDirectoryWithCertainFileExtension(targetsFileDirectoryFilePath, fileExtension);
                VerifyTargetsDataPathData(targetsFilePaths);
                List<ITarget> targets = GetListOfTargetsAndEnsureTheyHaveADefinedShape(targetsFilePaths);
                LogToConsoleWithDateTime_WriteLine("***Targets to scan for successfully loaded!***");

                Console.WriteLine();

                //4. Pause program to allow user to review data input prior to commencing scan if anyKeyToStartAnalysis = true.
                if (anyKeyToStartAnalysis)
                {
                    LogToConsoleWithDateTime_WriteLine("Press any key to start scanning after reviewing the above input: ");
                    Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                }

                //5. Echo to console that we are now scanning for targets...
                Console.WriteLine();
                LogToConsoleWithDateTime_Write("Now scanning for targets....");
                Console.WriteLine();
                Console.WriteLine();

                //6. Scan for each target...
                Scanner scanner = new Scanner(snapperImage, minimumPrecision);

                foreach (ITarget t in targets)
                {
                    Console.WriteLine("===============================================");
                    ScanForTargetAndEchoeToConsole(scanner, t);
                    Console.WriteLine();
                }





            }
        }

        public static void VerifySnapperImageDataPath(string[] filePaths)
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
            Console.WriteLine($"{DateTime.Now} {msg}");
        }

        public static void LogToConsoleWithDateTime_Write(string msg)
        {
            Console.Write($"{DateTime.Now} {msg}");
        }

        public static void PrintTargetInformation(TargetTxt t)
        {
            Console.WriteLine();
            Console.WriteLine($"Target Name = {t.Name}");
            Console.WriteLine($"File Path = {t.FilePath}");
            Console.WriteLine(t.GridDimensions);
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(t.GridRepresentation);
        }

        public static void PrintScannerImageInformation(SnapperImageTxt s)
        {
            Console.WriteLine();
            Console.WriteLine($"Scanner Image Name = {s.Name}");
            Console.WriteLine($"File Path = {s.FilePath}");
            Console.WriteLine(s.GridDimensions);
            Console.WriteLine();
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(s.GridRepresentation);
            Console.WriteLine();
        }

        public static List<ITarget> GetListOfTargetsAndEnsureTheyHaveADefinedShape(string[] targetsFilePath)
        {
            List<ITarget> targets = new List<ITarget>();
            foreach (string s in targetsFilePath)
            {
                TargetTxt t = new TargetTxt(Path.GetFileNameWithoutExtension(s), s, blankCharacter);
                targets.Add(t);

                if (t.InternalShapeCoordinatesOfTarget.Count == 0)
                {
                    Console.WriteLine($"{DateTime.Now} *ERROR* Invalid target {t.Name}: file not defined by a shape.");
                    Console.WriteLine($"{DateTime.Now} *ERROR* Please check input and restart the program to proceed.");
                }
                else
                {
                    PrintTargetInformation(t);
                    Console.WriteLine();
                }              
            }

            return targets;
        }

        public static void ScanForTargetAndEchoeToConsole(Scanner scanner, ITarget target)
        {
            LogToConsoleWithDateTime_WriteLine($"Scanning for {target.Name}!");
            scanner.ScanForTarget(target);
            LogToConsoleWithDateTime_WriteLine($"Scanning for {target.Name} complete!");

            var scansOfTargetType = scanner.Scans.Where(x => x.Target.Name == target.Name && x.TargetFound == true).ToList();
            LogToConsoleWithDateTime_WriteLine($"{scansOfTargetType.Count} instances of {target.Name} identified. ");

            foreach (Scan scan in scansOfTargetType)
            {
                if (scan.TargetFound == true)
                {
                    Console.WriteLine(scan.ScanSummary());
                }
            }
        }

        public static Tuple<double, bool> ProcessOptionsFile(string optionsFilePath)
        {
            string[] lines = File.ReadAllLines(optionsFilePath);

            //1. Get minimum precision 
            string[] s1 = lines[0].Split('=');
            double minimumPrecision = 0;
            bool minimumPrecisionSuccessfullyParsed = Double.TryParse(s1[1], out minimumPrecision);

            //2. Get minimum precision 
            string[] s2 = lines[1].Split('=');
            bool anyKeyToStartAnalysis = false;
            bool anyKeyToStartAnalysisSuccessfullyParsed = Boolean.TryParse(s2[1], out anyKeyToStartAnalysis);

            //Check if file has been successfully parsed, if so return as Tuple<double,bool> else throw Exception and quit application...

            if (minimumPrecisionSuccessfullyParsed && anyKeyToStartAnalysisSuccessfullyParsed)
            {
                return new Tuple<double, bool>(minimumPrecision, anyKeyToStartAnalysis);
            }
            else
            {
                throw new ArgumentException("Options file corrupt - please check input and try again.");
            }
        }

    }
}


