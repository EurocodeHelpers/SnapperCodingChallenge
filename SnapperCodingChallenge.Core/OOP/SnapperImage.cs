namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A class that models the snapper image as a 2D array of characters.
    /// </summary>
    public class SnapperImage 
    {
        public SnapperImage(string name, string filePath)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = TextFileHelpers.ConvertTxtFileInto2DArray(filePath);
        }

        public SnapperImage(string name, char[,] array)
        {
            this.Name = name;
            this.FilePath = "N/A";
            this.GridRepresentation = array;
        }

        /// <summary>
        /// The name of the snapper image e.g. TestData, Galaxy121 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The filepath of the textfile used to create the snapper image.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// The 2D multi-dimensional array that models the snapper image, used for the business logic.
        /// </summary>
        public char[,] GridRepresentation { get; }

        /// <summary>
        /// The number of rows for the grid representation of the snapper image.
        /// </summary>
        public int NumberOfRows => GridRepresentation.GetLength(0);

        /// <summary>
        /// The number of columns for the grid representation of the snapper image.
        /// </summary>
        public int NumberOfColumns => GridRepresentation.GetLength(1);

        /// <summary>
        /// Returns a string describing the size of the snapper image in terms of a multi-dimensional character array/
        /// </summary>
        public string GridDimensionsSummary => $"Grid Size [rows,cols] = {NumberOfRows},{NumberOfColumns}";

    }
}
