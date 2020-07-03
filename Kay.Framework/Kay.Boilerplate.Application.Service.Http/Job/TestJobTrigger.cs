using Kay.Framework.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kay.Boilerplate.Application.Service.Http.Job
{
    public class TestJobTrigger : BaseJobTrigger
    {
        public TestJobTrigger() :
            base(TimeSpan.Zero,
                TimeSpan.FromMinutes(1),
                new TestJobExcutor())
        {
            
        }
    }
    public class TestJobExcutor
                     : IJobExecutor
    {
        public void StartJob()
        {
        }

        public void StopJob()
        {
           
        }
    }
}
