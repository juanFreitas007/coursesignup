using System;

namespace Juan.CourseSignup.ApplicationCore.Exceptions
{
    public class CourseIsFullException : Exception
    {
        public CourseIsFullException(long courseId) : base($"Course with Id {courseId} is full")
        {

        }

        public CourseIsFullException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
