using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.NUnit
{
    class ScanTests
    {
        [SetUp]
        public void Setup()
        {
        }

        public char[,] snapperImageArray = new char[,]
        {
                {'X','X',' ',' '},
                {'X','X',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ','X','X'},
                {' ',' ','X','X'},
                {' ',' ',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ',' ',' '},
                {' ','X','X',' '},
                {' ','X',' ',' '},

        };

        public char[,] targetImageArray = new char[,]
        {
                {'X','X'},
                {'X','X'},
        };

        [TestCase(0, 0, 1, true)]
        [TestCase(2, 1, 1, false)]
        [TestCase(1, 9, 0.75, true)]
        [TestCase(1, 9, 1, false)]
        public void Verify_Scan_TargetIdentified(int horizontalOffset, int verticalOffset, double minimumPrecision, bool targetFound)
        {
            var snapperImage = new SnapperImage("test1", snapperImageArray);
            var target = new Target("test1", targetImageArray, ' ');

            Scan scan = new Scan(snapperImage, target, horizontalOffset, verticalOffset, minimumPrecision);

            var expected = targetFound;
            var actual = scan.TargetFound;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(0, 0, 1, 0.5, 0.5)]
        [TestCase(2, 3, 1, 2.5, 3.5)]
        [TestCase(1, 9, 0.75, 1.5, 9.5)]
        public void Verify_Scan_GlobalCoordinates(int horizontalOffset, int verticalOffset, double minimumPrecision, double expectedX, double expectedY)
        {
            var snapperImage = new SnapperImage("test1", snapperImageArray);
            var target = new Target("test1", targetImageArray, ' ');

            Scan scan = new Scan(snapperImage, target, horizontalOffset, verticalOffset, minimumPrecision);

            var expected = new Coordinates(expectedX, expectedY);
            var actual = scan.CentroidGlobalCoordinates;

            Assert.AreEqual(expected, actual);
        }

    }
}
