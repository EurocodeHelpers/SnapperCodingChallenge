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

        public Scanner(Target map, double minimumAccuracy, List<Target> targets)
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
            foreach (Target t in Targets)
            {

            }
        }
    }
}
