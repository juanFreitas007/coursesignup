using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Juan.CourseSignup.ApplicationCore.CourseAggregaate.Entities;
using Juan.CourseSignup.ApplicationCore.Entities;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using Juan.CourseSignup.WebApi.Converters;
using Juan.CourseSignup.WebApi.Interfaces;
using Juan.CourseSignup.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Juan.CourseSignup.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseStudentSignupService _courseStudentSignupService;
        private readonly ICourseViewModelService _courseViewModelService;
        private readonly ICourseRepository _courseRepository;
        private readonly IAppLogger<CoursesController> _appLogger;

        public CoursesController(ICourseStudentSignupService courseService, ICourseViewModelService courseViewModelService, ICourseRepository courseRepository, IAppLogger<CoursesController> appLogger)
        {
            _courseStudentSignupService = courseService;
            _courseViewModelService = courseViewModelService;
            _courseRepository = courseRepository;
            _appLogger = appLogger;
        }
         
        /// <summary>
        /// Get all Courses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<CourseViewModel> GetAllCourseDetailed()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var courses = _courseViewModelService.GetAllCourseDetailed();

            stopwatch.Stop();

            _appLogger.LogInformation($"Calling GetAllCourseDetailed took {stopwatch.ElapsedMilliseconds} ms");
            
            return courses;
        }

        /// <summary>
        /// Get Course with Students by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetCoursesWithStudentsById")]        
        public CourseWithStudentsViewModel GetCoursesWithStudentsById(long id)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var course = _courseViewModelService.GetCourseDetailedWithStudentsById(id);

            stopwatch.Stop();

            _appLogger.LogInformation($"Calling GetCoursesWithStudentsById took {stopwatch.ElapsedMilliseconds} ms");

            return course;
        }

        /// <summary>
        /// Add Student to Course
        /// </summary>
        /// <param name="id">Id of Course</param>
        /// <param name="student">Student details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/student", Name = "AddCourseStudent")]
        public IActionResult AddStudent(int id, [FromBody] StudentViewModel student)
        {
            _courseStudentSignupService.Signup(id, student.Name, student.Age);

            return Ok();
        }

        /// <summary>
        /// Add Student to Course Async
        /// </summary>
        /// <param name="id">Id of Course</param>
        /// <param name="student">Student details</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/queue/student", Name = "AddStudentAsync")]
        public async Task<IActionResult> AddStudentAsync(int id, [FromBody] StudentViewModel student)
        {
            // Notes: We do not use await because we want to do "Fire and Forget"
            _courseStudentSignupService.SignupAsync(id, student.Name, student.Age);

            return Ok();
        }

    }
}
