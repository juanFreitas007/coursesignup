using Juan.CourseSignup.ApplicationCore.Entities;
using Juan.CourseSignup.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.Converters
{
    public static class StudentConverter
    {
        public static StudentViewModel ToViewModel(this Student student)
        {
            return new StudentViewModel()
            {
                Name = student.Name,
                Age = student.Age
            };
        }

        public static IEnumerable<StudentViewModel> ToViewModels(this IEnumerable<Student> students)
        {
            return students.Select(a => a.ToViewModel());
        }
    }
}
