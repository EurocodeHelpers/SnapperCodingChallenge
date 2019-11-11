using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class SnapperSolver
    {
        public SnapperSolver(SnapperImage snapperImage, List<TargetImage> targetImages, double minimumConfidenceInTargetDetection)
        {
            //Import snapper image, target images and minimumConfidenceInTargetDetection and minimumConfidenceInTargetDetection          
            this.SnapperImage = snapperImage;
            this.TargetImages = targetImages; 
            this.MinimumConfidenceInTargetDetection = minimumConfidenceInTargetDetection;
            
             //Perform initial scan for each targetImage 
            foreach(TargetImage t in targetImages)
            {
                ScanForTarget(t);
            }           

            //Remove the duplicates 
            foreach(TargetImage t in targetImages)
            {
                var scansWithRemovedDuplicatesForTarget = GetListOfNonDuplicateTargets(t);
                _scansTargetFoundDuplicatesRemoved.AddRange(scansWithRemovedDuplicatesForTarget);
            }
        }

        //Properties 
        private SnapperImage SnapperImage { get; }
        private List<TargetImage> TargetImages = new List<TargetImage>(); 
        public double MinimumConfidenceInTargetDetection { get; }   
        private List<Scan> _scans = new List<Scan>();    
        private List<Scan> _scansTargetFound => _scans.Where(x => x.TargetFound == true).ToList();
        private List<Scan> _scansTargetFoundDuplicatesRemoved = new List<Scan>();

        //Methods 
        public List<Scan> GetListOfScans()
        {
           return _scansTargetFoundDuplicatesRemoved;
        }

        public void ScanForTarget(TargetImage target)
        {
            int snapperImageRows = SnapperImage.NumberOfRows;
            int snapperImageColumns = SnapperImage.NumberOfColumns;

            int targetRows = target.NumberOfRows;
            int targetCols = target.NumberOfColumns;

            int maximumHorizontalOffset = snapperImageColumns - targetCols;
            int maximumVerticalOffset = snapperImageRows - targetRows;

            for (int i = 0; i <= maximumVerticalOffset; i++)
            {
                for (int j = 0; j <= maximumHorizontalOffset; j++)
                {
                    Scan s = new Scan(SnapperImage, target, j, i, MinimumConfidenceInTargetDetection);
                    _scans.Add(s);
                }
            }
        }         

        public List<Scan> GetListOfNonDuplicateTargets(TargetImage targetImage)
        {
            List<Scan> scansWithDuplicates = _scans.Where(scan => scan.TargetFound == true && scan.TargetImage.Name == targetImage.Name).ToList();

            // List<string> dump = new List<string>();
            //Note the dimensions of the SnapperImage and Target for conciseness
            int snapperImageRows = SnapperImage.NumberOfRows;
            int snapperImageColumns = SnapperImage.NumberOfColumns;

            int targetImageRows = targetImage.NumberOfRows;
            int targetImageColumns = targetImage.NumberOfColumns;        

            for (int i = 0; i < snapperImageRows - targetImageRows; i++)
            {
                for (int j = 0; j < snapperImageColumns - targetImageColumns; j++)
                {
                    //Get a subarray from the snapperimagearray and look for squares which contain global centroids.                    
                    var subArray = MultiDimensionalCharacterArrayHelpers.GetSubArrayFromArray
                        (SnapperImage.GridRepresentation, targetImage.GridRepresentation, j, i);

                    //For each element in the subarray, look for any targets in targetsFound 
                    List<Scan> potentialDuplicates = new List<Scan>();

                    // dump.Add($"Scanning {i},{j}");
                    
                    for (int k = 0; k < targetImage.NumberOfRows; k++)
                    {
                        for (int m = 0; m < targetImage.NumberOfColumns; m++)
                        {
                            int globalX = j + k;
                            int globalY = i + m;
                            var globalCords = new Coordinates(globalX, globalY);

                            //Look for any targets within targetsFound with matching coordinates, if so add to potentialDuplicates.
                            Scan scan =
                                scansWithDuplicates.Where(x => x.TopLHCornerGlobalCoordinates.X == globalX && x.TopLHCornerGlobalCoordinates.Y == globalY).FirstOrDefault(); ;
                            //dump.Add($"Scanning {globalX},{globalY}");
                            if (scan != null) 
                            {
                                // dump.Add($"Scanning {globalX},{globalY}");
                                potentialDuplicates.Add(scan);
                                // dump.Add($"Match found!");
                            }
                        }
                    }

                    // if (potentialDuplicates.Count > 0)
                    // {
                    //     dump.Add($"Target = {targetImage.Name} Dupes = {potentialDuplicates.Count}, {i},{j}");
                    // }

                    //Sort the duplicates by calculated accuracy in descending order.
                    potentialDuplicates.OrderBy(x => x.ConfidenceInTargetDetection);

                    //If potentialDuplicates.Count > 1, remove all duplicates except for one with highest accuracy/
                    if (potentialDuplicates.Count > 1)
                    {
                        for (int n = 0; n < potentialDuplicates.Count - 1; n++)
                        {
                            scansWithDuplicates.Remove(potentialDuplicates[n]);
                        }
                    }                    
                }
            }

            return scansWithDuplicates;

            // File.AppendAllLines("dumpfiletxtNEW.txt", dump);
        }

        public void WriteOutputFile(string filePath)
        {
            var s = new StringBuilder();

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
            s.AppendLine($"Minimum confidence in target detection: {MinimumConfidenceInTargetDetection}");
            s.AppendLine("");

            int totalNumberOfTargetsIdentified = GetListOfScans().Count;
            s.AppendLine($"Total number of targets detected = {totalNumberOfTargetsIdentified}");

            foreach (TargetImage t in TargetImages)
            {
                s.AppendLine($"Number of {t.Name} detected = {_scansTargetFoundDuplicatesRemoved.Where(scan => scan.TargetImage.Name == t.Name)}");
            }
            s.AppendLine("");

            foreach (Scan scan in _scansTargetFoundDuplicatesRemoved)
            {
                s.AppendLine(scan.ScanSummary());
            }

            string output = s.ToString();

            File.WriteAllText(filePath, output);
        }
        
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
