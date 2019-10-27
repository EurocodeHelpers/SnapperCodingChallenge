using NUnit.Framework;
using SnapperCodingChallenge._Console.Procedural;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.NUnit.ArrayTrimTests
{
    public class ArrayTrimTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Verify_TrimArray_Test1()
        {
            char[,] originalArray = new char[,]
        {
            {'0','0','0','0','0','0'},
            {'0','X','X','X','X','0'},
            {'0','0','0','0','0','0'},
            {'0','0','X','X','0','0'},
            {'0','0','X','X','0','0'},
            {'0','0','0','0','0','0'},
        };

            char[,] expected = new char[,]
            {
            {'X','X','X','X'},
            {'0','0','0','0'},
            {'0','X','X','0'},
            {'0','X','X','0'},
            };

            char[,] actual = ProceduralHelpers.TrimArray(originalArray, '0');

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_TrimArray_Test2()
        {
            char[,] originalArray = new char[,]
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

            char[,] expected = new char[,]
            {
                {'1','1','1','1','X','1'},
                {'X','X','X','X','X','X'},
                {'X','X','X','X','X','X'},
                {'X','X','X','X','X','X'},
                {'1','1','1','1','X','1'},
            };
            
            char[,] actual = ProceduralHelpers.TrimArray(originalArray, 'X');

            Assert.AreEqual(expected, actual);
        }






    }
}
        
