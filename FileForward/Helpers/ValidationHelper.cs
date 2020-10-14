using System.IO;
using FileForward.Exceptions;

namespace FileForward.Helpers
{
    public static class ValidationHelper
    {
        public static void CheckEndpointUrl(string endpointUrl)
        {
            StringCannotBeNull(endpointUrl, "Endpoint cannot be null!");
            StringMustStartWithSlash(endpointUrl, "Endpoint must start with (or be equal to) a forward slash (/)!");
        }

        public static void CheckFilename(string filename)
        {
            StringCannotBeNull(filename, "Filename cannot be null!");
            if (!File.Exists(filename))
                throw new OperationException($"File not found! File: {filename}");
        }
        
        public static void MustHaveContent(byte[] content)
        {
            if (content.Length == 0 || content.LongLength == 0)
                throw new OperationException("The file must have a content to be sent!");
        }

        private static void StringCannotBeNull(string value, string errorMessage)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                throw new OperationException(errorMessage);   
        }

        private static void StringMustStartWithSlash(string value, string errorMessage)
        {
            if (!value.StartsWith("/"))
                throw new OperationException(errorMessage);
        }
    }
}