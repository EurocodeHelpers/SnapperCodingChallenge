namespace SnapperCodingChallenge.Core
{
    public interface ILogger
    {
        void Write(string msg, bool withDateTime = false);

        void WriteLine(string msg, bool withDateTime = false);

        void WriteBlankLine();
    }
}
