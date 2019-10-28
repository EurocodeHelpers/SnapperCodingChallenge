using NUnit.Framework;
using SnapperCodingChallenge.Core;

namespace SnapperCodingChallenge.NUnit
{
    public class TextFileHelpersTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Verify_ConvertTxtFileInto2DArray_Test1()
        {
            var expected = new char[10, 11]
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

            var actual =
                TextFileHelpers.ConvertTxtFileInto2DArray(@"1-TextFileHelpersTests/TextFileHelpers-TestFile1.txt");

            Assert.AreEqual(expected, actual);
        }



        [Test]
        public void Verify_ConvertTxtFileInto2DArray_Test2()
        {
            var expected = new char[3, 5]
                {
                    {'X','X','1','X','X'},
                    {'X','2','3','4','X'},
                    {'5','6','7','8','9'},
                };

            var actual =
                TextFileHelpers.ConvertTxtFileInto2DArray(@"1-TextFileHelpersTests/TextFileHelpers-TestFile2.txt");

            Assert.AreEqual(expected, actual);
        }



    }
}
