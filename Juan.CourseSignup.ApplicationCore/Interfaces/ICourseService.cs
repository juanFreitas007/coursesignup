using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.Interfaces
{
    public interface ICourseStudentSignupService
    {
        void Signup(long courseId, string name, int age);     
        
        Task SignupAsync(long courseId, string name, int age);        
    }
}
