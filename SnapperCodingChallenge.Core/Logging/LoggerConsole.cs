using System;

namespace SnapperCodingChallenge.Core
{
    public class LoggerConsole : ILogger
    {
        public LoggerConsole()
        {
        }

        public void WriteBlankLine()
        {
            Console.WriteLine();
        }

        public void Write(string msg, bool withDateTime = false)
        {
            if (withDateTime)
            {
                Console.WriteLine($"{DateTime.Now} {msg}");
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public void WriteLine(string msg, bool withDateTime = false)
        {
            if (withDateTime)
            {
                Console.WriteLine($"{DateTime.Now} {msg}");
            }
            else
            {
                Console.WriteLine(msg);
            }
        }

        public void ReadLine()
        {
            Console.ReadLine();
        }
    }
}
