using System;

namespace Juan.CourseSignup.ApplicationCore.Exceptions
{
    public class NameIsNullOrEmptyException : Exception
    {
        public NameIsNullOrEmptyException() : base($"Name cannot be null or empty")
        {

        }

        public NameIsNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
