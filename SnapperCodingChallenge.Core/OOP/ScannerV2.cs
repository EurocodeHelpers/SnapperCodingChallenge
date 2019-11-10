using System.Collections.Generic;
using System.IO;
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

            List<string> dump = new List<string>();
            //Note the dimensions of the SnapperImage and Target for conciseness
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
                        (SnapperImage.GridRepresentation, target.GridRepresentation, j, i);

                    //For each element in the subarray, look for any targets in targetsFound 
                    List<Scan> potentialDuplicates = new List<Scan>();

                    dump.Add($"Scanning {i},{j}");
                    
                    for (int k = 0; k < target.NumberOfRows; k++)
                    {
                        for (int m = 0; m < target.NumberOfColumns; m++)
                        {
                            int globalX = j + k;
                            int globalY = i + m;
                            var globalCords = new Coordinates(globalX, globalY);

                            //Look for any targets within targetsFound with matching coordinates, if so add to potentialDuplicates.
                            Scan scan =
                                scansWhereTargetFound.Where(x => x.TopLHCornerGlobalCoordinates.X == globalX && x.TopLHCornerGlobalCoordinates.Y == globalY && target.Name == x.Target.Name
                                ).FirstOrDefault(); ;
                            //dump.Add($"Scanning {globalX},{globalY}");
                            if (scan != null) 
                            {
                                dump.Add($"Scanning {globalX},{globalY}");
                                potentialDuplicates.Add(scan);
                                dump.Add($"Match found!");
                            }
                        }
                    }

                    if (potentialDuplicates.Count > 0)
                    {
                        dump.Add($"Target = {target.Name} Dupes = {potentialDuplicates.Count}, {i},{j}");
                    }

                    //Sort the duplicates by calculated accuracy in descending order.
                    potentialDuplicates.OrderBy(x => x.ConfidenceInTargetDetection);

                    //If potentialDuplicates.Count > 1, remove all duplicates except for one with highest accuracy/
                    if (potentialDuplicates.Count > 1)
                    {
                        for (int n = 0; n < potentialDuplicates.Count - 1; n++)
                        {
                            scansWhereTargetFound.Remove(potentialDuplicates[n]);
                        }
                    }                    
                }
            }

            File.AppendAllLines("dumpfiletxtNEW.txt", dump);
        }



    }
}
