using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Juan.CourseSignup.Infrastructure.Data
{
    public class CourseRepository : EfGenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(CoursesDbContext context) : base(context)
        {

        }

        public Course GetWithStudents(long id)
        {
            var courses = this._dbContext.Courses.Include(s => s.Students);

            var course = courses.SingleOrDefault(c => c.Id == id);

            return course;
        }

        public IReadOnlyCollection<Course> GetAllWithStudents()
        {
            return this._dbContext.Courses.Include(s => s.Students).ToList().AsReadOnly();
        }

    }
}
