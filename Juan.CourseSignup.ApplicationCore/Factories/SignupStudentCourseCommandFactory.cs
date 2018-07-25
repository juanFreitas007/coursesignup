using Juan.CourseSignup.ApplicationCore.Commands;
using Juan.CourseSignup.ApplicationCore.Dtos;
using Juan.CourseSignup.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.ApplicationCore.Factories
{
    public static class SignupStudentCourseCommandFactory
    {
        public static SignupStudentCourseCommand Get(long courseId, string studentName, int studentAge)
        {
            var student = new StudentDto() {
                Name = studentName,
                Age = studentAge
            };

            return new SignupStudentCourseCommand
            {
                CourseId = courseId,
                Student = student
            };   
        }

    }
}
