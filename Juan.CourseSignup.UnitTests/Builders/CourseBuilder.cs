using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.UnitTests.Builders
{
    public class CourseBuilder
    {
        private Course _course;
        private int _defaultCapacity = 10;
        private string _defaultName = "Default Course Name";
        
        public CourseBuilder()
        {
            _course = WithDefaultValues();
        }

        public Course WithDefaultValues()
        {
            _course = new Course(_defaultName, _defaultCapacity, new List<Student>());

            return _course;
        }

        public Course WithFullCapacity()
        {
            _course = new Course(_defaultName, 1, new List<Student>());

            _course.AddStudent(new StudentBuilder().WithDefaultValues());
            
            return _course;
        }
    }
}
