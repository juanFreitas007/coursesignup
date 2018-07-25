using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.Interfaces
{
    public interface ICommandRequestHandler<T> where T : IRequest
    {
        Task Handle(T request);
    }

    public interface IRequest
    {

    }
}
