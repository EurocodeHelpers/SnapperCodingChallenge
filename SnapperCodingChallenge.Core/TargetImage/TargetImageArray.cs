using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class TargetImageArray : ITargetImage
    {
        public TargetImageArray(string name, char[,] array, char blankCharacter)
        {
            this.Name = name;
            this.GridRepresentation = array;
            this.InternalShapeCoordinatesOfTarget
                = ITargetImage.CalculateCoordinatesInsidePerimeterOfObject(this, blankCharacter);
            this.CentroidLocalCoordinates = CalculateLocalCoordinatesOfShapeCentroid(array);
            bool targetOK = ITargetImage.VerifyTargetHasADefinedShape(InternalShapeCoordinatesOfTarget);

            if (!targetOK)
            {
                throw new Exception("Target is not defined by a particular shape - please check input and try again.");
            }
        }

        /// <summary>
        /// The name of the target e.g. Starship, NuclearTorpedo
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The filepath of the textfile used to create the target.
        /// </summary>
        public string FilePath => "N/A - Array hard coded.";

        public char[,] GridRepresentation { get; }

        public Coordinate CentroidLocalCoordinates { get; }

        public List<Coordinate> InternalShapeCoordinatesOfTarget { get; }

        /// <summary>
        /// Returns the local co-ordinates of the target, for example:
        /// 
        /// Local coords {x,y} => 0.5,1.5
        /// 
        ///   0 1  x=> 
        /// 0 X X
        /// 1 X X 
        /// 2 X X 
        /// 3 X X 
        /// 
        /// </summary>
        /// <returns></returns>
        private Coordinate CalculateLocalCoordinatesOfShapeCentroid(char[,] gridRepresentation)
        {
            int numberOfRows = GridRepresentation.GetLength(0);
            int numberOfColumns = GridRepresentation.GetLength(1);

            double numberOfColumnsDbl = Convert.ToDouble(numberOfColumns);
            double numberOfRowsDbl = Convert.ToDouble(numberOfRows);

            double x = (numberOfColumnsDbl - 1) / 2;
            double y = (numberOfRowsDbl - 1) / 2;

            return new Coordinate(x, y);
        }
    }
}
