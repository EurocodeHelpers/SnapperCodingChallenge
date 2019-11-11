using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System.Collections.Generic;

namespace SnapperCodingChallenge.Core.Tests
{
    class TargetImageTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private char[,] snapperImageArray = new char[,]
        {
              //   0   1   2   3   4   5 | 6   7
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //0
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //1
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //2
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //3
                 {' ',' ',' ',' ',' ',' ',' ',' '},  //4
                 {' ',' ',' ',' ','X','X','X','X'},  //5
                 {' ',' ',' ',' ',' ','X','X',' '},  //6
                 {' ',' ',' ',' ',' ','X','X',' '},  //7|
                 {' ',' ',' ',' ','X','X','X','X'},  //8|
                 {' ',' ',' ',' ','X','X','X','X'},  //9
                 {' ',' ',' ',' ','X',' ',' ','X'},  //10

        };

        private char[,] targetArray = new char[,]
        {
                 {'X','X','X','X'},
                 {' ','X','X',' '},
                 {' ','X','X',' '},
                 {'X','X','X','X'},
                 {'X','X','X','X'},
                 {'X',' ',' ','X'},
        };

        [Test]
        public void Verify_Target_Test1_ParsingTxtFile1()
        {
            //var originalArray = new char[,]
            //    {
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','1','1','1','1','X','1','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','1','1','1','1','X','1','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //             {'X','X','X','X','X','X','X','X','X','X','X'},
            //    };

            var expected = new char[,]
                {
                          {'1','1','1','1','X','1'},
                          {'X','X','X','X','X','X'},
                          {'X','X','X','X','X','X'},
                          {'X','X','X','X','X','X'},
                          {'1','1','1','1','X','1'},
                };




            var target = new TargetImage("testfile1", @"OOPTests/TestFile1.txt", 'X');

            var actual = target.GridRepresentation;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test2_ParsingTxtFile1()
        {
            /*
             * 
             * XXXXXXXXX
               XXXX1XXXX
               XXX111XXX
               XX11X11XX
               XXXXXXXXX
               XXXXXXXXX
             * 
             */

            var expected = new char[,]
                {
                      {'X','X','1','X','X'},
                      {'X','1','1','1','X'},
                      {'1','1','X','1','1'},
                };

            var target = new TargetImage("testfile1", @"OOPTests/TestFile2.txt", 'X');

            var actual = target.GridRepresentation;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateNumberOfRows()
        {
            var target = new TargetImage("test1", targetArray, ' ');

            var expected = 6;
            var actual = target.NumberOfRows;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateNumberOfColumns()
        {
            var target = new TargetImage("test1", targetArray, ' ');

            var expected = 4;
            var actual = target.NumberOfColumns;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateInternalShapeCoodinatesOfTarget()
        {
            var target = new TargetImage("test1", targetArray, ' ');

            var expected = new List<Coordinates>()
             {
                 new Coordinates(0, 0),
                 new Coordinates(0, 1),
                 new Coordinates(0, 2),
                 new Coordinates(0, 3),

                 new Coordinates(1, 1),
                 new Coordinates(1, 2),

                 new Coordinates(2, 1),
                 new Coordinates(2, 2),

                 new Coordinates(3, 0),
                 new Coordinates(3, 1),
                 new Coordinates(3, 2),
                 new Coordinates(3, 3),

                 new Coordinates(4, 0),
                 new Coordinates(4, 1),
                 new Coordinates(4, 2),
                 new Coordinates(4, 3),

                 new Coordinates(5, 0),
                 new Coordinates(5, 1),
                 new Coordinates(5, 2),
                 new Coordinates(5, 3),
             };

            var actual = target.InternalShapeCoordinatesOfTarget;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateCentroidLocalCoordinates()
        {
            var target = new TargetImage("test1", targetArray, ' ');

            var expected = new Coordinates(1.5, 2.5);

            var actual = target.CentroidLocalCoordinates;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateGlobalCoodinates()
        {
            var snapper = new SnapperImage("map", snapperImageArray);
            var target = new TargetImage("test1", targetArray, ' ');

            var expected = new Coordinates(5.5, 7.5);

            var actual = target.CalculateGlobalCoordinatesOfShapeCentroid(snapper, 4, 5);

            Assert.AreEqual(expected, actual);
        }




    }
}
