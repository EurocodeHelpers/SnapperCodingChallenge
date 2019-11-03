using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System;

namespace SnapperCodingChallenge.NUnit
{
    public class CalculateSubArrayCentroidCoordinatesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Verify_CalculateSubArrayCentroidCoordinates1()
        {
            char[,] array = new char[4, 5]
            {
                {'0','0','0','0','0'},
                {'0','0','0','1','1'},
                {'0','0','0','1','1'},
                {'0','0','0','0','0'},
            };

            Tuple<double, double> expected =
                new Tuple<double, double>(3.5, 1.5);

            Tuple<double, double> actual =
                TupleHelpers.GetSubArrayCentroid(array, 3, 1, 2, 2);

            Assert.AreEqual(expected, actual);
        }
    }
}
