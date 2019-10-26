using NUnit.Framework;
using SnapperCodingChallenge._Console.Procedural;

namespace SnapperCodingChallenge.NUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private string filePath = @"C:\Users\pcox\Desktop\git\SnapperCodingChallenge\SnapperCodingChallenge.NUnit\UnitTesting_ProceduralHelpers DONOTMODIFY.txt";
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
                ProceduralHelpers.ConvertTxtFileInto2DArray(filePath);

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