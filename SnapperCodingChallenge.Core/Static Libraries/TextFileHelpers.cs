using System.IO;

namespace SnapperCodingChallenge.Core
{
    public class TextFileHelpers
    {
        //TODO Handle cases where the number of cells for rows are different by throwing an exception.         
        /// <summary>
        /// Parses a text file and outputs a 2D character array indexed by the notion [row,col].
        /// 
        /// For example:
        /// {A,B}
        /// {C,D}
        /// {E,F}
        /// {G,H}
        /// 
        /// array[3,1] = H
        /// </summary>
        /// <param name="filePath">The filepath for the textfile.</param>
        /// <returns></returns>
        public static char[,] ConvertTxtFileInto2DArray(string filePath)
        {
            //Open the text file and get an array of strings representing each line.
            string[] rows = File.ReadAllLines(filePath);

            //Set the dimensions of the 2D character array [rows,cols]
            int numberOfRows = rows.Length;
            int numberOfColumns = rows[0].Length;
            char[,] array = new char[numberOfRows, numberOfColumns]; 
            
            //For each row, convert to character array and set the elements of the 2d char array/

            for (int i = 0; i < numberOfRows; i++)
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

    }
}
