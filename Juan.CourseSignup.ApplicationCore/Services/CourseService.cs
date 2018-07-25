using Juan.CourseSignup.ApplicationCore.Commands;
using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Entities;
using Juan.CourseSignup.ApplicationCore.Factories;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.Services
{
    public class CourseStudentSignupService : ICourseStudentSignupService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAppLogger<CourseStudentSignupService> _logger;
        private readonly ICommandRequestHandler<SignupStudentCourseCommand> _commandRequestHandler;

        public CourseStudentSignupService(ICourseRepository courseRepository, IAppLogger<CourseStudentSignupService> logger, ICommandRequestHandler<SignupStudentCourseCommand> commandRequestHandler)
        {
            _courseRepository = courseRepository;
            _logger = logger;
            _commandRequestHandler = commandRequestHandler;
        }


        public void Signup(long courseId, string name, int age)
        {
            var course = _courseRepository.GetWithStudents(courseId);

            var student = new Student(name, age);

            course.AddStudent(student);

            _courseRepository.Update(course);

            _logger.LogInformation($"Student with name {name} was signed up to the course {course.Name}.");
        }

        public async Task SignupAsync(long courseId, string name, int age)
        {
            var command = SignupStudentCourseCommandFactory.Get(courseId, name, age);

            await _commandRequestHandler.Handle(command);

            _logger.LogInformation($"");
        }
    }
}
