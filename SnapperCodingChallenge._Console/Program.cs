using SnapperCodingChallenge.Core;
using SnapperCodingChallenge.Core.Static_Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge._Console
{
    class Program
    {
        public const string mapFileDirectoryFilePath = @"ScannerImage";
        public const string targetsFileDirectoryFilePath = @"Targets";
        public const string optionsFilePath = @"Options/options.txt";
        public const string fileExtension = "*.blf";
        public const char blankCharacter = ' ';
        public const string dateTimeFormat = "yyyyMMdd_HHmm";


        static void Main(string[] args)
        {

            //1. Set up the object which will echo the console to a dumpfile to examine - use new C# 8 inline using statement.
            //File.Create($"SAS Console Log {DateTime.Now.ToString("yyyyMMdd")}.txt");
            using var cc = new EchoConsoleToTextFile($"SAS Console Log {DateTime.Now.ToString(dateTimeFormat)}.txt");
            
            //2. Show introductory message                    
            Console.WriteLine(@"***********************************************");
            Console.WriteLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
            Console.WriteLine(@"***********************************************");
            Console.WriteLine();
            Console.WriteLine(@"Developed by: Peter Cox");
            Console.WriteLine(@"Brief by: Bruno Martins");

            Console.WriteLine(@"Github: https://github.com/EurocodeHelpers");

            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine();

            //2. Setup options using options.txt
            LogToConsoleWithDateTime_WriteLine($"***Setting Options***");
            Console.WriteLine();
            VerifyExistenceOfOptionsFile(optionsFilePath);
            Tuple<double, bool> options = ProcessOptionsFile(optionsFilePath);
            double minimumPrecision = options.Item1;
            bool anyKeyToStartAnalysis = options.Item2;

            Console.WriteLine($"Minimum precision of analysis set to {100*Math.Round(options.Item1, 2)}%");
            Console.WriteLine($"Program pause before running scan = {options.Item2}");
            Console.WriteLine();
            LogToConsoleWithDateTime_WriteLine($"***Options Set!***");

            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine();

            //3. Load the snapper image we will scan for targets. There must be exactly one .blf file 
            //in the ScannerImage directory which we will scan. 
            LogToConsoleWithDateTime_WriteLine("***Loading Snapper Image****");
            string[] mapFilePaths = DirectoryHelpers.GetFilesWithinDirectoryWithCertainFileExtension(mapFileDirectoryFilePath, fileExtension);
            VerifySnapperImageDataPath(mapFilePaths);
            SnapperImage snapperImage = new SnapperImage(Path.GetFileNameWithoutExtension(mapFilePaths[0]), mapFilePaths[0]);
            PrintScannerImageInformation(snapperImage);
            LogToConsoleWithDateTime_WriteLine("***Snapper Image successfully loaded!***");

            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine();

            //4. Loading the Targets that we are scanning the Snapper Image for.
            //There must be at least one .blf file in the Targets directory which we are going to scan for - else quit application.
            LogToConsoleWithDateTime_WriteLine("***Loading Targets***");
            string[] targetsFilePaths =
                DirectoryHelpers.GetFilesWithinDirectoryWithCertainFileExtension(targetsFileDirectoryFilePath, fileExtension);
            VerifyTargetsDataPathData(targetsFilePaths);
            List<Target> targets = GetListOfTargetsAndEnsureTheyHaveADefinedShape(targetsFilePaths);
            LogToConsoleWithDateTime_WriteLine("***Targets to scan for successfully loaded!***");

            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine();

            //5. Pause the console to allow user to review data input prior to analysis, if this has been specified in the options.txt
            //file:
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

            foreach (Target t in targets)
            {
                Console.WriteLine("===============================================");
                ScanForTargetAndEchoeToConsole(scanner, t);
                Console.WriteLine();
            }

            //7. Write to output file.
            WriteOutputFile(scanner, $"SAS Output File {DateTime.Now.ToString(dateTimeFormat)}.txt");

            //8. Press any to key to exit.
            Console.ReadLine();

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

        public static void PrintTargetInformation(Target target)
        {
            Console.WriteLine();
            Console.WriteLine($"Target Name = {target.Name}");
            Console.WriteLine($"File Path = {target.FilePath}");
            Console.WriteLine(target.GridDimensionsSummary);
            Console.WriteLine(target.LocalCoordinatesOfCentroidSummary);
            Console.WriteLine();
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(target.GridRepresentation);
            Console.WriteLine();
        }

        public static void PrintScannerImageInformation(SnapperImage s)
        {
            Console.WriteLine();
            Console.WriteLine($"Scanner Image Name = {s.Name}");
            Console.WriteLine($"File Path = {s.FilePath}");
            Console.WriteLine(s.GridDimensionsSummary);
            Console.WriteLine();
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(s.GridRepresentation);
            Console.WriteLine();
        }

        public static List<Target> GetListOfTargetsAndEnsureTheyHaveADefinedShape(string[] targetsFilePath)
        {
            List<Target> targets = new List<Target>();
            foreach (string s in targetsFilePath)
            {
                Target t = new Target(Path.GetFileNameWithoutExtension(s), s, blankCharacter);
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

        public static void ScanForTargetAndEchoeToConsole(Scanner scanner, Target target)
        {
            LogToConsoleWithDateTime_WriteLine($"Scanning for {target.Name}.");
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

        public static void VerifyExistenceOfOptionsFile(string optionsFilePath)
        {
            if (!File.Exists(optionsFilePath))
            {
                Console.WriteLine();
                Console.WriteLine($"{DateTime.Now} *ERROR* Options.txt file could not be found:");
                Console.WriteLine($"{DateTime.Now} *ERROR* Please check input and try again.");
                Environment.Exit(0);
            }
        }
        
        public static Tuple<double, bool> ProcessOptionsFile(string optionsFilePath)
        {
            bool fileExists = File.Exists(optionsFilePath);
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

        public static void WriteOutputFile(Scanner scanner, string filePath)
        {
            StringBuilder s = new StringBuilder();

            s.AppendLine(@"***********************************************");
            s.AppendLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
            s.AppendLine(@"***********************************************");
            s.AppendLine("");
            s.AppendLine(@"Developed by: Peter Cox");
            s.AppendLine(@"Brief by: Bruno Martins");
            s.AppendLine(@"Github: https://github.com/EurocodeHelpers");
            s.AppendLine("");
            s.AppendLine("***Summary***");
            s.AppendLine($"Date of analysis: {DateTime.Now}");
            s.AppendLine($"Minimum confidence in target detection: {scanner.MinimumConfidenceInTargetDetection}");
            s.AppendLine("");

            int totalNumberOfTargetsIdentified = scanner.GetListOfIdentifiedTargets().Count();
            s.AppendLine($"Total number of targets detected = {scanner.GetListOfIdentifiedTargets().Count}");
            foreach (Target t in scanner.TargetsScannedFor)
            {
                s.AppendLine($"Number of {t.Name} detected = {scanner.GetListOfIdentifiedTargets(t).Count()}");
            }
            s.AppendLine("");

            foreach (Scan scan in scanner.GetListOfIdentifiedTargets())
            {
                s.AppendLine(scan.ScanSummary());
            }

            string output = s.ToString();

            File.WriteAllText(filePath, output);
        }
    }
}


