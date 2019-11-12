using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class SnapperSolver
    {
        public SnapperSolver(ISnapperImage snapperImage, List<ITargetImage> targetImages, double minimumConfidenceInTargetDetection)
        {
            //Import snapper image, target images and minimumConfidenceInTargetDetection and minimumConfidenceInTargetDetection          
            this.SnapperImage = snapperImage;
            this.TargetImages = targetImages; 
            this.MinimumConfidenceInTargetDetection = minimumConfidenceInTargetDetection;
            
             //Perform initial scan for each targetImage 
            foreach(TargetImageTextFile t in targetImages)
            {
                ScanForTarget(t);
            }           

            //Remove the duplicates 
            foreach(TargetImageTextFile t in targetImages)
            {
                var scansWithRemovedDuplicatesForTarget = GetListOfNonDuplicateTargets(t);
                _scansTargetFoundDuplicatesRemoved.AddRange(scansWithRemovedDuplicatesForTarget);
            }
        }

        //Properties 
        private ISnapperImage SnapperImage { get; }
        private List<ITargetImage> TargetImages = new List<ITargetImage>(); 
        public double MinimumConfidenceInTargetDetection { get; }   
        private List<Scan> _rawScans = new List<Scan>();    
        private List<Scan> _scansTargetFoundDuplicatesRemoved = new List<Scan>();

        //Methods 
        public List<Scan> GetListOfScans()
        {
           return _scansTargetFoundDuplicatesRemoved;
        }

        private void ScanForTarget(TargetImageTextFile target)
        {
            int snapperImageRows = SnapperImage.GridRepresentation.GetLength(0);
            int snapperImageColumns = SnapperImage.GridRepresentation.GetLength(1); 

            int targetRows = target.GridRepresentation.GetLength(0);
            int targetCols = target.GridRepresentation.GetLength(1);

            int maximumHorizontalOffset = snapperImageColumns - targetCols;
            int maximumVerticalOffset = snapperImageRows - targetRows;

            for (int i = 0; i <= maximumVerticalOffset; i++)
            {
                for (int j = 0; j <= maximumHorizontalOffset; j++)
                {
                    Scan s = new Scan(SnapperImage, target, j, i, MinimumConfidenceInTargetDetection);
                    _rawScans.Add(s);
                }
            }
        }         

        private List<Scan> GetListOfNonDuplicateTargets(TargetImageTextFile targetImage)
        {
            List<Scan> scansWithDuplicates = _rawScans.Where(scan => scan.TargetFound == true && scan.TargetImage.Name == targetImage.Name).ToList();

            // List<string> dump = new List<string>();
            //Note the dimensions of the SnapperImage and Target for conciseness
            int snapperImageRows = SnapperImage.GridRepresentation.GetLength(0);
            int snapperImageColumns= SnapperImage.GridRepresentation.GetLength(1);

            int targetImageRows = targetImage.GridRepresentation.GetLength(0);
            int targetImageColumns = targetImage.GridRepresentation.GetLength(1);

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
                    
                    for (int k = 0; k < targetImageRows; k++)
                    {
                        for (int m = 0; m < targetImageColumns; m++)
                        {
                            int globalX = j + k;
                            int globalY = i + m;
                            var globalCords = new Coordinate(globalX, globalY);

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

                    //If potentialDuplicates.Count > 1, remove all duplicates except for one with highest accuracy/
                    if (potentialDuplicates.Count > 1)
                    {
                        potentialDuplicates = potentialDuplicates.OrderByDescending(x => x.ConfidenceInTargetDetection).ToList();
                        for (int n = 1; n < potentialDuplicates.Count; n++)
                        {
                            scansWithDuplicates.Remove(potentialDuplicates[n]);
                        }
                    }                    
                }
            }

            return scansWithDuplicates;

            // File.AppendAllLines("dumpfiletxtNEW.txt", dump);
        }

        public void SummariseAnalysis(ILogger logger)
        {
            int totalNumberOfTargetsIdentified = GetListOfScans().Count;
           logger.WriteLine($"Total number of targets detected = {totalNumberOfTargetsIdentified}");

            foreach (TargetImageTextFile t in TargetImages)
            {
                logger.WriteLine($"Number of {t.Name}s detected = {_scansTargetFoundDuplicatesRemoved.Where(scan => scan.TargetImage.Name == t.Name).Count()}");
            }
            logger.WriteBlankLine();

            foreach (Scan scan in _scansTargetFoundDuplicatesRemoved)
            {
                logger.WriteLine(scan.ScanSummary());
            }           
        }

        //public void SummariseAnalysis(ILogger logger)
        //{
        //    var s = new StringBuilder();

        //    s.AppendLine(@"***********************************************");
        //    s.AppendLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
        //    s.AppendLine(@"***********************************************");
        //    s.AppendLine("");
        //    s.AppendLine(@"Developed by: Peter Cox");
        //    s.AppendLine(@"Brief by: Bruno Martins");
        //    s.AppendLine(@"Github: https://github.com/EurocodeHelpers");
        //    s.AppendLine("");
        //    s.AppendLine("***Summary***");
        //    s.AppendLine($"Date of analysis: {DateTime.Now}");
        //    s.AppendLine($"Minimum confidence in target detection: {MinimumConfidenceInTargetDetection}");
        //    s.AppendLine("");

        //    int totalNumberOfTargetsIdentified = GetListOfScans().Count;
        //    s.AppendLine($"Total number of targets detected = {totalNumberOfTargetsIdentified}");

        //    foreach (TargetImage t in TargetImages)
        //    {
        //        s.AppendLine($"Number of {t.Name} detected = {_scansTargetFoundDuplicatesRemoved.Where(scan => scan.TargetImage.Name == t.Name)}");
        //    }
        //    s.AppendLine("");

        //    foreach (Scan scan in _scansTargetFoundDuplicatesRemoved)
        //    {
        //        s.AppendLine(scan.ScanSummary());
        //    }

        //    string output = s.ToString();

        //    File.WriteAllText(filePath, output);
        //}

        
    }
}
