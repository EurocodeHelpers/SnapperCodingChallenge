using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class SnapperSolver
    {
        public SnapperSolver(ISnapperImage snapperImage, List<ITargetImage> targetImages, double minimumConfidenceInTargetPrecision)
        {
            //Import snapper image, target images and minimumConfidenceInTargetDetection and minimumConfidenceInTargetDetection          
            this.SnapperImage = snapperImage;
            this.TargetImages = targetImages; 
            this.MinimumConfidenceInTargetPrecision = minimumConfidenceInTargetPrecision;
            
             //Perform initial scan for each targetImage 
            foreach(ITargetImage t in targetImages)
            {
                ScanForTarget(t);
            }           

            //Remove the duplicates 
            foreach(ITargetImage t in targetImages)
            {
                var scansWithRemovedDuplicatesForTarget = GetListOfNonDuplicateTargets(t);
                _scansTargetFoundDuplicatesRemoved.AddRange(scansWithRemovedDuplicatesForTarget);
            }
        }

        //Properties 
        private ISnapperImage SnapperImage { get; }
        private List<ITargetImage> TargetImages = new List<ITargetImage>(); 
        public double MinimumConfidenceInTargetPrecision { get; }   
        private List<Scan> _rawScans = new List<Scan>();    
        private List<Scan> _scansTargetFoundDuplicatesRemoved = new List<Scan>();

        //Methods 
        public List<Scan> GetListOfScans()
        {
           return _scansTargetFoundDuplicatesRemoved;
        }

        private void ScanForTarget(ITargetImage target)
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
                    Scan s = new Scan(SnapperImage, target, j, i, MinimumConfidenceInTargetPrecision);
                    _rawScans.Add(s);
                }
            }
        }         

        private List<Scan> GetListOfNonDuplicateTargets(ITargetImage targetImage)
        {
            List<Scan> scansWithDuplicates = _rawScans.Where(scan => scan.TargetFound == true && scan.TargetImage.Name == targetImage.Name).ToList();

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
                            if (scan != null) 
                            {
                                potentialDuplicates.Add(scan);
                            }
                        }
                    }

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
        }

        public void SummariseAnalysis(ILogger logger, bool withIntroduction)
        {
           if (withIntroduction)
           {
                logger.WriteLine(@"***********************************************");
                logger.WriteLine(@"* WELCOME TO THE SNAPPER ANALYSIS SYSTEM (SAS)*");
                logger.WriteLine(@"***********************************************");
                logger.WriteLine("");
                logger.WriteLine(@"Developed by: Peter Cox");
                logger.WriteLine(@"Brief by: Bruno Martins");
                logger.WriteLine(@"Github: https://github.com/EurocodeHelpers");
                logger.WriteLine("");
                logger.WriteLine("***Summary***");
                logger.WriteLine($"Date of analysis: {DateTime.Now}");
                logger.WriteLine("");
           }

           int totalNumberOfTargetsIdentified = GetListOfScans().Count;
           logger.WriteLine($"Minimum confidence in target detection: {100*Math.Round(MinimumConfidenceInTargetPrecision, 2)}%");
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
    }
}
