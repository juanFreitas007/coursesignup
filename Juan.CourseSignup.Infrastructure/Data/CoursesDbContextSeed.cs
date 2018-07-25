using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.Infrastructure.Data
{
    public class CoursesDbContextSeed
    {
        public static async Task SeedAsync(CoursesDbContext coursesDbContext)
        {
            if (!coursesDbContext.Courses.Any())
            {
                coursesDbContext.Courses.AddRange(GetPreConfiguredCourses());

                await coursesDbContext.SaveChangesAsync();
            }

        }

        private static IEnumerable<Course> GetPreConfiguredCourses()
        {
            var courses = new List<Course>();

            for (int i = 0; i < 100; i++)
            {
                courses.Add(new Course($".net Core Level {i}", 10, GetRandomStudents(10)));
            }

            courses.Add(new Course("Azure", 5, new List<Student>()));
            courses.Add(new Course("AWS", 5, new List<Student>()));
            courses.Add(new Course("Microservices", 2, new List<Student>()));
            courses.Add(new Course("Kubernetes", 20, new List<Student>()));
            
            return courses;
        }

        private static List<Student> GetRandomStudents(int countMaxRange)
        {
            var students = new List<Student>();

            Random rnd = new Random();

            int maxRange = rnd.Next(0, countMaxRange);
           
            for (int i = 0; i < maxRange; i++)
            {
                int randomAge = rnd.Next(18, 60);

                students.Add(new Student($"Student Number {i}", randomAge));
            }

            return students;
        }
    }
}
