using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class TargetTxt : ITarget
    {
        public TargetTxt(string name, string filePath, char blankCharacter)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = TextFileHelpers.ConvertTxtFileInto2DArray(filePath).TrimArray(blankCharacter);
            this.InternalShapeCoordinatesOfTarget = 
                ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
        }

        public string Name { get; }
        public string FilePath { get; }
        public char[,] GridRepresentation { get; }
        public List<Tuple<int, int>> InternalShapeCoordinatesOfTarget { get; }
        public string GridDimensions { get; }
    }
}
