using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnapperCodingChallenge._Console.Procedural
{
    public static class ProceduralHelpers
    {
        /// <summary>
        /// Parses a text file and outputs a 2D character array, including any rows/columns with empty space [row,col]
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
                    array[i, j] = charArray[j];
                }
            }

            return array;
        }

        /// <summary>
        /// Prints a 2D array to the console, assuming following notation [row,col]
        /// </summary>
        /// <param name="array">The 2D array of characters to be printed to the console [row,col].</param>
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
        /// Verifies whether a given column of a 2D matrix [row,col] includes only blank characters
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

        //TODO: Convert this into an extension method...
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
                    trimmedArray[i, j] = array[i + minNonBlankRow, j + minNonBlankCol];
                }
            }

            return trimmedArray;
        }

        //A method that calculates the characters inside a 2D array that are to be checked. 
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
                    if (array[i,j] != blankCharacter)
                    {
                        nonBlankCells.Add(j);
                    }                    
                }

                int xMinPosition = nonBlankCells.Min();
                int xMaxPosition = nonBlankCells.Max();

                for (int k = xMinPosition; k <= xMaxPosition; k++)
                {
                    coords.Add(new Tuple<int, int>(i, k));
                }
            }

            return coords;
        }

        //A method that prints a list of tuples(x,y) to the console. 
        public static void PrintListOfTuplesToConsole(List<Tuple<int, int>> coords)
        {
            foreach (Tuple<int,int> tuple in coords)
            {
                Console.WriteLine($"{tuple.Item1},{tuple.Item2}");
            }
        }












































    }
}
