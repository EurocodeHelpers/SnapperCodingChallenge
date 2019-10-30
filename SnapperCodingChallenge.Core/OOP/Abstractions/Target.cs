using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core.OOP
{
    public class Target : ITarget
    {
        public Target(string name, string filePath, char blankCharacter)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = TextFileHelpers.ConvertTxtFileInto2DArray(filePath).TrimArray(blankCharacter);
            this.InternalShapeCoordinatesOfTarget = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
        }

        public string Name { get; }
        public string FilePath { get; }
        public char[,] GridRepresentation { get; }
        public List<Tuple<int, int>> InternalShapeCoordinatesOfTarget { get; }
        public string GridDimensions => $"Grid Size (Rows x Cols = {GridRepresentation.GetLength(0)},{GridRepresentation.GetLength(1)}";
    }
}
