using System;

namespace FileForwardCLI.Converters
{
    public static class StringConverter
    {
        public static byte[] ToByteArray(string input)
        {
            try
            {
                return System.Text.Encoding.ASCII.GetBytes(input);
            }
            catch (Exception e)
            {
                return new byte[0];
            }
        }
        
        
    }
}