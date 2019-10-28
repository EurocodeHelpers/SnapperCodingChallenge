using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A static helper library for interacting with multi-dimensional arrays of characters.
    /// </summary>
    public static class MultiDimensionalArrayHelpers
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

        //TODO: Provide a worked example.        
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

        //TODO: Provide a worked example and improve documentation.
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
        /// A method that obtains a sub-array based on a specified array and a supplied row and column numbers and offsets. 
        /// 
        /// Example: 
        /// rows = 2, cols = 2, x0=2, y0=1
        /// {A, B, C, D, E}
        /// {F, G, H, I, J}     
        /// {K, L, M, N, O}
        /// 
        /// => {H,I}
        ///    {M,N}        /// 
        /// </summary>
        /// <param name="array">The array from which a sub array is returned. </param>
        /// <param name="x0">The horizontal offset, better defined in the summary graphically</param>
        /// <param name="y0">The vertical offset, better defined in the summary graphically</param>
        /// <param name="rows">The number of rows requested in the subassembly.</param>
        /// <param name="columns">The number of columns requested in the subassembly.</param>
        /// <returns></returns>
        public static char[,] GetSubArrayFromArray(char[,] array, int x0, int y0, int rows, int columns)
        {
            var subArray = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    subArray[i, j] = array[i + y0, j + x0];
                }
            }

            return subArray;
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
                if (MultiDimensionalArrayHelpers.CheckIfRowIsBlank(array, i, ch) == false)
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
                if (MultiDimensionalArrayHelpers.CheckIfColumnIsBlank(array, j, ch) == false)
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




    }
}
