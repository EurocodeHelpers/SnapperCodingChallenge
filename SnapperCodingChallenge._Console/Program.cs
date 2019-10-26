using System;
using System.Collections.Generic;
using System.IO;
using SnapperCodingChallenge._Console.Procedural;

namespace SnapperCodingChallenge._Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Convert the raw "Snapper" data into a 2d array of characters            
            char[,] array = ProceduralHelpers.ConvertTxtFileInto2DArray(@"Test Files/perimeterTest.txt");
            array.Print2DCharacterArrayToConsole();

            Console.WriteLine();

            var trimmedArray = ProceduralHelpers.TrimArray(array, '0');
            trimmedArray.Print2DCharacterArrayToConsole();

            Console.WriteLine();

            var coords = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(trimmedArray, ' ');
            ProceduralHelpers.PrintListOfTuplesToConsole(coords);

            PrintTupleDataToConsole(coords);
        }

        //Cheat method to get static list of tuples...

        public static void PrintTupleDataToConsole(List<Tuple<int,int>> tuples)
        {
            Console.WriteLine("var coords = new List<Tuple<int,int>>(){");

            foreach (Tuple<int,int> tuple in tuples)
            {
                Console.WriteLine($"new Tuple<int,int>({tuple.Item1},{tuple.Item2}),");
            }
                Console.WriteLine("};");
        }



    }
}
