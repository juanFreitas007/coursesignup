using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.Interfaces
{
    public interface IQueueSender<T>
    {
        Task SendAsync(T item);
        Task SendAsync(T item, Dictionary<string, object> properties);
    }
}
