using Juan.CourseSignup.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.UnitTests.Builders
{
    public class StudentBuilder
    {
        private string _defaultName = "Student A";
        private int _defaultAge = 21;

        private Student _student;

        public StudentBuilder()
        {
            _student = WithDefaultValues();
        }

        public Student WithDefaultValues()
        {
            _student = new Student(_defaultName, _defaultAge);

            return _student;
        }
    }
}
