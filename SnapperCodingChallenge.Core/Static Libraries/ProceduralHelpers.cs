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
        public static List<Coordinates> CalculateCoordinatesInsidePerimeterOfObject(char[,] array, char blankCharacter)
        {
            var coords = new List<Coordinates>();

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
                        coords.Add(new Coordinates(i, k));
                    }
                }
            }

            return coords;
        }
    }
}
