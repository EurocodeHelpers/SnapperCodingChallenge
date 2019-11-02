using SnapperCodingChallenge.Core;
using System;

namespace SnapperCodingChallenge._ConsoleRunnerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] exampleMap = new char[8,5]
           {
                {'1','1','0','0','0'},
                {'1','1','0','1','1'},
                {'0','0','0','1','1'},
                {'0','0','0','0','0'},
                {'0','1','1','0','0'},
                {'0','1','1','0','0'},
                {'0','0','0','0','0'},
                {'0','0','0','0','0'},
           };

            char[,] exampleTarget = new char[2,2]
            {
                 {'1','1'},
                 {'1','1'},
            };


            ISnapperImage snapperImage = new SnapperImageStub("Example", exampleMap);
            ITarget target = new TargetStub("exampleTarget", exampleTarget, ' ');

            Scanner s = new Scanner(snapperImage, 1);

            s.ScanForTarget(target);

            Console.WriteLine("Hello world.");
        }
    }
}
