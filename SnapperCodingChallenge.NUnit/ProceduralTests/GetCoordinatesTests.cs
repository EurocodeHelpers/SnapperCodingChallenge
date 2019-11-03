//using NUnit.Framework;
//using SnapperCodingChallenge.Core;
//using System;
//using System.Collections.Generic;

//namespace SnapperCodingChallenge.NUnit
//{
//    class GetCoordinatesTests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public void Verify_GetCoordinates_Test1()
//        {
//            char[,] array = new char[,]
//              {
//                {' ',' ','0',' ',' '},
//                {' ','0',' ','0',' '},
//                {'0','0',' ','0','0'},
//                {' ',' ','0',' ',' '},

//              };

//            //Number of tuples expected = 1 + 3 + 5 + 1 = 10No. elements. 

//            var expected = new List<Tuple<int, int>>
//            {
//                new Tuple<int,int>(0,2),
                
//                new Tuple<int,int>(1,1),
//                new Tuple<int,int>(1,2),
//                new Tuple<int,int>(1,3),

//                new Tuple<int,int>(2,0),
//                new Tuple<int,int>(2,1),
//                new Tuple<int,int>(2,2),
//                new Tuple<int,int>(2,3),
//                new Tuple<int,int>(2,4),

//                new Tuple<int,int>(3,2),
//            };

//            List<Tuple<int, int>> actual = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(array, ' ');


//            Assert.AreEqual(expected, actual);
//        }
//    }
//}
