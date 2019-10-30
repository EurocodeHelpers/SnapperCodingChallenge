using System;
using System.Collections.Generic;

namespace SnapperCodingChallenge.Core.OOP
{
    /// <summary>
    /// An interface for the target object e.g. spaceship, nuclear torpedo. The implementation 
    /// must be able to produce an array of char[,] as this is what the business logic of the program
    /// is based on - whether or not the array is produced via a text file, HTTP request, PNG etc it does
    /// not matter, so long as it satisfies the below requirements!
    /// </summary>
    public interface ITarget
    {
        string GridDimensions { get; }
        char[,] GridRepresentation { get; }
        List<Tuple<int, int>> InternalShapeCoordinatesOfTarget { get; }
        string Name { get; }
    }
}