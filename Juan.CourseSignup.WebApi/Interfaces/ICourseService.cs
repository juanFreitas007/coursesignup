using Juan.CourseSignup.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.Interfaces
{
    public interface ICourseViewModelService
    {
        IEnumerable<CourseViewModel> GetAllCourses();
        IEnumerable<CourseViewModel> GetAllCourseDetailed();
        CourseWithStudentsViewModel GetCourseDetailedWithStudentsById(long id);
    }
}
