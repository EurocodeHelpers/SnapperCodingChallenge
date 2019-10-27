using SnapperCodingChallenge._Console.Procedural;
using System;
using System.Collections.Generic;

namespace SnapperCodingChallenge.Core.OOP
{
    public class Target
    {
        public Target(string name, string txtFile, char ch)
        {
            this.Name = name;

            var array = ProceduralHelpers.ConvertTxtFileInto2DArray(txtFile);
            var trimmedArray = ProceduralHelpers.TrimArray(array, ch);
            targetArray = trimmedArray;

            InternalCoordinates = ProceduralHelpers.CalculateCoordinatesInsidePerimeterOfObject(trimmedArray, ch);    
        }

        //Inputs 
        public string Name { get; set; }        
        public char[,] targetArray { get; set; }

        //Properties 
        public List<Tuple<int, int>> InternalCoordinates { get; set; }
        public Tuple<int, int> CentroidCoordinates => new Tuple<int, int>(targetArray.GetLength(0) / 2, targetArray.GetLength(1) / 2);
        




    }
}
