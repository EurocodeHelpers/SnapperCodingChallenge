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
    }
}
