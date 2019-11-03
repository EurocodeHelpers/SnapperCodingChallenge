using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System;
using System.Collections.Generic;

namespace SnapperCodingChallenge.NUnit
{
    class CompareArraysTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[TestCase(true, 0.635)]     //Accuracy > MinimumAccuracy PASS
        //[TestCase(false, 0.637)]    //Accuracy < MinimumAccuracy FAIL
        //public void Verify_CompareArrays_Test1(bool expectedOutput, double minimumAccuracy)
        //{
        //    char[,] arrayA = new char[,]
        //    {
        //        {' ',' ','0',' ',' '},
        //        {' ',' ','0',' ',' '},
        //        {' ','0','0','0','0'},
        //        {'0','0','0','0','0'},
        //    };

        //    char[,] arrayB = new char[,]
        //    {
        //        {' ',' ','0',' ','0'},
        //        {' ',' ','0',' ',' '},
        //        {' ','0','0','0','0'},
        //        {'0',' ',' ',' ',' '},

        //     };

        //    //Number elements changed = 5 (including addition of element in top right hand corner.
        //    //However => recall that accuracy depends only on checking the eleements within the shape perimeter!
        //    //Therefore changed elements = 5-1 = 4. 
        //    //Total elements checked = 5+4+1+1 = 11
        //    //Calculated accuracy = (11-4)/11 = 7/11 = 0.6363

        //    bool expected = expectedOutput;

        //    List<Tuple<int, int>> coords = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(arrayA, ' ');
        //    bool actual = ProceduralHelpers.VerifyArraysAreIdenticalAgainstKnownCoordinates(arrayA, arrayB, coords, minimumAccuracy);

        //    Assert.AreEqual(expected, actual);
        //}
    }
}
