using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SnapperCodingChallenge.Core.Static_Libraries
{
    public static class DirectoryHelpers
    {
        public static string[] GetFilesWithiNDirectoryWithCertainFileExtension(string directoryPath, string fileExtension)
        {
            return Directory.GetFiles(directoryPath, fileExtension, SearchOption.AllDirectories);
        }
    }
}
