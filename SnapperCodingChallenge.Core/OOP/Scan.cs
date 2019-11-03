using System;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A class used to compare a piece of a snapper image with a defined target to determine whether a target
    /// indeed exists.
    /// </summary>
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

        /// <summary>
        /// The snapper image to be scanned.
        /// </summary>
        public SnapperImage SnapperImage { get; }

        /// <summary>
        /// The target we are scanning for.
        /// </summary>
        public Target Target { get; }

        /// <summary>
        /// The horizontal offset from 0,0 for the snapper image which we're scanning for.
        /// </summary>
        public int HorizontalOffset { get; }

        /// <summary>
        /// The vertical offset from 0,0 for the snapper image which we're scanning for.
        /// </summary>
        public int VerticalOffset { get; }
        
        /// <summary>
        /// The minimum match in elements we need to determine a match e.g. 0.7 => 70% minimum match required.
        /// </summary>
        public double MinimumConfidenceInTargetDetection { get; }

        //Properties

        /// <summary>
        /// The global coordinates of the centroid of the target slice.
        /// </summary>
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
