using Juan.CourseSignup.ApplicationCore.Interfaces;
using Juan.CourseSignup.Infrastructure.Settings;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.Infrastructure.Services
{
    public class AzureQueueSender<T> : IQueueSender<T> where T : class
    {
        public IConfiguration _configuration { get; }

        public AzureQueueSender(IConfiguration configuration)
        {
            var config = new AzureConfig();
            configuration.GetSection("AzureConfig").Bind(config);
            Init(config);
        }

        private QueueClient client;

        private void Init(AzureConfig config)
        {
            client = new QueueClient(config.AzureServiceBusConnection, config.QueueName);
        }


        public async Task SendAsync(T item)
        {
            await SendAsync(item, null);
        }

        public async Task SendAsync(T item, Dictionary<string, object> properties)
        {
            var json = JsonConvert.SerializeObject(item);
            var message = new Message(Encoding.UTF8.GetBytes(json));

            if (properties != null)
            {
                foreach (var prop in properties)
                {
                    message.UserProperties.Add(prop.Key, prop.Value);
                }
            }

            await client.SendAsync(message);
        }

    }
}
