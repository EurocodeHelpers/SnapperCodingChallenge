using System;
using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A helper class used for interacting and modifying two-dimensional arrays of characters.
    /// </summary>
    public static class ProceduralHelpers
    {
       

       
       

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
        public static List<Tuple<int, int>> CalculateCoordinatesInsidePerimeterOfObject(char[,] array, char blankCharacter)
        {
            var coords = new List<Tuple<int, int>>();

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
                        coords.Add(new Tuple<int, int>(i, k));
                    }
                }                
            }

            return coords;
        }

        

        /// <summary>
        /// A method that compares 2No. two-dimensional arrays of characters of equal sizes, against only a series of provided 
        /// coordinates using a supplied accuracy.
        /// 
        /// For example:
        /// 
        /// Matrix A 
        ///  0
        /// 000 
        ///00000 
        ///
        /// Matrix B 
        ///  0 0
        /// 000 
        ///0  00
        ///
        ///There are only two differences (within the pyramid) - the extra 0 in the top right hand corner 
        ///is not counted as its outside the perimeter of the shape.  
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <param name="coordinates"></param>
        /// <param name="minimumAccuracy"></param>
        /// <returns></returns>
        public static bool VerifyArraysAreIdenticalAgainstKnownCoordinates
            (char[,] matrixA, char[,] matrixB, List<Tuple<int, int>> coordinates, double minimumAccuracy)
        {
            double matches = 0;
            double differences = 0;

            foreach (Tuple<int, int> coordinate in coordinates)
            {
                int x = coordinate.Item1;
                int y = coordinate.Item2;

                if (matrixA[x, y] == matrixB[x, y])
                {
                    matches++;
                }
                else
                {
                    differences++;
                }
            }

            double accuracy = matches / (matches + differences);

            return (accuracy >= minimumAccuracy);
        }


       

        public static List<Tuple<int, int>> CalculateCoordinatesOfIdentifiedTargets(char[,] map, char[,] target, double minimumAccuracy, char character)
        {
            var coordsOfTarget = new List<Tuple<int, int>>();

            int mapRows = map.GetLength(0);
            int mapCols = map.GetLength(1);

            int targetRows = target.GetLength(0);
            int targetCols = target.GetLength(1);

            int maxX0 = mapCols - targetCols;
            int maxYo = mapRows - targetRows;

            var coordinates = CalculateCoordinatesInsidePerimeterOfObject(target, character);

            char[,] slice = new char[targetRows, targetCols];

            int sliceCounter = 0;

            for (int i = 0; i <= maxYo ; i++)
            {
                for (int j = 0; j <= maxX0; j++)
                {
                    Console.WriteLine($"Slice {sliceCounter}");
                    Console.WriteLine($"Xo = {j}");
                    Console.WriteLine($"Yo = {i}");

                    slice = MultiDimensionalArrayHelpers.GetSubArrayFromArray(map, j, i, targetRows, targetCols);
                    MultiDimensionalArrayHelpers.Print2DCharacterArrayToConsole(slice);

                    bool targetIdentified =
                        VerifyArraysAreIdenticalAgainstKnownCoordinates(target, slice, coordinates, minimumAccuracy);

                    if (targetIdentified == true)
                    {
                        Console.WriteLine($"Target Identified!");

                        coordsOfTarget.Add(new Tuple<int, int>(j + targetRows / 2, i + targetCols));
                    }
                }
            }

            return coordsOfTarget;
        }

       





    }
}
