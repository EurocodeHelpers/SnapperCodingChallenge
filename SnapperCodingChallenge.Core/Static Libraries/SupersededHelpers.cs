using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    public static class SupersededHelpers
    {
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


    }
}
