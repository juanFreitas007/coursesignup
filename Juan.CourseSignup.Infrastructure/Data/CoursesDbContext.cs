using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.Infrastructure.Data
{
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Course>(ConfigureCourses);
        }

        private void ConfigureCourses(EntityTypeBuilder<Course> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Course.Students));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
