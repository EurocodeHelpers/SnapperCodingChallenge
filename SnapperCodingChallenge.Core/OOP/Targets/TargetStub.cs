using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class TargetStub : ITarget
    {
        public TargetStub(string name, char[,] gridRepresentation, char blankCharacter)
        {
            this.Name = name;
            this.GridRepresentation = gridRepresentation;
            this.InternalShapeCoordinatesOfTarget =
                ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(GridRepresentation, blankCharacter);
        }

        public string Name { get; }
        public string GridDimensions { get; }
        public char[,] GridRepresentation { get; }
        public List<Tuple<int, int>> InternalShapeCoordinatesOfTarget { get; }
    }
}
