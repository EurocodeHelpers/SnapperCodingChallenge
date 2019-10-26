using System;
using System.IO;
using SnapperCodingChallenge._Console.Procedural;

namespace SnapperCodingChallenge._Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Convert the raw "Snapper" data into a 2d array of characters            
            char[,] array = ProceduralHelpers.ConvertTxtFileInto2DArray(@"datafiles/perimeterTest.txt");
            array.Print2DCharacterArrayToConsole();

            Console.WriteLine();

            var trimmedArray = ProceduralHelpers.TrimArray(array, '0');
            trimmedArray.Print2DCharacterArrayToConsole();

            Console.WriteLine();

            var coords = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(trimmedArray, '0');
            ProceduralHelpers.PrintListOfTuplesToConsole(coords);

        }
    }
}
