namespace SnapperCodingChallenge.Core
{
    public struct Coordinates
    {
        public double X;
        public double Y;
           
        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }


    }
}
