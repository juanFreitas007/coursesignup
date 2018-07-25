using Juan.CourseSignup.ApplicationCore.Exceptions;
using System;

namespace Juan.CourseSignup.ApplicationCore.Entities
{
    public class Student : BaseEntity
    {

        private Student()
        {

        }

        public Student(string name, int age)
        {
            // We could add some validations for age
            //  --> age cannot be less than 0 
            //  --> other validations for example if the course would have a minimum age for the students, but then the validation would need to be in the context of the course     
            if (age <= 0)
                throw new ArgumentOutOfRangeException($"Age {age} must be greater than 0");

            if (string.IsNullOrEmpty(name))
                throw new NameIsNullOrEmptyException();

            Name = name;
            Age = age;

        }


        public string Name { get; private set; }

        public int Age { get; private set; } // Improvement Notes: We could use BirthDate instead of Age. If the course takes 3 years to complete we will need to update this info every year.
    }
}
