using Juan.CourseSignup.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.Infrastructure.Logging
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public AppLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
