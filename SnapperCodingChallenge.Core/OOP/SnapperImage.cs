using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class SnapperImage 
    {
        public SnapperImage(string name, string filePath)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = TextFileHelpers.ConvertTxtFileInto2DArray(filePath);
        }

        public SnapperImage(string name, char[,] array)
        {
            this.Name = name;
            this.FilePath = "N/A";
            this.GridRepresentation = array;
        }

        public string Name { get; }
        public string FilePath { get; }
        public char[,] GridRepresentation { get; }
        public int NumberOfRows => GridRepresentation.GetLength(0);
        public int NumberOfColumns => GridRepresentation.GetLength(1);
        public string GridDimensionsSummary => $"Grid Size [rows,cols] = {NumberOfRows},{NumberOfColumns}";

    }
}
