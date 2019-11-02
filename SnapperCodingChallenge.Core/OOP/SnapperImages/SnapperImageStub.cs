using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class SnapperImageStub : ISnapperImage
    {
        public SnapperImageStub(string name, char[,] array)
        {
            this.Name = name;
            this.GridRepresentation = array;
        }

        public string Name { get; }
        public char[,] GridRepresentation { get; }
        public string GridDimensions => $"Grid Size [Rows,Cols] = {GridRepresentation.GetLength(0)},{GridRepresentation.GetLength(1)}";
    }
}
