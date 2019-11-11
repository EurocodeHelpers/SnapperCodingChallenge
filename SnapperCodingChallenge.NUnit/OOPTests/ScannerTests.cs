// using NUnit.Framework;
// using SnapperCodingChallenge.Core;
// using System.Collections.Generic;

// namespace SnapperCodingChallenge.Core.Tests
// {
//     class ScannerTests
//     {
//         [SetUp]
//         public void Setup()
//         {
//         }

//         public char[,] snapperImageArray = new char[,]
//         {
//               //  0   1   2   3   4   5   6   7
//                 {' ','0',' ',' ',' ',' ','0','0'},  //0
//                 {'0','0',' ',' ',' ',' ','0','0'},  //1
//                 {' ',' ',' ',' ',' ',' ',' ',' '},  //2
//                 {' ',' ',' ',' ',' ','X',' ',' '},  //3
//                 {' ',' ',' ','X',' ','X',' ',' '},  //4
//                 {' ',' ',' ','X',' ','X',' ',' '},  //5
//                 {' ',' ','X','X','X',' ',' ',' '},  //6
//                 {' ',' ',' ',' ',' ',' ',' ',' '},  //7
//                 {' ',' ',' ',' ',' ',' ',' ','0'},  //8
//                 {' ',' ',' ',' ',' ',' ','0',' '},  //9
//         };

//         public char[,] targetA = new char[,]
//         {
//                 {'0','0'},
//                 {'0','0'},
//         };

//         public char[,] targetB = new char[,]
//         {
//                 {' ',' ',' ','X'},
//                 {' ','X','X','X'},
//                 {' ','X','X','X'},
//                 {'X','X','X','X'},
//         };

//         [TestCase(0.75, 2)]
//         [TestCase(1, 1)]
//         public void Verify_Scanner_NumberOfIdentifiedTargetA(double minimumPrecision, int numberOfTargets)
//         {
//             var snapperImage = new SnapperImage("test", snapperImageArray);
//             var target1 = new Target("targetA", targetA, ' ');
//             var target2 = new Target("targetB", targetB, ' ');

//             var scanner = new Scanner(snapperImage, minimumPrecision);

//             scanner.ScanForTarget(target1);
//             scanner.ScanForTarget(target2);

//             var expected = numberOfTargets;
//             var actual = scanner.GetListOfIdentifiedTargets(target1).Count;

//             Assert.AreEqual(expected, actual);
//         }

//         [TestCase(0.72, 1)]
//         [TestCase(0.73, 0)]
//         public void Verify_Scanner_NumberOfIdentifiedTargetB(double minimumPrecision, int numberOfTargets)
//         {
//             var snapperImage = new SnapperImage("test", snapperImageArray);
//             var target1 = new Target("targetA", targetA, ' ');
//             var target2 = new Target("targetB", targetB, ' ');

//             var scanner = new Scanner(snapperImage, minimumPrecision);

//             scanner.ScanForTarget(target1);
//             scanner.ScanForTarget(target2);

//             var expected = numberOfTargets;
//             var actual = scanner.GetListOfIdentifiedTargets(target2).Count;

//             Assert.AreEqual(expected, actual);
//         }

//         [Test]
//         public void Verify_Scanner_GlobalCoordinatesOfIdentifiedTargets()
//         {
//             var snapperImage = new SnapperImage("test", snapperImageArray);
//             var target1 = new Target("targetA", targetA, ' ');
//             var target2 = new Target("targetB", targetB, ' ');

//             var scanner = new Scanner(snapperImage, 0.7);

//             scanner.ScanForTarget(target1);
//             scanner.ScanForTarget(target2);

//             List<Scan> identifiedTargets = scanner.GetListOfIdentifiedTargets();

//             var expected = new List<Coordinates>()
//             {
//                 new Coordinates(0.5, 0.5),
//                 new Coordinates(6.5, 0.5),
//                 new Coordinates(3.5, 4.5),
//             };

//             var actual = new List<Coordinates>();

//             foreach (Scan scan in identifiedTargets)
//             {
//                 actual.Add(scan.CentroidGlobalCoordinates);
//             }

//             Assert.AreEqual(expected, actual);
//         }
//     }
// }
