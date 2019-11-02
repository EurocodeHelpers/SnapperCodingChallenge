using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core.OOP.Implementations
{
    public class TargetIdentified
    {
        public TargetIdentified(ITarget target, Tuple<double, double> globalCoordinatesOfShapeCentroid, double confidenceInTargetDetection)
        {
            Target = target;
            GlobalCoordinatesOfShapeCentroid = globalCoordinatesOfShapeCentroid;
            ConfidenceInTargetDetection = confidenceInTargetDetection;
        }

        public ITarget Target { get; set; }
        public Tuple<double, double> GlobalCoordinatesOfShapeCentroid { get; set; }
        public double ConfidenceInTargetDetection { get; set; }
    }
}
