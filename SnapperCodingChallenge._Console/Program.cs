using SnapperCodingChallenge.Core.Procedural;
using System;
using System.Collections.Generic;
using System.IO;
using static SnapperCodingChallenge._Console.Procedural.ProceduralHelpers;

namespace SnapperCodingChallenge._Console
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] map = ConvertTxtFileInto2DArray(@"Supplied Files DONOTEDIT/TestData.blf");
            char[,] target1 = ConvertTxtFileInto2DArray(@"Supplied Files DONOTEDIT/NuclearTorpedo.blf");
            char[,] target2 = ConvertTxtFileInto2DArray(@"Supplied Files DONOTEDIT/Starship.blf");


            var coordsTarget1 = CalculateCoordinatesOfIdentifiedTargets(map, target1, 0.65, ' ');
            var coordsTarget2 = CalculateCoordinatesOfIdentifiedTargets(map, target2, 0.65, ' ');

            Console.WriteLine();

            PrintTupleDataToConsole(coordsTarget1);
            PrintTupleDataToConsole(coordsTarget2); 

            Console.WriteLine($"Target 1 Instances detected = {coordsTarget1.Count}");
            Console.WriteLine($"Target 2 Instances detected = {coordsTarget2.Count}");
            Console.WriteLine($"TOTAL Instances detected = {coordsTarget1.Count} + {coordsTarget2.Count}");
        }

        //Cheat method to get static list of tuples...

        public static void PrintTupleDataToConsole(List<Tuple<int, int>> tuples)
        {
            Console.WriteLine("var coords = new List<Tuple<int,int>>(){");

            foreach (Tuple<int, int> tuple in tuples)
            {
                Console.WriteLine($"new Tuple<int,int>({tuple.Item1},{tuple.Item2}),");
            }
            Console.WriteLine(";");
        }







    }
}
