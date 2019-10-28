using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public static class TupleHelpers
    {
        public static void PrintTuplePOCOObjectsToConsole(List<Tuple<int, int>> tuples)
        {
            Console.WriteLine("var coords = new List<Tuple<int,int>>(){");

            foreach (Tuple<int, int> tuple in tuples)
            {
                Console.WriteLine($"new Tuple<int,int>({tuple.Item1},{tuple.Item2}),");
            }
            Console.WriteLine(";");
        }
        
        /// <summary>
        /// Calculates the location of the centroid of a subarray within an array, where a unit is taken
        /// as a single horizontal or vertical translation.:
        /// 
        /// 0 0 0 0 0
        /// 0 0 0 1 1
        /// 0 0 0 1 1 
        /// 0 0 0 0 0
        /// 
        /// x0 = 3, y0 = 1
        /// xcentroid = x0+(cols-1)/2
        /// ycentrpod = y0+(rows=1)/2
        /// </summary>
        /// <param name="array"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public static Tuple<double, double> GetSubArrayCentroid(char[,] array, int x0, int y0, int rows, int cols)
        {
            double x = Convert.ToDouble(x0) + (Convert.ToDouble(cols) - 1) / 2;
            double y = Convert.ToDouble(y0) + (Convert.ToDouble(rows) - 1) / 2;

            return new Tuple<double, double>(x, y);
        }


    }
}
