using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnapperCodingChallenge.Core.Static_Libraries
{
    public static class FileHandlingHelpers
    {
        public static bool CheckForExistenceOfFileType(string dirPath, string fileExtension)
        {
            if (Directory.GetFiles(dirPath, fileExtension).Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
