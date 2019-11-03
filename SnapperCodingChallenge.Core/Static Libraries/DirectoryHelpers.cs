using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnapperCodingChallenge.Core
{
    public static class DirectoryHelpers
    {
        public static string[] GetFilesWithinDirectoryWithCertainFileExtension(string directoryPath, string fileExtension)
        {
            return Directory.GetFiles(directoryPath, fileExtension, SearchOption.AllDirectories);
        }

        public static bool CheckForExistenceOfFileTypeWithinDirectoryPath(string dirPath, string fileExtension)
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
