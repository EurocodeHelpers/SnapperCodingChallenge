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
            char[,] array = ProceduralHelpers.ConvertTxtFileInto2DArray(@"C:\Users\pcox\Desktop\git\SnapperCodingChallenge\Starship.blf");

            //2. Print array to console
            array.Print2DArrayToConsole();
        }
    }
}
