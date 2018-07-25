using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.Interfaces
{
    public interface ICourseRepository : IRepository<Course>, IAsyncRepository<Course>
    {
        Course GetWithStudents(long id);
        IReadOnlyCollection<Course> GetAllWithStudents();              
    }
}
