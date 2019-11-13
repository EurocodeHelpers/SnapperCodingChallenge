using SnapperCodingChallenge.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnapperCodingChallenge.ConsoleApplication
{
    /// <summary>
    /// A static container that injects concrete implementations of the TargetImage, SnapperImage, Logger, Output and Settings
    /// Classes. 
    /// </summary>
    public static class Services
    {
        //Analysis Settings
        private const char blankCharacter = ' ';
        private const string dateTimeFormat = "yyyyMMdd_HHmm";

        //Textfile Settings
        private const string snapperImageDirectoryPath = @"ScannerImage";
        private const string targetsDirectoryPath = @"Targets";
        private const string optionsFilePath = @"Options/options.txt";
        private const string fileExtension = "*.blf";
        private static readonly string snapperConsoleLogFilePath = $"SAS Console Log {DateTime.Now.ToString(dateTimeFormat)}.txt";
        private static readonly string snapperConsoleOutputFile = $"SAS Output File {DateTime.Now.ToString(dateTimeFormat)}.txt";

        //TODO - Change to private set
        public static ISnapperImage SnapperImage { get; private set; }
        public static List<ITargetImage> TargetImages { get; private set; }
        public static ILogger Logger { get; private set; }
        public static ILogger Output { get; private set; }
        public static ISettings Settings { get; private set; }

        public static void InitialiseSnapperImage(SnapperImageType type)
        {
            switch (type)
            {
                case SnapperImageType.TextFile:
                    InitialiseSnapperImage_TextFile();
                    break;
                default:
                    break;
            }
        }
        
        private static void InitialiseSnapperImage_TextFile()
        {
            //Get array of absolute filepaths for SnapperImages (although there should only be 1!)
            string[] snapperImageFilePaths =
               DirectoryHelpers.GetFilesWithinDirectoryWithCertainFileExtension(snapperImageDirectoryPath, fileExtension);

            //Handle cases where there isn't exactly 1 .blf file and throw exception if this is the case. 
            if (snapperImageFilePaths.Length != 1 || snapperImageFilePaths == null)
            {
                throw new InvalidProgramException("*ERROR* Invalid Snapper Image data: the directory must contain " +
                    "exactly 1 .blf file.");
            }

            //Get the name of the SnapperImage txt file without file extension. 
            //Get the filepath (only the first string - remember as theres only 1 snapper image!)
            string snapperImageName = Path.GetFileNameWithoutExtension(snapperImageFilePaths[0]);
            string snapperImageFilepath = snapperImageFilePaths[0];

            //Set up the SnapperImage 
            var snapperImage = new SnapperImageTextFile(snapperImageName, snapperImageFilepath);

            SnapperImage = snapperImage;
        }
        
        public static void InitialiseTargetImages(TargetImageType type)
        {
            switch (type)
            {
                case TargetImageType.TextFile:
                    InitialiseTargetImages_TextFile();
                    break;
                default:
                    break;
            }
        }

        private static void InitialiseTargetImages_TextFile()
        {
            //Get array of absolute filepaths for SnapperImages (although there should only be 1! We will throw an exception where this isnt the case
            //to ensure the user is not misled, instead of just taking the first one.)
            string[] targetImageFilePaths =
               DirectoryHelpers.GetFilesWithinDirectoryWithCertainFileExtension(targetsDirectoryPath, fileExtension);

            //Handle cases where there isn't exactly 1 .blf file and throw exception if this is the case. 
            if (targetImageFilePaths.Length < 1 || targetImageFilePaths == null)
            {
                throw new InvalidProgramException("*ERROR* Invalid map data: directory must contain exactly 1 .blf file. ");
            }

            var targetImages = new List<ITargetImage>();

            foreach (string targetImageTextFilePath in targetImageFilePaths)
            {
                string targetName = Path.GetFileNameWithoutExtension(targetImageTextFilePath);
                TargetImageTextFile t = new TargetImageTextFile(targetName, targetImageTextFilePath, blankCharacter);
                targetImages.Add(t);
            }

            TargetImages = targetImages;
        }

        public static void InitialiseLogger(LoggerType type)
        {
            switch (type)
            {
                case LoggerType.Console:
                    Logger = new LoggerConsole();
                    break;
                default:
                    break;
            }
        }

        public static void InitialiseOutput(OutputType type)
        {
            switch (type)
            {
                case OutputType.TextFile:
                    Output = new LoggerTextFile(snapperConsoleOutputFile);
                    break;
                default:
                    break;
            }
        }

        public static void InitialiseOptions(OptionsType type)
        {
            switch (type)
            {
                case OptionsType.TextFile:
                    Settings = new SettingsTextFile(optionsFilePath);
                    break;
                default:
                    break;
            }
        }








    }
}
