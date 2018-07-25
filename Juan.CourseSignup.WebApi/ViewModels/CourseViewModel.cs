using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.ViewModels
{
    public class CourseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
        public int? AverageAge { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalStudents { get; set; }
    }

    public class CourseWithStudentsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TeacherName { get; set; }
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
        public int? AverageAge { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalStudents { get; set; }
        public List<StudentViewModel> Students { get; set; } = new List<StudentViewModel>();
    }
}
