using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Exceptions;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using Juan.CourseSignup.ApplicationCore.Services;
using Juan.CourseSignup.Infrastructure.Data;
using Juan.CourseSignup.UnitTests.Builders;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Juan.CourseSignup.UnitTests.ApplicationCore
{
    public class AddStudentsToCourse
    {
        private Mock<ICourseRepository> _courseRepository;
        private Mock<IAppLogger<CourseStudentSignupService>> _appLogger;
        
        public AddStudentsToCourse()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _appLogger = new Mock<IAppLogger<CourseStudentSignupService>>();
        }

        [Fact]
        public void ThrowsGivenFullCourseCapacity()
        {
            _courseRepository.Setup(c => c.GetWithStudents(It.IsAny<long>())).Returns(new CourseBuilder().WithFullCapacity());

            var serviceMock = new CourseStudentSignupService(_courseRepository.Object, _appLogger.Object, null);

            var student = new StudentBuilder().WithDefaultValues();

            Assert.Throws<CourseIsFullException>(() =>
              serviceMock.Signup(It.IsAny<long>(), student.Name, student.Age));
        }

        [Fact]
        public void AddsOneStudentToEmptyCourseSuccessfully()
        {
            _courseRepository.Setup(c => c.GetWithStudents(It.IsAny<long>())).Returns(new CourseBuilder().WithDefaultValues());

            var serviceMock = new CourseStudentSignupService(_courseRepository.Object, _appLogger.Object, null);

            var student = new StudentBuilder().WithDefaultValues();

            serviceMock.Signup(It.IsAny<long>(), student.Name, student.Age);
        }
        

    }
}
