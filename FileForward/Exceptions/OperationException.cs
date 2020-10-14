using System;

namespace FileForward.Exceptions
{
    [Serializable]
    public class OperationException: Exception
    {
        public OperationException() : base(message: "Operation error for FileForward!") {}
        public OperationException(string message): base(message) {}
    }
}