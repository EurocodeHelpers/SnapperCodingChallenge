using System;
using System.IO;

namespace SnapperCodingChallenge.Core
{
    public class LoggerTextFile : ILogger
    {        
        public LoggerTextFile(string filePath)
        {
            this._filePath = filePath;
        }
        private string _filePath;

        public void Write(string msg, bool withDateTime)
        {
            File.AppendAllText(_filePath, $"{DateTime.Now} {msg}");
        }

        public void WriteLine(string msg, bool withDateTime)
        {
            File.AppendAllText(_filePath, $"{DateTime.Now} {msg}");
        }

        public void WriteBlankLine()
        {
            File.AppendAllText(_filePath, "");
        }
    }
}
