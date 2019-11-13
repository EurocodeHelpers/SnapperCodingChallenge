using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core.Tests
{
    class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private char[,] snapperImageArray = new char[,]
        {
               //  0   1   2   3   4   5   6   7
                 {' ','0',' ',' ',' ',' ','0','0'},  //0
                 {'0','0',' ',' ',' ',' ','0','0'},  //1
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //2
                 {' ',' ',' ',' ',' ','X',' ',' '},  //3
                 {' ',' ',' ','X',' ','X',' ',' '},  //4
                 {' ',' ',' ','X',' ','X',' ',' '},  //5
                 {' ',' ','X','X','X',' ',' ',' '},  //6
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //7
                 {' ',' ',' ',' ',' ',' ',' ','0'},  //8
                 {' ',' ',' ',' ',' ',' ','0',' '},  //9
        };

        private char[,] targetA = new char[,]
        {
                 {'0','0'},
                 {'0','0'},
        };

        private char[,] targetB = new char[,]
        {
                 {' ',' ',' ','X'},
                 {' ','X','X','X'},
                 {' ','X','X','X'},
                 {'X','X','X','X'},
        };

        [TestCase(0.75, 2)]
        [TestCase(1, 1)]
        public void Verify_Scanner_NumberOfIdentifiedTargetA(double minimumPrecision, int numberOfTargets)
        {
            var snapperImage = new SnapperImageArray("test", snapperImageArray);
            var targetImageList = new List<ITargetImage>()
            {
                 new TargetImageArray("targetA", targetA, ' '),
                 new TargetImageArray("targetB", targetB, ' '),
            };

            var snapperSolver = new SnapperSolver(snapperImage, targetImageList, minimumPrecision);

            int expected = numberOfTargets;
            int actual = snapperSolver.GetListOfScans().Where(scan => scan.TargetImage.Name == "targetA").ToList().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(0.72, 1)]
        [TestCase(0.73, 0)]
        public void Verify_Scanner_NumberOfIdentifiedTargetB(double minimumPrecision, int numberOfTargets)
        {
            var snapperImage = new SnapperImageArray("test", snapperImageArray);

            var targetImageList = new List<ITargetImage>()
            {
                 new TargetImageArray("targetA", targetA, ' '),
                 new TargetImageArray("targetB", targetB, ' '),
            };

            var snapperSolver = new SnapperSolver(snapperImage, targetImageList, minimumPrecision);

            var expected = numberOfTargets;
            int actual = snapperSolver.GetListOfScans().Where(scan => scan.TargetImage.Name == "targetB").ToList().Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Scanner_GlobalCoordinatesOfIdentifiedTargets()
        {
            var snapperImage = new SnapperImageArray("test", snapperImageArray);
            var targetImages = new List<ITargetImage>()
            {
                new TargetImageArray("targetA", targetA, ' '),
                new TargetImageArray("targetB", targetB, ' '),
            };

            var scanner = new SnapperSolver(snapperImage, targetImages, 0.7);

            List<Scan> identifiedTargets = scanner.GetListOfScans();

            var expected = new List<Coordinate>()
             {
                 new Coordinate(0.5, 0.5),
                 new Coordinate(6.5, 0.5),
                 new Coordinate(3.5, 4.5),
             };

            var actual = new List<Coordinate>();

            foreach (Scan scan in identifiedTargets)
            {
                actual.Add(scan.CentroidGlobalCoordinates);
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
