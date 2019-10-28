using SnapperCodingChallenge.Core.OOP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnapperCodingChallenge.Core
{
    public class Scanner
    {
        public Scanner(Target map, double minimumAccuracy, params Target[] targets)
        {
            this.Map = map;
            this.MinimumAccuracy = minimumAccuracy;

            //TODO Handle instances where no targets have been specified. 
            Targets.AddRange(from Target t in targets select t);
        }
        
        public Target Map {get; set; }
        public double MinimumAccuracy { get; set; }
        public List<Target> Targets = new List<Target>();

        //Methods 
        public void ScanSnapperImage()
        {
            Console.WriteLine($"LOADING SNAPPER");
            Console.WriteLine();

            Console.WriteLine("MAP DATA");
            MultiDimensionalArrayHelpers.Print2DCharacterArrayToConsole(Map.GridRepresentation);

            Console.WriteLine($"TARGETS");
            foreach (Target t in Targets)
            {
                t.PrintTargetInformation();
            }

            Console.WriteLine($"SCANNING");
            foreach (Target t in Targets)
            {

            }



        }
    }
}
