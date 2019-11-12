using System;
using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    public interface ITargetImage
    {
        string Name { get; }
        string FilePath { get; }
        char[,] GridRepresentation { get; }
        Coordinate CentroidLocalCoordinates { get; }
        List<Coordinate> InternalShapeCoordinatesOfTarget { get; }

        /// <summary>
        /// Returns the local co-ordinates of the target, for example:
        /// 
        /// Local coords {x,y} => 0.5,1.5
        /// 
        ///   0 1  x=> 
        /// 0 X X
        /// 1 X X 
        /// 2 X X 
        /// 3 X X 
        /// 
        /// </summary>
        /// <returns></returns>
        public static Coordinate CalculateLocalCoordinatesOfShapeCentroid(ITargetImage targetImage)
        {
            int numberOfRows = targetImage.GridRepresentation.GetLength(0);
            int numberOfColumns = targetImage.GridRepresentation.GetLength(1);

            double numberOfColumnsDbl = Convert.ToDouble(numberOfColumns);
            double numberOfRowsDbl = Convert.ToDouble(numberOfRows);

            double x = (numberOfColumnsDbl - 1) / 2;
            double y = (numberOfRowsDbl - 1) / 2;

            return new Coordinate(x, y);
        }

        /// <summary>
        /// Returns the global co-ordinates of the target relative to a snapperimage, for example:
        /// 
        /// Horizontal Offset = 5
        /// Vertical Offset = 2
        /// Global coords {x,y} => 5.5,2.5
        /// 
        ///  0 1 2 3 4 5 6 
        /// 0
        /// 1
        /// 2          X X 
        /// 3          X X 
        /// 4
        /// 5
        /// 6
        /// </summary>
        /// <param name="snapperImage">The snapper image</param>
        /// <param name="horizontalOffset">"Horizontal "distance" from (0,0) square.</param>
        /// <param name="verticalOffset">"Vertical "distance" from (0,0) square.</param>
        /// <returns></returns>
        public static Coordinate CalculateGlobalCoordinatesOfShapeCentroid(ITargetImage snapperImage, double horizontalOffset, double verticalOffset)
        {
            return new Coordinate(horizontalOffset + snapperImage.CentroidLocalCoordinates.X, verticalOffset + snapperImage.CentroidLocalCoordinates.Y);
        }

        /// <summary>
        /// A method that calculates the coordinates of a shape within a 2D array of characters. For example:
        /// 
        ///  0
        /// 000 
        ///00000 
        ///
        /// Would return (0,2), (1,1), (1,3), (1,5), (2,0),(2,1),(2,2),(2,3),(2,4),
        /// </summary>
        /// <param name="coords">The coordinates that define the internal area of the shape within the 2D Array</param>
        public static List<Coordinate> CalculateCoordinatesInsidePerimeterOfObject(ITargetImage targetImage, char blankCharacter)
        {
            var coords = new List<Coordinate>();
            var array = targetImage.GridRepresentation;
            //1. For each row, get first and last non-blank index maintaining any blank chars sandwiched between first and last. 

            //For each row in the array
            for (int i = 0; i < array.GetLength(0); i++)
            {
                List<int> nonBlankCells = new List<int>();

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] != blankCharacter)
                    {
                        nonBlankCells.Add(j);
                    }
                }

                //If number of cells != 0 add a tuple for each x-ordinate.
                if (nonBlankCells.Count != 0)
                {
                    int xMinPosition = nonBlankCells.Min();
                    int xMaxPosition = nonBlankCells.Max();

                    for (int k = xMinPosition; k <= xMaxPosition; k++)
                    {
                        coords.Add(new Coordinate(i, k));
                    }
                }
            }

            return coords;
        }

        public void PrintTargetInformation(ILogger logger)
        {
            logger.WriteBlankLine();
            logger.WriteLine($"Target Name = {Name}");
            logger.WriteLine($"File Path = {FilePath}");
            logger.WriteLine($"Grid Size from 0,0 [Rows,Cols] = {GridRepresentation.GetLength(0)}, {GridRepresentation.GetLength(1)}");
            logger.WriteLine($"Local Coordinates of Centroid from 0,0 [Row,Col] = {CentroidLocalCoordinates.X},{CentroidLocalCoordinates.Y}");
            logger.WriteBlankLine();
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(GridRepresentation);
            logger.WriteBlankLine();
        }

    }
}