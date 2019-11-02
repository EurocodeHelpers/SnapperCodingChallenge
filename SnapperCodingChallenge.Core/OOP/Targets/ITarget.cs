using System;
using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    /// <summary>
    /// An interface for the target object e.g. spaceship, nuclear torpedo. The implementation 
    /// must be able to produce an array of char[,] as this is what the business logic of the program
    /// is based on - whether or not the array is produced via a text file, HTTP request, PNG etc it does
    /// not matter, so long as it satisfies the below requirements!
    /// </summary>
    public interface ITarget
    {
        string Name { get; }
        string GridDimensions => $"Grid Size [Rows,Cols] = {GridRepresentation.GetLength(0)},{GridRepresentation.GetLength(1)}";
        char[,] GridRepresentation { get; }
        List<Tuple<int, int>> InternalShapeCoordinatesOfTarget { get; }        
    }
}