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

        public void WriteLine(string msg, bool withDateTime)
        {
            if (withDateTime)
            {
                File.AppendAllText(_filePath, $"{DateTime.Now} {msg} + {Environment.NewLine}");
            }
            else
            {
                File.AppendAllText(_filePath, $"{msg} {Environment.NewLine}");
            }
        }

        public void WriteBlankLine()
        {
            File.AppendAllText(_filePath, "");
        }
    }
}
