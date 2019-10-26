using NUnit.Framework;
using SnapperCodingChallenge._Console.Procedural;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SnapperCodingChallenge.NUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private string filePath1 = @"Datafiles\UnitTestFile1.txt";
        private string filePath2 = @"Datafiles\UnitTestFile2.txt";

        private char[,] untrimmedarray = new char[10, 11]
        {
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','1','1','1','1','X','1','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','1','1','1','1','X','1','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
                {'X','X','X','X','X','X','X','X','X','X','X'},
        };

        private char[,] trimmedArray = new char[5, 6]
        {
                {'1','1','1','1','X','1'},
                {'X','X','X','X','X','X'},
                {'X','X','X','X','X','X'},
                {'X','X','X','X','X','X'},
                {'1','1','1','1','X','1'},
        };

        [Test]
        public void Verify_ConvertTxtFileInto2DArray()
        {
            var expected = untrimmedarray;

            var actual = 
                ProceduralHelpers.ConvertTxtFileInto2DArray(filePath1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_TrimmedArray()
        {
            var expected = trimmedArray;

            var actual =
                ProceduralHelpers.TrimArray(untrimmedarray,'X');

            Assert.AreEqual(expected, actual);
        }

    }
} 