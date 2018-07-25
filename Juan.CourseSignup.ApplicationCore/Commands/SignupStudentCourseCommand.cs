using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Dtos;
using Juan.CourseSignup.ApplicationCore.Entities;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.ApplicationCore.Commands
{
    public class SignupStudentCourseCommand : IRequest
    {
        public long CourseId { get; set; }

        public StudentDto Student { get; set; }
    }
}
