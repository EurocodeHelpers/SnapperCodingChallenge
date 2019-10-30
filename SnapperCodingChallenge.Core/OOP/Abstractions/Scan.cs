using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core.OOP
{
    public class Scan
    {
        public Scan(ISnapperImage snapperImage, ITarget target, int xOffset, int yOffset, double minimumPrecision)
        {
            SnapperImage = snapperImage;
            Target = target;
            this.XOffset = xOffset;
            this.YOffset = yOffset;
            MinimumPrecision = minimumPrecision;

            ScanImageForTarget();            
        }

        public ISnapperImage SnapperImage{ get; }
        public ITarget Target { get; }
        public int XOffset { get;  }
        public int YOffset { get;  }
        public double MinimumPrecision { get; }

        //Properties
        public Tuple<double, double> TargetCentroidCoordinates { get; private set; }
        public double Matches { get; private set; }
        public double Differences { get; private set; }
        public double CalculatedPrecision { get; private set; }
        public bool TargetFound { get; private set; }

        //Methods
        private void ScanImageForTarget()
        {
            int targetRows = Target.GridRepresentation.GetLength(0);
            int targetCols = Target.GridRepresentation.GetLength(1);

            char[,] slice = MultiDimensionalCharacterArrayHelpers.GetSubArrayFromArray
                ((char[,])SnapperImage.GridRepresentation, XOffset, YOffset, targetRows, targetCols);

            TargetCentroidCoordinates = TupleHelpers.GetSubArrayCentroid(slice, XOffset, YOffset, targetRows, targetCols);

            foreach (Tuple<int, int> coordinate in Target.InternalShapeCoordinatesOfTarget)
            {
                int x = coordinate.Item1;
                int y = coordinate.Item2;

                if (slice[x, y] == Target.GridRepresentation[x, y])
                {
                    Matches++;
                }
                else
                {
                    Differences++;
                }
            }

            CalculatedPrecision = Matches / (Matches + Differences);

            TargetFound = (CalculatedPrecision >= MinimumPrecision) ? true : false;            
        }

        public string ScanSummary()
        {
            if (TargetFound)
            {
                return $"Position {XOffset},{XOffset} - {Target.Name} found with centroid co-ordinates {TargetCentroidCoordinates.Item1}," +
                    $"{TargetCentroidCoordinates.Item2} with a certainty of {100 * Math.Round(CalculatedPrecision,2)}%!";
            }
            else
            {
                return $"Position {XOffset},{YOffset} - No match found...";
            }
        }

    }
}
