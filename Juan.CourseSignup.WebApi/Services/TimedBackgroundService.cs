using Juan.CourseSignup.ApplicationCore.Commands;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juan.CourseSignup.WebApi.Services
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IQueueReceiver<SignupStudentCourseCommand> _queueReceiver;
        private readonly IConfiguration _configuration;

        public TimedHostedService(ILogger<TimedHostedService> logger, IQueueReceiver<SignupStudentCourseCommand> queueReceiver, IConfiguration configuration)
        {
            _logger = logger;
            _queueReceiver = queueReceiver;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            _queueReceiver.Receive(message =>
            {
                SignupStudentAsync(message);

                return MessageProcessResponse.Complete;

            }, ex => { }, () => { });

            return Task.CompletedTask;
        }

        private async Task SignupStudentAsync(SignupStudentCourseCommand message)
        {
            var requestBody = JsonConvert.SerializeObject(message.Student);

            Uri signupStudentUri = GetSignupStudentUri(message.CourseId);

            var result = await Post(signupStudentUri, requestBody);

            _logger.LogInformation($"Signup {message.Student.Name} ResultStatusCode {result.StatusCode} ResultMessage: {result.ResultMessage}");

        }

        private Uri GetSignupStudentUri(long courseId)
        {
            var apiUrl = _configuration.GetSection("SignupServiceConfig").GetValue<string>("ApiUrl");

            return new Uri($"{apiUrl}/api/courses/{courseId}/student");
        }

        private async Task<ResultStatus> Post(Uri url, string body)
        {
            var jObject = JObject.Parse(body);

            var content = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 30, 0);

                using (HttpResponseMessage response = await client.PostAsync(url, content))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return new ResultStatus(response.StatusCode, responseContent);
                }
            }
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

    public class ResultStatus
    {
        public ResultStatus(HttpStatusCode statusCode, string resultMessage)
        {
            StatusCode = statusCode;
            ResultMessage = resultMessage;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string ResultMessage { get; set; }
    }

}
