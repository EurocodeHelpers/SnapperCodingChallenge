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
        private static readonly string snapperConsoleDumpFilePath = $"SAS Console Log {DateTime.Now.ToString(dateTimeFormat)}.txt";

        static void ConfigureServices()
        {
            InitialiseSnapperImage(SnapperImageType.TextFile);
            InitialiseTargetImages(TargetImageType.TextFile);
            InitialiseLogger(LoggerType.Console);
            InitialiseOptions(OptionsType.TextFile);
            InitialiseOutput(OutputType.TextFile);
        }

        static void Main(string[] args)
        {
            //1. Configure services container and inject the concrete dependencies. 
            ConfigureServices();

            //2. Using boilerplate stackoverflow code to echo the console to a dumpfile for record purposes - use new C# 8 using statement.
            using var cc = new EchoConsoleToTextFile(snapperConsoleDumpFilePath);

            //3. Show introductory message                    
            Logger.WriteLine(@"***********************************************");
            Logger.WriteLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
            Logger.WriteLine(@"***********************************************");
            Logger.WriteLine(@"Version: 1.1");
            Logger.WriteLine("");
            Logger.WriteLine(@"Developed by: Peter Cox");
            Logger.WriteLine(@"Brief by: Bruno Martins");
            Logger.WriteLine(@"Source code from: https://github.com/EurocodeHelpers");
            Logger.WriteBlankLine();
            Logger.WriteLine("===============================================");
            Logger.WriteLine("");

            //4. Set options. 
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

            //5. Load the snapper image we will scan for targets. There must be exactly one .blf file in the ScannerImage directory which we will scan. 
            Logger.WriteLine("***Loading Snapper Image****", true);
            Services.SnapperImage.PrintSnapperImageInformation(Logger);
            Logger.WriteLine("***Snapper Image successfully loaded.***", true);

            Logger.WriteBlankLine();
            Logger.WriteLine("===============================================");
            Logger.WriteBlankLine();

            //6. Loading the target images we will scan the snapper image for.
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

            //7. Echo to console that we are now scanning for targets...
            Logger.WriteBlankLine();
            Logger.WriteLine("Now scanning for targets....", true);
            Logger.WriteBlankLine();
            Logger.WriteLine("Scanning complete.", true);
            Logger.WriteBlankLine();

            //8. Scan for each target...
            SnapperSolver snapperSolver = new SnapperSolver(Services.SnapperImage, Services.TargetImages, minimumConfidenceInTargetPrecision);

            //9. Write output to console.
            snapperSolver.SummariseAnalysis(Logger, false);

            //10. Write to output file.
            snapperSolver.SummariseAnalysis(Output, true);

            //11. Terminate program.
            Logger.WriteLine("Analysis complete.", true);
            Logger.ReadLine();
        }
    }
}


