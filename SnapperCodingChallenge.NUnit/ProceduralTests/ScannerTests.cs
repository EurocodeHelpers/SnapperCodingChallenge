using NUnit.Framework;
using SnapperCodingChallenge.Core;
using System;
using System.Collections.Generic;

namespace SnapperCodingChallenge.NUnit
{
    public class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //public char[,] SnapperImage = new char[8, 5]
        //{
        //        {' ',' ',' ',' ',' '},
        //        {' ',' ',' ','1','1'},
        //        {' ',' ',' ','1','1'},
        //        {'1','1',' ',' ',' '},
        //        {'1','1',' ',' ',' '},
        //        {' ',' ',' ',' ',' '},
        //        {' ',' ','1','1',' '},
        //        {' ',' ','1','1',' '},
        //};
        //private char[,] exampleTarget2 = new char[2, 2]
        //{
        //    {'1','1'},
        //    {'1','1'},
        //};

        //[Test]
        //public void Verify_GetNumberOfDetections_Precision100Percent()
        //{
        //    ISnapperImage snapperImage = new SnapperImageStub("TestData", SnapperImage);
        //    ITarget t = new TargetStub("Example", exampleTarget2, ' ');

        //    Scanner s = new Scanner(snapperImage, 1);

        //    s.ScanForTarget(t);

        //    var identifiedTargets = s.GetListOfIdentifiedTargets();

        //    int expected = 3;
        //    int actual = identifiedTargets.Count;

        //    Assert.AreEqual(expected, actual);
        //}

        //[Test]
        //public void Verify_GetIdentifiedTargets()
        //{
        //    ISnapperImage snapperImage = new SnapperImageStub("TestData", SnapperImage);
        //    ITarget t = new TargetStub("Example", exampleTarget2, ' ');
        //    Scanner s = new Scanner(snapperImage, 1);
        //    s.ScanForTarget(t);

        //    List<Scan> expected = new List<Scan>
        //    {

        //    }


        //    List<Scan> expected = s.GetListOfIdentifiedTargets();





        //    int expected = 3;
        //    int actual = identifiedTargets.Count;

        //    Assert.AreEqual(expected, actual);
        //}

    }
}
