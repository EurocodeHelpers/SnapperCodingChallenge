using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A class that models a target object e.g. starship, nuclear torpedo as a 2D array of characters.
    /// </summary>
    public class Target
    {
        public Target(string name, string filePath, char blankCharacter)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = ConvertTxtFileInto2DArray(filePath).TrimArray(blankCharacter);
            this.InternalShapeCoordinatesOfTarget = CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
        }

        public Target(string name, char[,] array, char blankCharacter)
        {
            this.Name = name;
            this.FilePath = "N/A";
            this.GridRepresentation = array;
            this.InternalShapeCoordinatesOfTarget
                = CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
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
        /// The coordinates {x,y} describing the centroid of the object. relative to the local {0,0} square.
        /// For example:
        /// 
        ///  0 1|2 3 
        /// 0X X X X
        /// 1X X X X  
        /// 2X X X X
        /// 3X X X X
        /// 4X X X X
        /// 
        /// The "centroid" of the shape {x,y} from 0,0 square is 1.5,2.5
        /// </summary>
        public Coordinates CentroidLocalCoordinates => CalculateLocalCoordinatesOfShapeCentroid();

        /// <summary>
        /// Returns a string describing the size of the target in terms of a multi-dimensional character array/
        /// </summary>
        public string GridDimensionsSummary => $"Grid Size [rows,cols] = {NumberOfRows},{NumberOfColumns}";

        /// <summary>
        /// Returns a string describing the local co-ordinates of the target in terms of a multi-dimensional character array.
        /// </summary>
        public string LocalCoordinatesOfCentroidSummary => $"Local Coordinates of Centroid [x,y] = {CentroidLocalCoordinates.X},{CentroidLocalCoordinates.Y}";

        /// <summary>
        /// Takes a textfile and converts it into a 2D array of characters.
        /// </summary>
        /// <param name="filePath">The filepath for the textfile.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the local co-ordinates of the target, for example:
        /// 
        /// Local coords {x,y} => 0.5,1.5
        /// 
        ///   0 1  x=> 
        /// 0 X X
        /// 1 X X 
        /// 2 X X 
        /// 3 X X 
        /// 
        /// </summary>
        /// <returns></returns>
        public Coordinates CalculateLocalCoordinatesOfShapeCentroid()
        {
            double numberOfColumnsDbl = Convert.ToDouble(NumberOfColumns);
            double numberOfRowsDbl = Convert.ToDouble(NumberOfRows);

            double x = (numberOfColumnsDbl - 1) / 2;
            double y = (numberOfRowsDbl - 1) / 2;

            return new Coordinates(x, y);
        }

        /// <summary>
        /// Returns the global co-ordinates of the target relative to a snapperimage, for example:
        /// 
        /// Horizontal Offset = 5
        /// Vertical Offset = 2
        /// Global coords {x,y} => 5.5,2.5
        /// 
        ///  0 1 2 3 4 5 6 
        /// 0
        /// 1
        /// 2          X X 
        /// 3          X X 
        /// 4
        /// 5
        /// 6
        /// </summary>
        /// <param name="snapperImage">The snapper image</param>
        /// <param name="horizontalOffset">"Horizontal "distance" from (0,0) square.</param>
        /// <param name="verticalOffset">"Vertical "distance" from (0,0) square.</param>
        /// <returns></returns>
        public Coordinates CalculateGlobalCoordinatesOfShapeCentroid(SnapperImage snapperImage, double horizontalOffset, double verticalOffset)
        {
            return new Coordinates(horizontalOffset + CentroidLocalCoordinates.X, verticalOffset + CentroidLocalCoordinates.Y);
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
        public static List<Coordinates> CalculateCoordinatesInsidePerimeterOfObject(char[,] array, char blankCharacter)
        {
            var coords = new List<Coordinates>();

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

                //If number of cells != 0 add a tuple for each x-ordinate.
                if (nonBlankCells.Count != 0)
                {
                    int xMinPosition = nonBlankCells.Min();
                    int xMaxPosition = nonBlankCells.Max();

                    for (int k = xMinPosition; k <= xMaxPosition; k++)
                    {
                        coords.Add(new Coordinates(i, k));
                    }
                }
            }

            return coords;
        }





    }
}
