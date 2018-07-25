using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.ViewModels.ExceptionResults
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string message)
        {
            Message = message;
        }
        public ErrorViewModel(string message, string code)
        {
            Message = message;
            ErrorCode = code;
        }

        public string Message { get; set; }
        public string ErrorCode { get; set; }
    }
}
