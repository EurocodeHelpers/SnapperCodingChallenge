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
        public List<Scan> Scans = new List<Scan>();

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
                    Scan s = new Scan (SnapperImage, target, j, i, MinimumConfidenceInTargetDetection);
                    Scans.Add(s);
                }
            }
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

    }
}
