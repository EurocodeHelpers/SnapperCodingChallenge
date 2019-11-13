namespace SnapperCodingChallenge.Core
{
    public interface ISnapperImage
    {
        string Name { get; }
        string FilePath { get; }
        char[,] GridRepresentation { get; }

        /// <summary>
        /// Returns a string describing the size of the snapper image in terms of a multi-dimensional character array/
        /// </summary>
        void PrintSnapperImageInformation(ILogger logger)
        {
            logger.WriteBlankLine();
            logger.WriteLine($"Scanner Image Name = {Name}");
            logger.WriteLine($"File Path = {FilePath}");
            logger.WriteLine($"Grid Size [Rows,Cols] = {GridRepresentation.GetLength(0)}, {GridRepresentation.GetLength(1)}");
            logger.WriteBlankLine();
            MultiDimensionalCharacterArrayHelpers.Print2DCharacterArrayToConsole(GridRepresentation);
            logger.WriteBlankLine();
        }
    }
}