using Juan.CourseSignup.ApplicationCore.Entities;
using Juan.CourseSignup.ApplicationCore.Exceptions;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities
{
    public class Course : BaseEntity, IAggregateRoot
    {
        private Course()
        {

        }             

        public Course(string name, int maxCapacity, List<Student> students)
        {
            // add validation that does not allow empty or null name
            if (string.IsNullOrEmpty(name))
                throw new NameIsNullOrEmptyException();

            // add validation that does not allow less than 1 capacity
            if (maxCapacity <= 0)
                throw new ArgumentOutOfRangeException("Course capacity must be greater than 0");

            Name = name;
            MaxCapacity = maxCapacity;
            _students = students;
        }
        
        public string Name { get; private set; }
        public int MaxCapacity { get; private set; } = 1;

        /// <summary>
        /// This ensures that Students cannot be added direclty to the list. It can only be done from Course.AddStudent() that includes business logic.
        /// </summary>
        private readonly List<Student> _students = new List<Student>();
        public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

        public void AddStudent(Student student)
        {
            // Validate if course is full
            if (_students.Count() + 1 > this.MaxCapacity)
                throw new CourseIsFullException(this.Id);
            
            _students.Add(student);
        }
        public int GetStudentsMinimumAge()
        {
            if (_students.Any())
                return _students.Min(s => s.Age);

            return 0;
        }
        public int GetStudentsMaximumAge()
        {
            if (_students.Any())
                return _students.Min(s => s.Age);

            return 0;
        }
        public int GetStudentsAverageAge()
        {
            if (_students.Any())
            {
                var sumAges = GetStudentsSummedAges();

                var count = _students.Count();

                return sumAges / count;
            }

            return 0;
        }
        public int GetStudentsSummedAges()
        {
            if (_students.Any())
                return _students.Sum(s => s.Age);

            return 0;
        }
        public int GetTotalStudents()
        {
            return _students.Count();
        }
    }
}
