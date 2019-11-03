using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System;

namespace SnapperCodingChallenge.NUnit
{
    public class SubArrayTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Verify_SubArray1()
        {
            char[,] arrayMain = new char[3, 4]
            {
                {'A','B', 'C', 'D' },
                {'E','F', 'G', 'H' },
                {'I','J', 'K', 'L' },
            };

            var expected = new char[2, 2]
            {
                {'A','B'},
                {'E','F'},
            };

            var actual =
                MultiDimensionalCharacterArrayHelpers.GetSubArrayFromArray(arrayMain, 0, 0, 2, 2);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Verify_SubArray2()
        {
            char[,] arrayMain = new char[3, 4]
            {
                {'A','B', 'C', 'D' },
                {'E','F', 'G', 'H' },
                {'I','J', 'K', 'L' },
            };

            char[,] expected = new char[2, 1]
            {
                  {'H'},
                  {'L'},
            };

            var actual =
                MultiDimensionalCharacterArrayHelpers.GetSubArrayFromArray(arrayMain, 3, 1, 2, 1);

            Assert.AreEqual(expected, actual);
        }
    }
}
