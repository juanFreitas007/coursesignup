using Juan.CourseSignup.ApplicationCore.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.Infrastructure.Services
{
    public class AzureQueueReceiver<T> : IQueueReceiver<T> where T : class
    {
        public IConfiguration _configuration { get; }

        public AzureQueueReceiver(IConfiguration configuration)
        {
            var config = new AzureConfig();
            configuration.GetSection("AzureConfig").Bind(config);
            Init(config);
        }

        public void Receive(
            Func<T, MessageProcessResponse> onProcess,
            Action<Exception> onError,
            Action onWait)
        {
            var options = new MessageHandlerOptions(e =>
            {
                onError(e.Exception);
                return Task.CompletedTask;
            })
            {
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
            };

            client.RegisterMessageHandler(
                async (message, token) =>
                {
                    try
                    {
                        // Get message
                        var data = Encoding.UTF8.GetString(message.Body);
                        T item = JsonConvert.DeserializeObject<T>(data);

                        // Process message
                        var result = onProcess(item);

                        if (result == MessageProcessResponse.Complete)
                            await client.CompleteAsync(message.SystemProperties.LockToken);
                        else if (result == MessageProcessResponse.Abandon)
                            await client.AbandonAsync(message.SystemProperties.LockToken);
                        else if (result == MessageProcessResponse.Dead)
                            await client.DeadLetterAsync(message.SystemProperties.LockToken);

                        // Wait for next message
                        onWait();
                    }
                    catch (Exception ex)
                    {
                        await client.DeadLetterAsync(message.SystemProperties.LockToken);
                        onError(ex);
                    }
                }, options);
        }

        private QueueClient client;

        private void Init(AzureConfig config)
        {
            client = new QueueClient(config.AzureServiceBusConnection, config.QueueName);
        }

    }
}
