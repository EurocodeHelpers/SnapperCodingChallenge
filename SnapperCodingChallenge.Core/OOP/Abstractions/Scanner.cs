using SnapperCodingChallenge.Core.OOP;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public class Scanner
    {
        public Scanner(ISnapperImage snapperImage, double minimumPrecision)
        {
            SnapperImage = snapperImage;
            MinimumPrecision = minimumPrecision;
        }

        public ISnapperImage SnapperImage { get; set; }
        public List<Scan> Scans = new List<Scan>();
        public double MinimumPrecision { get; set; }

        public void ScanForTarget(ITarget target)
        {
            int mapRows = SnapperImage.GridRepresentation.GetLength(0);
            int mapCols = SnapperImage.GridRepresentation.GetLength(1);

            int targetRows = target.GridRepresentation.GetLength(0);
            int targetCols = target.GridRepresentation.GetLength(1);

            int maxX0 = mapCols - targetCols;
            int maxY0 = mapRows - targetRows;

            for (int i = 0; i < maxY0; i++)
            {
                for (int j = 0; j < maxX0; j++)
                {
                    Scan s = new Scan(SnapperImage, target, j, i, MinimumPrecision);
                    Scans.Add(s);
                }
            }
        }
    }
}
