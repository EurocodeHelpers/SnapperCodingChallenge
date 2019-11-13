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
            //    {Original Untrimmed Array}
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




            var target = new TargetImageTextFile("testfile1", @"OOPTests/TestFile1.txt", 'X');

            var actual = target.GridRepresentation;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test2_ParsingTxtFile1()
        {
            /*
             * 
            //{Original Untrimmed Array}
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

            var target = new TargetImageTextFile("testfile1", @"OOPTests/TestFile2.txt", 'X');

            var actual = target.GridRepresentation;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateInternalShapeCoodinatesOfTarget()
        {
            var target = new TargetImageArray("test1", targetArray, ' ');

            var expected = new List<Coordinate>()
             {
                 new Coordinate(0, 0),
                 new Coordinate(0, 1),
                 new Coordinate(0, 2),
                 new Coordinate(0, 3),

                 new Coordinate(1, 1),
                 new Coordinate(1, 2),

                 new Coordinate(2, 1),
                 new Coordinate(2, 2),

                 new Coordinate(3, 0),
                 new Coordinate(3, 1),
                 new Coordinate(3, 2),
                 new Coordinate(3, 3),

                 new Coordinate(4, 0),
                 new Coordinate(4, 1),
                 new Coordinate(4, 2),
                 new Coordinate(4, 3),

                 new Coordinate(5, 0),
                 new Coordinate(5, 1),
                 new Coordinate(5, 2),
                 new Coordinate(5, 3),
             };

            var actual = target.InternalShapeCoordinatesOfTarget;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateCentroidLocalCoordinates()
        {
            var target = new TargetImageArray("test1", targetArray, ' ');

            var expected = new Coordinate(1.5, 2.5);

            var actual = target.CentroidLocalCoordinates;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_Target_Test1_ValidateGlobalCoodinates()
        {
            var snapper = new SnapperImageArray("map", snapperImageArray);
            var target = new TargetImageArray("test1", targetArray, ' ');

            var expected = new Coordinate(5.5, 7.5);
            var actual = ITargetImage.CalculateGlobalCoordinatesOfShapeCentroid(target, 4, 5);

            Assert.AreEqual(expected, actual);
        }




    }
}
