using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SnapperCodingChallenge._Console.Procedural
{
    /// <summary>
    /// A helper class used for interacting and modifying two-dimensional arrays of characters.
    /// </summary>
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

        //TODO Make this an extension method.
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

                int xMinPosition = nonBlankCells.Min();
                int xMaxPosition = nonBlankCells.Max();

                for (int k = xMinPosition; k <= xMaxPosition; k++)
                {
                    coords.Add(new Tuple<int, int>(i, k));
                }
            }

            return coords;
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
        /// <param name="coords"></param>
        public static void PrintListOfTuplesToConsole(List<Tuple<int, int>> coords)
        {
            foreach (Tuple<int, int> tuple in coords)
            {
                Console.WriteLine($"{tuple.Item1},{tuple.Item2}");
            }
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

            return (accuracy > minimumAccuracy);
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


        public static int CalculateNumberofIdentifiedTargets(char[,] map, char[,] target, double minimumAccuracy, char character)
        {
            int targetsIdentified = 0;

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

                    slice = GetSubArrayFromArray(map, j, i, targetRows, targetCols);
                    Print2DCharacterArrayToConsole(slice);

                    bool targetIdentified =
                        VerifyArraysAreIdenticalAgainstKnownCoordinates(slice, target, coordinates, minimumAccuracy);

                    if (targetIdentified == true)
                    {
                        targetsIdentified++;
                        Console.WriteLine($"Target Identified!");
                    }
                }
            }

            return targetsIdentified;
        }
    }
}
