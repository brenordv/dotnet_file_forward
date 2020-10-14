using System.IO;

namespace FileForward.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Use for files with size equal to or lesser than 4.2GB.
        /// </summary>
        /// <param name="filename">Name of the file to be read</param>
        /// <returns>file content as bytes</returns>
        public static byte[] GetFileContent(string filename)
        {
            return File.ReadAllBytes(filename);
        }
    }
}