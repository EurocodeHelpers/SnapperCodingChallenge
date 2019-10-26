using System;
using System.IO;

namespace SnapperCodingChallenge._Console.Procedural
{
    /// <summary>
    /// A class used to define methods 
    /// </summary>
    public static class ProceduralHelpers
    {
        /// <summary>
        /// Converts the contents of a txt file (including blank rows into 2D array [row,column]
        /// </summary>
        /// <param name="txtFile">The path of the txt file.</param>
        /// <returns></returns>
        public static char[,] ConvertTxtFileInto2DArray(string txtFile)
        {
            //TODO Remove any containing white space only from strings[]

            //Open textfile and get an array of strings representing each lines, and calc reqd size of 2d array 
            string[] rows = File.ReadAllLines(txtFile);
            int xSize = rows[0].Length - 1;
            int ySize = rows.Length;

            //For each string, convert to array and set the appropriate rows in the char array 
            //char[row,col] remember arrays are 0 based. 

            char[,] array = new char[xSize, ySize];

            //For each row in the matrix
            for (int i = 0; i < ySize; i++)
            {
                //Convert the ith row into a character array.                     
                char[] charArray = rows[i].ToCharArray();

                //Add each element in the char array to the row under consideration. 
                for (int j = 0; j < xSize; j++)
                {
                    array[j, i] = charArray[j];
                }
            }

            return array;
        }

        /// <summary>
        /// Prints a 2D array of characters to the console.
        /// </summary>
        /// <param name="array">The 2D array of characters to be printed to the console.</param>
        public static void Print2DArrayToConsole(this char[,] array)
        {
            int xSize = array.GetLength(0);
            int ySize = array.GetLength(1);

            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    Console.Write(array[j, i]);
                }
                Console.WriteLine();
            }
        }


    }
}
