using NUnit.Framework;
using SnapperCodingChallenge._Console.Procedural;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.NUnit.GetCoordinatesTests
{
    class CompareArraysTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Verify_CompareArrays_Test1()
        {
            char[,] arrayA = new char[,]
              {
                {' ',' ','0',' ',' '},
                {' ','0',' ','0',' '},
                {'0','0',' ','0','0'},
                {' ',' ','0',' ',' '},

              };

            char[,] arrayB = new char[,]
              {
                {' ',' ','0',' ',' '},
                {' ',' ',' ',' ',' '},
                {' ',' ',' ','0','0'},
                {' ',' ','0',' ',' '},

              };

            //Number of elements checked = 1 + 3 + 5 + 1 = 10
            //Changed elements INSIDE object perimeter = 4
            //Therefore, specifying an accuracy of 50% (0.5) the test should still pass!

            bool expected = true;

            List<Tuple<int, int>> coords = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(arrayA, ' ');
            bool actual = ProceduralHelpers.VerifyArraysAreIdenticalAgainstKnownCoordinates(arrayA, arrayB, coords, 0.5);

            Assert.AreEqual(expected, actual);
        }
    }
}
