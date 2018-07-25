using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.Converters
{
    public static class CourseConverter
    {
        public static CourseViewModel ToViewModel(this Course course)
        {
            return new CourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                MaxCapacity = course.MaxCapacity,
                MaximumAge = course.GetStudentsMaximumAge(),
                MinimumAge = course.GetStudentsMinimumAge(),
                AverageAge = course.GetStudentsAverageAge(),
                TotalStudents = course.GetTotalStudents()
            };
        }

        public static IEnumerable<CourseViewModel> ToViewModelDetailed(this IEnumerable<Course> courses)
        {
            return courses.Select(a => a.ToViewModel());
        }
    }
}
