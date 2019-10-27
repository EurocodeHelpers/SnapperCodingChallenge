using NUnit.Framework;
using SnapperCodingChallenge._Console.Procedural;
using System;
using System.Collections.Generic;

namespace SnapperCodingChallenge.NUnit
{
    public class ProceduralHelpersTests

    {
        [SetUp]
        public void Setup()
        {
        }

        private string filePath1 = @"Datafiles\UnitTestFile1.txt";
        private string filePath2 = @"Datafiles\UnitTestFile2.txt";

        private char[,] matrixA = new char[3, 5]
        {
            {' ', ' ', 'X', ' ', ' ' },
            {' ', 'X', 'X', 'X', ' ' },
            {'X', 'X', 'X', 'X', 'X' },
        };

        private char[,] matrixB = new char[3, 5]
    {
            {' ', ' ', 'X', ' ', 'X' },
            {' ', 'X', 'X', 'X', ' ' },
            {'X', 'X', ' ', ' ', 'X' },
    };


        private List<Tuple<int, int>> coords = new List<Tuple<int, int>>()
        {
            new Tuple<int,int>(0,4),
            new Tuple<int,int>(1,4),
            new Tuple<int,int>(2,3),
            new Tuple<int,int>(2,4),
            new Tuple<int,int>(2,5),
            new Tuple<int,int>(3,1),
            new Tuple<int,int>(3,2),
            new Tuple<int,int>(3,3),
            new Tuple<int,int>(3,4),
            new Tuple<int,int>(3,5),
            new Tuple<int,int>(3,6),
            new Tuple<int,int>(3,7),
            new Tuple<int,int>(4,1),
            new Tuple<int,int>(4,2),
            new Tuple<int,int>(4,3),
            new Tuple<int,int>(4,4),
            new Tuple<int,int>(4,5),
            new Tuple<int,int>(4,6),
            new Tuple<int,int>(4,7),
            new Tuple<int,int>(5,0),
            new Tuple<int,int>(5,1),
            new Tuple<int,int>(5,2),
            new Tuple<int,int>(5,3),
            new Tuple<int,int>(5,4),
            new Tuple<int,int>(5,5),
            new Tuple<int,int>(5,6),
            new Tuple<int,int>(5,7),
            new Tuple<int,int>(5,8),
            new Tuple<int,int>(6,0),
            new Tuple<int,int>(6,1),
            new Tuple<int,int>(6,2),
            new Tuple<int,int>(6,3),
            new Tuple<int,int>(6,4),
            new Tuple<int,int>(6,5),
            new Tuple<int,int>(6,6),
            new Tuple<int,int>(6,7),
            new Tuple<int,int>(6,8),
            new Tuple<int,int>(7,0),
            new Tuple<int,int>(7,1),
            new Tuple<int,int>(7,2),
            new Tuple<int,int>(7,3),
            new Tuple<int,int>(7,4),
            new Tuple<int,int>(7,5),
            new Tuple<int,int>(7,6),
            new Tuple<int,int>(7,7),
            new Tuple<int,int>(7,8),
            new Tuple<int,int>(8,1),
            new Tuple<int,int>(8,2),
            new Tuple<int,int>(8,3),
            new Tuple<int,int>(8,4),
            new Tuple<int,int>(8,5),
            new Tuple<int,int>(8,6),
            new Tuple<int,int>(8,7),
            new Tuple<int,int>(9,1),
            new Tuple<int,int>(9,2),
            new Tuple<int,int>(9,3),
            new Tuple<int,int>(9,4),
            new Tuple<int,int>(9,5),
            new Tuple<int,int>(9,6),
            new Tuple<int,int>(9,7),
            new Tuple<int,int>(10,3),
            new Tuple<int,int>(10,4),
            new Tuple<int,int>(10,5),
        };


        //[Test]
        //public void Verify_Coords()
        //{
        //    var expected = coords;

        //    var spaceship = ProceduralHelpers.ConvertTxtFileInto2DArray(filePath2);
        //    var trimmedSpaceship = ProceduralHelpers.TrimArray(spaceship, ' ');

        //    var actual =
        //        ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(trimmedSpaceship, ' ');

        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Verify_Comparison()
        //{
        //    //TODO
        //}
    }
}