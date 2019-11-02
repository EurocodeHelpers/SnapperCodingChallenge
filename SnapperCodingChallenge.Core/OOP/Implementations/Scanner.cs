using SnapperCodingChallenge.Core.OOP.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    public class Scanner
    {
        public Scanner(ISnapperImage snapperImage, double minimumPrecision)
        {
            SnapperImage = snapperImage;
            MinimumConfidenceInTargetDetection = minimumPrecision;
        }

        public ISnapperImage SnapperImage { get; }
        public double MinimumConfidenceInTargetDetection { get; }
        public List<TargetIdentified> TargetDetections = new List<TargetIdentified>();

        public void ScanForTarget(ITarget target)
        {
            int mapRows = SnapperImage.GridRepresentation.GetLength(0);
            int mapCols = SnapperImage.GridRepresentation.GetLength(1);

            int targetRows = target.GridRepresentation.GetLength(0);
            int targetCols = target.GridRepresentation.GetLength(1);

            int maxX0 = mapCols - targetCols;
            int maxY0 = mapRows - targetRows;

            for (int i = 0; i <= maxY0; i++)
            {
                for (int j = 0; j <= maxX0; j++)
                {
                    Scan s = new Scan(SnapperImage, target, j, i, MinimumConfidenceInTargetDetection);
                    if (s.TargetFound == true)
                }
            }
        }

        public List<Scan> GetListOfIdentifiedTargets()
        {
            return Scans.Where(x => x.TargetFound == true).ToList();
        }
    }
}
