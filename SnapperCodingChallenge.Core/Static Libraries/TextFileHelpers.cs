using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

            //TODO Handle cases where the text file contains irregularly long rows e.g. 

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


        /// <summary>
        /// Converts a text file to C# code for an equivelant array e.g.
        /// 
        /// Input
        /// 00000
        /// XXXXX
        /// 00000
        /// 
        /// Output
        /// = new char[]
        /// {
        ///   {'0','0','0','0','0'}
        ///   {'X','X','X','X','X'}
        ///   {'0','0','0','0','0'}
        /// }
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="filePath">The filepath for the textfile.</param>
        public static void ConvertTxtFileToCSharpCodeFor2DArray(string filePath)
        {
            //Open the text file and get an array of strings representing each line.
            string[] rows = File.ReadAllLines(filePath);

            StringBuilder s = new StringBuilder();
            s.AppendLine($"char[,] array = new char[,]");
            s.AppendLine(@"{");

            foreach (string row in rows)
            {
                s.Append(@"{'");
                char[] c = row.ToCharArray();
                foreach (char ch in c)
                {
                    s.Append(ch);
                    s.Append($"','");  
                }

                //We need to trim off the last two characters - hence use a cheat method. 
                int stringLength = s.Length;
                s.Remove(stringLength - 2, 2);
                s.AppendLine(@"},");
            }
            s.AppendLine(@"};");

            s.ToString();

            Console.WriteLine(s);
            

        }

    }
}
