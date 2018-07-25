using System;
using System.Collections.Generic;
using System.Text;

namespace Juan.CourseSignup.ApplicationCore.Interfaces
{
    public interface IQueueReceiver<T>
    {
        void Receive(Func<T, MessageProcessResponse> onProcess,
                Action<Exception> onError,
                Action onWait);
    }

    public enum MessageProcessResponse
    {
        Complete,
        Abandon,
        Dead
    }
}
