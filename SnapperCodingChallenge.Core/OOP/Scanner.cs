using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    public class Scanner
    {
        public Scanner(SnapperImage snapperImage, double minimumConfidenceInTargetDetection)
        {
            SnapperImage = snapperImage;
            MinimumConfidenceInTargetDetection = minimumConfidenceInTargetDetection;
        }

        public SnapperImage SnapperImage { get; }
        public double MinimumConfidenceInTargetDetection { get; }

        public readonly List<Scan> Scans = new List<Scan>();
        public readonly List<Target> TargetsScannedFor = new List<Target>();


        public void ScanForTarget(Target target)
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
                    Scans.Add(s);
                }
            }

            TargetsScannedFor.Add(target);
        }

        public List<Scan> GetListOfIdentifiedTargets()
        {
            return Scans.Where(x => x.TargetFound == true).ToList();
        }

        public List<Scan> GetListOfIdentifiedTargets(Target target)
        {
            return Scans.Where(x => x.TargetFound == true && x.Target.Name == target.Name).ToList();
        }

        public void DumpAllScansToConsole()
        {
            foreach (Scan scan in Scans)
            {
                System.Console.WriteLine(scan.ScanSummary());
            }
        }

        public void RemoveAllDuplicates(List<Scan> scansWhereTargetFound, Target target)
        {
            int snapperImageRows = SnapperImage.NumberOfRows;
            int snapperImageColumns = SnapperImage.NumberOfColumns;

            int targetRows = target.NumberOfRows;
            int targetColumns = target.NumberOfColumns;

            for (int i = 0; i < snapperImageRows - targetRows; i++)
            {
                for (int j = 0; j < snapperImageColumns - targetColumns; j++)
                {
                    //Get a subarray from the snapperimagearray and look for squares which contain global centroids.                    
                    var subArray = MultiDimensionalCharacterArrayHelpers.GetSubArrayFromArray
                        (SnapperImage.GridRepresentation, target.GridRepresentation, j, 0);

                    //For each element in the subarray, look for any targets in targetsFound 
                    List<Scan> potentialDuplicates = new List<Scan>();


                    for (int k = 0; k < target.NumberOfRows; k++)
                    {
                        for (int m = 0; i < target.NumberOfColumns; i++)
                        {
                            int globalX = j + k;
                            int globalY = i + m;

                            var globalCords = new Coordinates(globalX, globalY);

                            //Look for any targets within targetsFound with matching coordinates, if so add to potentialDuplicates.
                            Scan scan =
                                scansWhereTargetFound.Where(x => x.CentroidGlobalCoordinates.X == globalX && x.CentroidGlobalCoordinates.Y == globalY).FirstOrDefault(); ;

                            if (scan != null) { potentialDuplicates.Add(scan); }
                        }
                    }

                    //Sort the duplicates by calculatedAccracy
                    potentialDuplicates.OrderByDescending(x => x.ConfidenceInTargetDetection);

                    for (int n = 1; n < potentialDuplicates.Count; i++)
                    {
                        scansWhereTargetFound.Remove(potentialDuplicates[n]);
                    }
                }
            }

        }




    }
}
