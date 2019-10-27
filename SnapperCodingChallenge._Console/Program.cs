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
<<<<<<< HEAD
            char[,] map = new char[6, 3]
            {
                    {'X','X', '0'},
                    {'X','X', '0'},
                    {'0','0', '0'},
                    {'0','0', '0'},
                    {'0','0', 'X'},
                    {'0','0', 'X'},
            };
=======
            //1. Convert the raw "Snapper" data into a 2d array of characters            
            char[,] array = ProceduralHelpers.ConvertTxtFileInto2DArray(@"Test Files/perimeterTest.txt");
            array.Print2DCharacterArrayToConsole();
>>>>>>> master

            char[,] target = new char[2, 2]
            {
                    {'X','X'},
                    {'X','X'},
            };

            int targetsIdentified = CalculateNumberofIdentifiedTargets(map, target, 1, '0');

            Console.WriteLine(targetsIdentified);
        }

        //Cheat method to get static list of tuples...

<<<<<<< HEAD
        public static void PrintTupleDataToConsole(List<Tuple<int,int>> tuples)
        {
            Console.WriteLine("var coords = new List<Tuple<int,int>>(){");

            foreach (Tuple<int,int> tuple in tuples)
            {
                Console.WriteLine($"new Tuple<int,int>({tuple.Item1},{tuple.Item2}),");
            }
                Console.WriteLine(";");
        }

=======
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

>>>>>>> master


    }
}
