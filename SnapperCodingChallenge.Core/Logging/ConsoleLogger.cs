using System;
using System.Collections.Generic;
using System.Text;

namespace SnapperCodingChallenge.Core.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Write(string msg, bool withDateTime)
        {
            Console.WriteLine(msg);
        }

        public void Write(string msg)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string msg)
        {
            Console.Write(msg);
        }
    }
}
