namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// An array-based implementation of the ISnapperImage interface, in which the user 
    /// directly specifies the array. 
    /// </summary>
    public class SnapperImageArray : ISnapperImage
    {
        public SnapperImageArray(string name, char[,] array)
        {
            this.Name = name;
            this.FilePath = "N/A";
            this.GridRepresentation = array;
        }

        /// <summary>
        /// The name of the SnapperImage e.g. TestData, Galaxy121 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The filepath from which the SnapperImage is derived. 
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// The 2D multi-dimensional array that models the SnapperImage and used to perform the business logic. 
        /// </summary>
        public char[,] GridRepresentation { get; }        
    }
}
