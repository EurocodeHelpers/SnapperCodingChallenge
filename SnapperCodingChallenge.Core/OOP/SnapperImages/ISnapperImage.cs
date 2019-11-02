namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// An interface for the Snapper Image. The implementation must be able to produce an array of char[,] 
    /// as this is what the business logic of the program is based on - whether or not the array is produced
    /// via a text file, HTTP request, PNG etc it does not matter, so long as it satisfies the below requirements!
    /// </summary>
    public interface ISnapperImage
    {
        string Name { get; }
        string GridDimensions { get; }
        char[,] GridRepresentation { get; }
    }
}