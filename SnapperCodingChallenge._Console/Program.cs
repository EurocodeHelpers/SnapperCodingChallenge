using SnapperCodingChallenge.Core;
using SnapperCodingChallenge.Core.OOP;
using SnapperCodingChallenge.Core.Static_Libraries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SnapperCodingChallenge._Console
{
    class Program
    {
        public const string mapFileDirectoryFilePath = @"Map";
        public const string targetsFileDirectoryFilePath = @"Targets";
        public const string fileExtension = "*.blf";
        public const char blankCharacter = ' ';


        static void Main(string[] args)
        {
            Console.WriteLine("==========================================");
            Console.WriteLine(@"========WELCOME TO ""THE SNAPPER""=======");
            Console.WriteLine("==========================================");
            Console.WriteLine("==========================================");
            Console.WriteLine();
            Console.WriteLine();

            //1. Look in the "Map folder" for a ".blf" file => if cannot be found, quit the application. 

            Console.WriteLine("==========================================");
            Console.WriteLine("=========NOW LOADING GRID DATA============");
            Console.WriteLine("==========================================");

            List<string> mapString = Directory.GetFiles(mapFileDirectoryFilePath, fileExtension, SearchOption.AllDirectories).ToList();
            if (mapString.Count != 1)
            {
                Console.WriteLine($"Error - 0 or >1 {fileExtension} files found in the map directory to analyse.");
                Console.WriteLine("Please check input and try again.");
                Environment.Exit(0);
            }

            Target map = new Target("Map", mapString[0], blankCharacter);
            map.PrintTargetInformation();

            //2. Loading targets 

            Console.WriteLine("=========================================");
            Console.WriteLine("====NOW LOADING TARGETS TO BE SCANNED====");
            Console.WriteLine("=========================================");

            List<string> targetStrings = Directory.GetFiles(targetsFileDirectoryFilePath, fileExtension, SearchOption.AllDirectories).ToList();
            if (mapString.Count < 1)
            {
                Console.WriteLine($"Error - 0 t{fileExtension} files .");
                Console.WriteLine("Please check input and try again.");
                Environment.Exit(0);
            }

            List<Target> targets = new List<Target>();
            foreach (string targetFile in targetStrings)
            {
                Target t = new Target(targetFile, targetFile, blankCharacter);
                targets.Add(t);
                t.PrintTargetInformation();
            }

            //3. Specify minimum accuracy to be considered a target.

            Console.WriteLine();

            Console.WriteLine($"======================================================");
            Console.WriteLine($" SPECIFY MINIMUM PRECISION FOR TARGET TO BE IDENTIFID ");
            Console.WriteLine($"======================================================");
            
            double minimumPrecision = double.Parse(Console.ReadLine());

            //4. Running the analysis: 

            Scanner s = new Scanner(map, minimumPrecision, targets);

            s.ScanSnapperImage();
            Console.ReadLine();

            /*
             * Scanning: {0-10} for {Target}
             * 
             * {Target Not found => 70% Match. 
             * 
             * OR
             * 
             * {Target found!}
             * 
             * 
             * 
             * 













        }



    }
}
