using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A static helper library for interacting with multi-dimensional arrays of characters.
    /// </summary>
    public static class MultiDimensionalCharacterArrayHelpers
    {
        /// <summary>
        /// Logs a multi-dimensional array of characters [row,col] to the console
        /// </summary>
        /// <param name="array">The multi-dimensional array of characters [row,col] to be logged to the console.</param>
        public static void Print2DCharacterArrayToConsole(this char[,] array)
        {
            int numberOfRows = array.GetLength(0);
            int numberOfColumns = array.GetLength(1);

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether for a multi-dimensional array [row,col] whether a given column contains only a given character. 
        /// </summary>
        /// <param name="array">The 2D array to check. </param>
        /// <param name="column">The column number (0-based).</param>
        /// <param name="character">The character being checked agaisnt which is considered "blank".</param>
        /// <returns></returns>
        public static bool CheckIfColumnIsBlank(char[,] array, int column, char character)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, column] != character)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifies whether a given row of a 2D matrix [row,col] includes only blank characters
        /// </summary>
        /// <param name="array">The 2D array to check. </param>
        /// <param name="row">The row number (0-based).</param>
        /// <param name="character">The character being checked agaisnt which is considered "blank".</param>
        /// <returns></returns>
        public static bool CheckIfRowIsBlank(char[,] array, int row, char character)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                if (array[row, i] != character)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Removes any rows or columns only containing a specific character, e.g. ' ' or 'X'.
        /// Note this does not trim any blank rows or columns within the main bulk of the array.
        /// For example, using the '0' as the "blank character".
        /// 
        /// {000000}
        /// {0XXXX0}
        /// {000000}        
        /// {00XX00} 
        /// {00XX00}
        /// {000000}
        /// 
        /// =>
        /// 
        /// {XXXX}
        /// {0000}        
        /// {0XX0} 
        /// {0XX0}
        /// 
        /// </summary>
        /// <param name="array">The array to trim empty rows and columns from.</param>
        /// <param name="ch">The "blank" character/</param>        
        /// <returns></returns>
        public static char[,] TrimArray(this char[,] array, char ch)
        {
            //Get max and min column numbers where not blank.
            List<int> nonBlankRows = new List<int>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (MultiDimensionalCharacterArrayHelpers.CheckIfRowIsBlank(array, i, ch) == false)
                {
                    nonBlankRows.Add(i);
                }
            }

            int minNonBlankRow = nonBlankRows.Min();
            int maxNonBlankRow = nonBlankRows.Max();

            //Get max and min row numbers where not blank.
            List<int> nonBlankCols = new List<int>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (MultiDimensionalCharacterArrayHelpers.CheckIfColumnIsBlank(array, j, ch) == false)
                {
                    nonBlankCols.Add(j);
                }
            }

            int minNonBlankCol = nonBlankCols.Min();
            int maxNonBlankCol = nonBlankCols.Max();

            //Copy array between above constraints over to new array to be replaced. 
            int numberOfRowsReqd = maxNonBlankRow - minNonBlankRow + 1;
            int numberOfColsReqd = maxNonBlankCol - minNonBlankCol + 1;

            char[,] trimmedArray = new char[numberOfRowsReqd, numberOfColsReqd];

            //For each row in the matrix
            for (int i = 0; i < numberOfRowsReqd; i++)
            {
                for (int j = 0; j < numberOfColsReqd; j++)
                {
                    trimmedArray[i, j] = array[i + minNonBlankRow, j + minNonBlankCol];
                }
            }

            return trimmedArray;
        }

        public static char[,] GetSubArrayFromArray(char[,] mainArray, char[,] targetArray, int horizontalOffset, int verticalOffset)
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

    }
}
