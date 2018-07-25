using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.Infrastructure.Services
{
    public class AzureConfig 
    {
        public string QueueName { get; set; }
        public string AzureServiceBusConnection { get; set; }
    }
}
