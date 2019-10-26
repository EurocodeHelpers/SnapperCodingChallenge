using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnapperCodingChallenge._Console.Procedural
{
    public static class ProceduralHelpers
    {
        /// <summary>
        /// Converts the contents of a txt file (including blank rows into 2D array [row,column]
        /// </summary>
        /// <param name="txtFile">The path of the txt file.</param>
        /// <returns></returns>
        public static char[,] ConvertTxtFileInto2DArray(string txtFile)
        {
            //Open textfile and get an array of strings representing each lines, and calc reqd size of 2d array 
            string[] rows = File.ReadAllLines(txtFile);

            int rowNumber = rows.Length;
            int colNumber = rows[0].Length;

            //For each string, convert to array and set the appropriate rows in the char array 
            //char[row,col] remember arrays are 0 based. 

            char[,] array = new char[rowNumber, colNumber];

            //For each row in the matrix
            for (int i = 0; i < rowNumber; i++)
            {
                //Convert the ith row into a character array.                     
                char[] charArray = rows[i].ToCharArray();

                //Add each element in the char array to the row under consideration. 
                for (int j = 0; j < charArray.Length; j++)
                {
                    array[i,j] = charArray[j];
                }
            }

            return array;
        }

        /// <summary>
        /// Prints a 2D array of characters to the console.
        /// </summary>
        /// <param name="array">The 2D array of characters to be printed to the console.</param>
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
        /// Determines whether all the cells in a given col of a 2d array of characters contain any non-white space or ''
        /// </summary>
        /// <param name="array">The array to check. </param>
        /// <param name="col">The col number (0-based).</param>
        /// <returns></returns>
        public static bool CheckIfColumnIsBlank(char[,] array, int col, char ch)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i, col] != ch)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines whether all the cells in a given row of a 2d array of characters contain any non-white space or ''
        /// </summary>
        /// <param name="array">The array to check. </param>
        /// <param name="row">The row number (0-based).</param>
        /// <returns></returns>
        public static bool CheckIfRowIsBlank(char[,] array, int row, char ch)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                if (array[row, i] != ch)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Removes any blank rows and columns before/after any non blank ones.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static char[,] TrimArray(char[,] array, char ch)
        {
            //Get max and min column numbers where not blank.
            List<int> nonBlankRows = new List<int>();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (CheckIfRowIsBlank(array, i, ch) == false)
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
                if (CheckIfColumnIsBlank(array, j, ch) == false)
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
                    trimmedArray[i,j] = array[i + minNonBlankRow, j + minNonBlankCol];
                }
            }

            return trimmedArray;
        }











































    }
}
