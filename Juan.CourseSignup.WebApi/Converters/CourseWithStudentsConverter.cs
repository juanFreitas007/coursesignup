using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.WebApi.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Juan.CourseSignup.WebApi.Converters
{
    public static class CourseWithStudentsConverter
    {
        public static CourseWithStudentsViewModel ToViewModelWithStudents(this Course course)
        {
            return new CourseWithStudentsViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                MaxCapacity = course.MaxCapacity,
                MaximumAge = course.GetStudentsMaximumAge(),
                MinimumAge = course.GetStudentsMinimumAge(),
                AverageAge = course.GetStudentsAverageAge(),
                TotalStudents = course.GetTotalStudents(),
                Students = course.Students.ToViewModels().ToList()
            };
        }

        public static IEnumerable<CourseWithStudentsViewModel> ToViewModelDetailedWithStudents(this IEnumerable<Course> courses)
        {
            return courses.Select(a => a.ToViewModelWithStudents());
        }
    }
}
