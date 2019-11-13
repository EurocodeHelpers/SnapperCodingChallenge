namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// A class that models the snapper image as a 2D array of characters.
    /// </summary>
    public class SnapperImageTextFile : ISnapperImage
    {
        public SnapperImageTextFile(string name, string filePath)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = TextFileHelpers.ConvertTxtFileInto2DArray(filePath);
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
    }
}
