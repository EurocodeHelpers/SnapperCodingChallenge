using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core.OOP
{
    public class Target 
    {
        public Target(string name, string filePath, char blankCharacter)
        {
            this.Name = name;

            var array = TextFileHelpers.ConvertTxtFileInto2DArray(filePath);
            var trimmedArray = MultiDimensionalArrayHelpers.TrimArray(array, ' ');
            this.GridRepresentation = trimmedArray;
            this.InternalCoordinatesOfTarget = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(trimmedArray, blankCharacter);
        }

        public string Name { get; }
        public char[,] GridRepresentation { get; }
        public List<Tuple<int,int>> InternalCoordinatesOfTarget { get; }

        public void PrintTargetInformation(bool printInternalCoordinates = false)
        {
            Console.WriteLine($"Name = {Name}");
            Console.WriteLine($"Grid Size (Rows x Cols = {GridRepresentation.GetLength(0)},{GridRepresentation.GetLength(1)}");
            Console.WriteLine();
            MultiDimensionalArrayHelpers.Print2DCharacterArrayToConsole(GridRepresentation);
            Console.WriteLine();

            if (printInternalCoordinates)
            {
                TupleHelpers.PrintTuplePOCOObjectsToConsole(InternalCoordinatesOfTarget);
            }
        }


    }
}
