using Juan.CourseSignup.ApplicationCore.Interfaces;
using Juan.CourseSignup.WebApi.ViewModels.ExceptionResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.Filters
{
    public class ApiExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly IAppLogger<ApiExceptionFilter> _appLogger;

        public ApiExceptionFilter(IAppLogger<ApiExceptionFilter> appLogger)
        {
            _appLogger = appLogger;
        }

        public void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new ErrorViewModel(context.Exception.Message));

            _appLogger.LogError(context.Exception.Message, new[] { context.Exception });
        }
    }

}
