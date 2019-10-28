﻿using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System;
using System.Collections.Generic;

namespace SnapperCodingChallenge.NUnit
{
    public class IdentifyTargetTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(1.0, 2)]     //Accuracy > MinimumAccuracy PASS
        [TestCase(0.5, 6)]    //Accuracy < MinimumAccuracy FAIL
        public void Verify_IdentifyTarget1(double minimumAccuracy, int numberOfTargetsIdentified)
        {
            char[,] map = new char[,]
            {
                {'X','X',' ',' '},
                {'X','X',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ','X','X'},
                {' ',' ','X','X'},
            };

            char[,] target = new char[,]
            {
                {'X','X'},
                {'X','X'},
            };

            //Using an accuracy of 100% there are evidently only 2 available targets.
            //using a fuzzy approach, because we are overlapping a 50% minumum accuracy should yield 6 targets.

            var expected = numberOfTargetsIdentified;

            var actual =
              ProceduralHelpers.CalculateCoordinatesOfIdentifiedTargets(map, target, minimumAccuracy, ' ');

            Assert.AreEqual(expected, actual.Count);
        }

        [Test]
        public void Verify_IdentifyTarget2()
        {
            char[,] map = new char[,]
            {
                {'X','X',' ',' '},
                {'X','X',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ',' ',' '},
                {' ',' ','X','X'},
                {' ',' ','X','X'},
            };

            char[,] target = new char[,]
            {
                {'X','X'},
                {'X','X'},
            };

            //Using an accuracy of 100% there are evidently only 2 available targets.
            //using a fuzzy approach, because we are overlapping a 50% minumum accuracy should yield 6 targets.

            var expected = new List<Tuple<double, double>>
            {
                new Tuple<double, double>(0.5, 0.5),
                new Tuple<double,double>(2.5, 4.5),
            };

            var actual =
              ProceduralHelpers.CalculateCoordinatesOfIdentifiedTargets(map, target, 1, ' ');

            Assert.AreEqual(expected, actual);
        }

    }
}
