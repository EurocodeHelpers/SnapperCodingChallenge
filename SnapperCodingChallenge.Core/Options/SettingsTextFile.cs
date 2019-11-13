using System;
using System.IO;

namespace SnapperCodingChallenge.Core
{
    public class SettingsTextFile : ISettings
    {
        public SettingsTextFile(string optionsFilePath)
        {
            bool fileExists = File.Exists(optionsFilePath);

            if (fileExists)
            {
                string[] lines = File.ReadAllLines(optionsFilePath);
                string[] s1 = lines[0].Split('=');
                double minimumConfidenceInTargetProtection = 0;
                bool minimumPrecisionSuccessfullyParsed = Double.TryParse(s1[1], out minimumConfidenceInTargetProtection);

                if (minimumPrecisionSuccessfullyParsed)
                {
                    MinimumConfidenceInTargetDetection = minimumConfidenceInTargetProtection;
                }
                else
                {
                    throw new Exception("Options.txt file corrupt - please check input and try again.");
                }                
            }
            else
            {
                throw new Exception("Options.txt file could not be found - please check input and try again.");
            }
        }

        public string OptionsFilePath { get; }
        public double MinimumConfidenceInTargetDetection { get; }
    }
}
