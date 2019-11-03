using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class Scan
    {
        public Scan(SnapperImage snapperImage, Target target, int horizontalOffset, 
            int verticalOffset, double minimumConfidenceInTargetDetection)
        {
            SnapperImage = snapperImage;
            Target = target;
            this.HorizontalOffset = horizontalOffset;
            this.VerticalOffset = verticalOffset;
            MinimumConfidenceInTargetDetection = minimumConfidenceInTargetDetection;

            ScanImageForTarget();
        }

        public SnapperImage SnapperImage { get; }
        public Target Target { get; }
        public int HorizontalOffset { get; }
        public int VerticalOffset { get; }
        public double MinimumConfidenceInTargetDetection { get; }

        //Properties
        public Coordinates CentroidGlobalCoordinates { get; private set; }
        public double Matches { get; private set; }
        public double Differences { get; private set; }
        public double ConfidenceInTargetDetection { get; private set; }
        public bool TargetFound { get; private set; }

        //Methods
        private void ScanImageForTarget()
        {
            //Get a "Slice" of the SnapperImage based on a horiz+vert offset from (0,0) based on dimensions of target
            char[,] slice = GetSubArrayFromArray
                (SnapperImage.GridRepresentation, Target.GridRepresentation, HorizontalOffset, VerticalOffset);

            CentroidGlobalCoordinates = Target.CalculateGlobalCoordinatesOfShapeCentroid(SnapperImage, HorizontalOffset, VerticalOffset);

            foreach (Coordinates coordinate in Target.InternalShapeCoordinatesOfTarget)
            {
                int x = Convert.ToInt32(coordinate.X);
                int y = Convert.ToInt32(coordinate.Y);

                if (slice[x, y] == Target.GridRepresentation[x, y])
                {
                    Matches++;
                }
                else
                {
                    Differences++;
                }
            }

            ConfidenceInTargetDetection = Matches / (Matches + Differences);

            TargetFound = (ConfidenceInTargetDetection >= MinimumConfidenceInTargetDetection) ? true : false;
        }

        private char[,] GetSubArrayFromArray(char[,] mainArray, char[,] targetArray, int horizontalOffset, int verticalOffset)
        {
            char[,] slice = new char[targetArray.GetLength(0), targetArray.GetLength(1)];

            for (int i = 0; i < slice.GetLength(0); i++)
            {
                for (int j = 0; j < slice.GetLength(1); j++)
                {
                    slice[i, j] = mainArray[i + verticalOffset, j + horizontalOffset];
                }
            }

            return slice;
        }

        public string ScanSummary()
        {
            if (TargetFound)
            {
                return $"Position {HorizontalOffset},{VerticalOffset} - {Target.Name} found with centroid co-ordinates [X,Y] {CentroidGlobalCoordinates.X}," +
                    $"{CentroidGlobalCoordinates.Y} with a certainty of {100 * Math.Round(ConfidenceInTargetDetection, 2)}%!";
            }
            else
            {
                return $"Position {HorizontalOffset},{VerticalOffset} - {Target.Name} NOT found with centroid co-ordinates [X,Y] {CentroidGlobalCoordinates.X}," +
                    $"{CentroidGlobalCoordinates.Y}.";
            }
        }

    }
}
