using SnapperCodingChallenge.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static SnapperCodingChallenge.ConsoleApplication.Services;

namespace SnapperCodingChallenge.ConsoleApplication
{
    class Program
    {
        private const string dateTimeFormat = "yyyyMMdd_HHmm";
        private static readonly string snapperConsoleLogFilePath = $"SAS Console Log {DateTime.Now.ToString(dateTimeFormat)}.txt";

        static void Main(string[] args)
        {
            //1. Configure services 
            InitialiseSnapperImage(SnapperImageType.TextFile);
            InitialiseTargetImages(TargetImageType.TextFile);
            InitialiseLogger(LoggerType.Console);
            InitialiseOptions(OptionsType.TextFile);

            //1. Set up the object which will echo the console to a dumpfile to examine - use new C# 8 inline using statement.
            using var cc = new EchoConsoleToTextFile(snapperConsoleLogFilePath);

            //2. Show introductory message                    
            Logger.WriteLine(@"***********************************************");
            Logger.WriteLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
            Logger.WriteLine(@"***********************************************");
            Logger.WriteLine("");
            Logger.WriteLine(@"Developed by: Peter Cox");
            Logger.WriteLine(@"Brief by: Bruno Martins");
            Logger.WriteLine(@"Source code from: https://github.com/EurocodeHelpers");
            Logger.WriteBlankLine();
            Logger.WriteLine("===============================================");
            Logger.WriteLine("");

            //3. Setup options using options.txt
            Logger.WriteLine($"***Setting Options***", true);
            Logger.WriteBlankLine();
            double minimumConfidenceInTargetPrecision = Settings.MinimumConfidenceInTargetDetection;
            double minimumConfidenceInTargetPrecision_AsAPercentage = 100 * Math.Round(minimumConfidenceInTargetPrecision, 2);
            Logger.WriteLine($"Minimum precision of analysis set to {minimumConfidenceInTargetPrecision_AsAPercentage}%.");
            Logger.WriteBlankLine();
            Logger.WriteLine($"***Options Set.***", true);

            Logger.WriteBlankLine();
            Logger.WriteLine("===============================================");
            Logger.WriteBlankLine();

            //3. Load the snapper image we will scan for targets. There must be exactly one .blf file in the ScannerImage directory which we will scan. 
            Logger.WriteLine("***Loading Snapper Image****", true);
            Services.SnapperImage.OutputSnapperImageInformation(Logger);
            Logger.WriteLine("***Snapper Image successfully loaded.***", true);

            Logger.WriteBlankLine();
            Logger.WriteLine("===============================================");
            Logger.WriteBlankLine();

            //4. Loading the target images we will scan the snapper image for.
            Logger.WriteLine("***Loading Targets****", true);
            foreach (ITargetImage t in TargetImages)
            {
                t.PrintTargetInformation(Logger);
                Logger.WriteBlankLine();
            }
            Logger.WriteLine("***Target images successfully loaded.***", true);

            Logger.WriteBlankLine();
            Logger.WriteLine("===============================================");
            Logger.WriteBlankLine();

            //5. Echo to console that we are now scanning for targets...
            Logger.WriteBlankLine();
            Logger.WriteLine("Now scanning for targets....", true);
            Logger.WriteBlankLine();
            Logger.WriteLine("Scanning complete.", true);
            Logger.WriteBlankLine();

            //6. Scan for each target...
            SnapperSolver snapperSolver = new SnapperSolver(Services.SnapperImage, Services.TargetImages, minimumConfidenceInTargetPrecision);

            //7. Write output to console.
            snapperSolver.SummariseAnalysis(Logger);

            //7. Write to output file.
            Logger.WriteLine("Analysis complete.", true);
        }

     

       

        //public static List<TargetImage> GetListOfTargetsAndEnsureTheyHaveADefinedShape(string[] targetsFilePath)
        //{
        //    var targets = new List<TargetImage>();
        //    foreach (string s in targetsFilePath)
        //    {
        //        var t = new TargetImage(Path.GetFileNameWithoutExtension(s), s, blankCharacter);
        //        targets.Add(t);

        //        if (t.InternalShapeCoordinatesOfTarget.Count == 0)
        //        {
        //            Console.WriteLine($"{DateTime.Now} *ERROR* Invalid target {t.Name}: file not defined by a shape.");
        //            Console.WriteLine($"{DateTime.Now} *ERROR* Please check input and restart the program to proceed.");
        //        }
        //        else
        //        {
        //            PrintTargetInformation(t);
        //            Console.WriteLine();
        //        }
        //    }

        //    return targets;
        //}

    
    }
}


