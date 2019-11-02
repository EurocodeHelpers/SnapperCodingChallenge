using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class SnapperImageTxt : ISnapperImage
    {
        public SnapperImageTxt(string name, string filePath)
        {
            this.Name = name;
            this.FilePath = filePath;
            this.GridRepresentation = TextFileHelpers.ConvertTxtFileInto2DArray(filePath);
        }

        public string Name { get; }
        public string FilePath { get; }
        public char[,] GridRepresentation { get; }
        public string GridDimensions => $"Grid Size [Rows,Cols] = {GridRepresentation.GetLength(0)},{GridRepresentation.GetLength(1)}";
    }
}
