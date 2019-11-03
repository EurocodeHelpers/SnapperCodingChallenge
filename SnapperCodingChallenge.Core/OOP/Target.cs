using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A textfile implementation of the ITarget interface. This class converts a textfile into a 2d multi-dimensional
    /// array of characters using System.IO.File.
    /// </summary>
    public class Target
    {
        public Target(string name, string filePath, char blankCharacter)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = ConvertTxtFileInto2DArray(filePath).TrimArray(blankCharacter);
            this.InternalShapeCoordinatesOfTarget
                = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
        }

        public Target(string name, char[,] array, char blankCharacter)
        {
            this.Name = name;
            this.FilePath = "N/A";
            this.GridRepresentation = array;
            this.InternalShapeCoordinatesOfTarget
                = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
        }

        /// <summary>
        /// The name of the target e.g. Starship, NuclearTorpedo
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The filepath of the textfile used to create the target.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// The 2D multi-dimensional array that models the target, used for the business logic.
        /// </summary>
        public char[,] GridRepresentation { get; }

        /// <summary>
        /// The number of rows for the grid representation of the target.
        /// </summary>
        public int NumberOfRows => GridRepresentation.GetLength(0);

        /// <summary>
        /// The number of columns for the grid representation of the target.
        /// </summary>
        public int NumberOfColumns => GridRepresentation.GetLength(1);

        /// <summary>
        /// The elements within the grid representation of the target, used as a comparison to determine whether a given
        /// slice of the SnapperImage contains a target - for example:
        /// 
        /// {
        ///   00X00
        ///   XXXXX
        ///   0X0X0
        ///   00X00
        /// }
        /// Yields [row,col] => 
        /// {0,2},
        /// {1,0},{1,2},{1,3},{1,4},{1,5}, 
        /// {2,1},{2,2}.{2,3},{2,4}
        /// {3,2}
        /// </summary>
        public List<Coordinates> InternalShapeCoordinatesOfTarget { get; }

        /// <summary>
        /// The coordinates {x,y} describing the centroid of the object.
        /// </summary>
        public Coordinates CentroidLocalCoordinates => CalculateLocalCoordinatesOfShapeCentroid();
        public string GridDimensionsSummary => $"Grid Size [rows,cols] = {NumberOfRows},{NumberOfColumns}";
        public string LocalCoordinatesOfCentroidSummary => $"Local Coordinates of Centroid [x,y] = {CentroidLocalCoordinates.X},{CentroidLocalCoordinates.Y}";

        private char[,] ConvertTxtFileInto2DArray(string filePath)
        {
            //Open the text file and get an array of strings representing each line.
            string[] rows = File.ReadAllLines(filePath);

            //Set the dimensions of the 2D character array.
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

        public Coordinates CalculateGlobalCoordinatesOfShapeCentroid(SnapperImage snapperImage, double horizontalOffset, double verticalOffset)
        {
            return new Coordinates(horizontalOffset + CentroidLocalCoordinates.X, verticalOffset + CentroidLocalCoordinates.Y);
        }

        public Coordinates CalculateLocalCoordinatesOfShapeCentroid()
        {
            double numberOfColumnsDbl = Convert.ToDouble(NumberOfColumns);
            double numberOfRowsDbl = Convert.ToDouble(NumberOfRows);

            double x = (numberOfColumnsDbl - 1) / 2;
            double y = (numberOfRowsDbl - 1) / 2;

            return new Coordinates(x, y);
        }
    }
}
