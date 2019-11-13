namespace SnapperCodingChallenge.Core
{
    public interface ILogger
    {
        void WriteLine(string msg, bool withDateTime = false);

        void WriteBlankLine();
    }
}
