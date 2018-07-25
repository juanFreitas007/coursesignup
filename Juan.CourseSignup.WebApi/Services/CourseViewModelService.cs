using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using Juan.CourseSignup.WebApi.Interfaces;
using Juan.CourseSignup.WebApi.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Juan.CourseSignup.WebApi.Converters;

namespace Juan.CourseSignup.WebApi.Services
{
    public class CourseViewModelService : ICourseViewModelService
    {
        private readonly ICourseRepository _coursesRepository;

        public CourseViewModelService(ICourseRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            var courses = _coursesRepository.ListAll();

            return courses.ToViewModelDetailed();
        }

        public IEnumerable<CourseViewModel> GetAllCourseDetailed()
        {
            var courses = _coursesRepository.GetAllWithStudents();

            var coursesViewModels = courses.ToViewModelDetailed();

            return coursesViewModels;
        }

        public CourseWithStudentsViewModel GetCourseDetailedWithStudentsById(long id)
        {
            var courses = _coursesRepository.GetWithStudents(id);

            var coursesViewModels = courses.ToViewModelWithStudents();

            return coursesViewModels;
        }
    }
}
