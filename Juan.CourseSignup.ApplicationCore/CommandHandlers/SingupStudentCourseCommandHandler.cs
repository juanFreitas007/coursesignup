using Juan.CourseSignup.ApplicationCore.Commands;
using Juan.CourseSignup.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Juan.CourseSignup.ApplicationCore.CommandHandlers
{
    public class SingupStudentCourseCommandHandler : ICommandRequestHandler<SignupStudentCourseCommand>
    {
        private readonly IQueueSender<SignupStudentCourseCommand> _queueSender;

        public SingupStudentCourseCommandHandler(IQueueSender<SignupStudentCourseCommand> queueSender)
        {
            _queueSender = queueSender;
        } 
        
        public async Task Handle(SignupStudentCourseCommand request)
        {           
            await _queueSender.SendAsync(request);
        }
    }
}
